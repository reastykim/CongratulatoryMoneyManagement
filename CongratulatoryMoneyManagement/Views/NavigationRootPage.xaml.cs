using CongratulatoryMoneyManagement.Helpers;
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
        public Frame AppFrame
        {
            get
            {
                return appNavFrame;
            }
        }

        public NavigationRootPage()
        {
            this.InitializeComponent();
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {
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
