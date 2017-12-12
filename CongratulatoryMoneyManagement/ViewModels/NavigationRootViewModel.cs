using CongratulatoryMoneyManagement.Helpers;
using CongratulatoryMoneyManagement.Services;
using CongratulatoryMoneyManagement.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class NavigationRootViewModel : ViewModelBase
    {
        #region Fields

        private NavigationServiceEx navigationService;

        #endregion

        #region Constructors

        public NavigationRootViewModel(NavigationServiceEx navigationService)
        {
            this.navigationService = navigationService;
        }

        #endregion



        #region Commands

        public RelayCommand LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new RelayCommand(ExecuteLoaded));
        }
        private RelayCommand loadedCommand;
        private void ExecuteLoaded()
        {
            // Only do an inital navigate the first time the page loads
            // when we switch out of compactoverloadmode this will fire but we don't want to navigate because
            // there is already a page loaded
            //if (!hasLoadedPreviously)
            //{
            //    var pageKey = navigationService.GetNameOfRegisteredPage(typeof(TakePage));
            //    navigationService.Navigate(pageKey);
            //    hasLoadedPreviously = true;
            //}

            //ViewModeService.Instance.Register(navview, appNavFrame);

            //if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox")
            //{
            //    ViewModeService.Instance.CollapseNavigationViewToBurger();
            //    TitleBarHelper.Instance.TitleVisibility = Visibility.Collapsed;
            //}
        }

        public RelayCommand<NavigationViewItemInvokedEventArgs> NavViewItemInvokedCommand
        {
            get => navViewItemInvokedCommand ?? (navViewItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(ExecuteNavViewItemInvoked));
        }
        private RelayCommand<NavigationViewItemInvokedEventArgs> navViewItemInvokedCommand;
        private async void ExecuteNavViewItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                await navigationService.NavigateAsync(typeof(SettingsPage));
                return;
            }

            switch (args.InvokedItem as string)
            {
                case "Take":
                case "입력":
                    await navigationService.NavigateAsync(typeof(TakePage));
                    break;
                case "Spend":
                case "지출":
                    await navigationService.NavigateAsync(typeof(SpendPage));
                    break;
                case "Statement":
                case "내역":
                    await navigationService.NavigateAsync(typeof(StatementPage));
                    break;
            }
        }

        #endregion
    }
}
