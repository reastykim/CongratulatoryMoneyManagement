using System;

using CongratulatoryMoneyManagement.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public sealed partial class TakePage : Page
    {
        private TakeViewModel ViewModel
        {
            get { return DataContext as TakeViewModel; }
        }

        public TakePage()
        {
            InitializeComponent();
        }
    }
}
