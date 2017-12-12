using System;
using System.Collections.Generic;
using System.Linq;

using CongratulatoryMoneyManagement.Helpers;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;

namespace CongratulatoryMoneyManagement.Services
{
    public class NavigationServiceEx
    {
        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        private Frame _frame;

        public Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }

            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        private bool _isNavigating;

        public bool CanGoBack => Frame.CanGoBack;

        public async Task GoBackAsync()
        {
            if (Frame.CanGoBack)
            {
                _isNavigating = true;

                Page navigatedPage = await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    Frame.GoBack();
                    return Frame.Content as Page;
                });
            }
        }

        public async Task<bool> NavigateAsync(string pageKey, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            lock (_pages)
            {
                if (!_pages.ContainsKey(pageKey))
                {
                    throw new ArgumentException(string.Format("ExceptionNavigationServiceExPageNotFound".GetLocalized(), pageKey), nameof(pageKey));
                }
            }

            // Early out if already in the middle of a Navigation
            if (_isNavigating)
            {
                return false;
            }

            _isNavigating = true;

            return await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                return Frame.Navigate(_pages[pageKey], parameter, infoOverride);
            });
        }

        public async Task<bool> NavigateAsync(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            lock (_pages)
            {
                if (!_pages.ContainsValue(pageType))
                {
                    throw new ArgumentException(string.Format("ExceptionNavigationServiceExPageNotFound".GetLocalized(), pageType.FullName), nameof(pageType.FullName));
                }
            }

            // Early out if already in the middle of a Navigation
            if (_isNavigating)
            {
                return false;
            }

            _isNavigating = true;

            return await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                return Frame.Navigate(pageType, parameter, infoOverride);
            });
        }

        public void Configure(string key, Type pageType)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("ExceptionNavigationServiceExKeyIsInNavigationService".GetLocalized(), key));
                }

                if (_pages.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException(string.Format("ExceptionNavigationServiceExTypeAlreadyConfigured".GetLocalized(), _pages.First(p => p.Value == pageType).Key));
                }

                _pages.Add(key, pageType);
            }
        }

        public string GetNameOfRegisteredPage(Type page)
        {
            lock (_pages)
            {
                if (_pages.ContainsValue(page))
                {
                    return _pages.FirstOrDefault(p => p.Value == page).Key;
                }
                else
                {
                    throw new ArgumentException(string.Format("ExceptionNavigationServiceExPageUnknow".GetLocalized(), page.Name));
                }
            }
        }

        private void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += Frame_Navigated;
                _frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        private void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= Frame_Navigated;
                _frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            _isNavigating = false;
            Navigated?.Invoke(sender, e);
        }
    }
}
