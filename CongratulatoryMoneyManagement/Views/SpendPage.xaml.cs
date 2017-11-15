using System;

using CongratulatoryMoneyManagement.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public sealed partial class SpendPage : Page
    {
        private SpendViewModel ViewModel
        {
            get { return DataContext as SpendViewModel; }
        }

        public SpendPage()
        {
            InitializeComponent();
        }
    }
}
