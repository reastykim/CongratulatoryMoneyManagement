using CongratulatoryMoneyManagement.Helpers;
using CongratulatoryMoneyManagement.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CongratulatoryMoneyManagement.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationRootPage : Page
    {
        private NavigationServiceEx navigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();
        public Frame AppFrame
        {
            get
            {
                return appNavFrame;
            }
        }

        private bool hasLoadedPreviously;


        public NavigationRootPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Only do an inital navigate the first time the page loads
            // when we switch out of compactoverloadmode this will fire but we don't want to navigate because
            // there is already a page loaded
            if (!hasLoadedPreviously)
            {
                navigationService.Frame = AppFrame;
                navview.SelectedItem = navview.MenuItems[0];
                hasLoadedPreviously = true;
            }
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // When the navigationService be called GoBack() method, set the IsSelected for target menu.
            switch (e.SourcePageType)
            {
                case Type c when e.SourcePageType == typeof(TakePage):
                    ((NavigationViewItem)navview.MenuItems[0]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(SpendPage):
                    ((NavigationViewItem)navview.MenuItems[1]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(StatementPage):
                    ((NavigationViewItem)navview.MenuItems[2]).IsSelected = true;
                    break;
            }
        }
    }
}
