using System;

using CongratulatoryMoneyManagement.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public sealed partial class StatementPage : Page
    {
        private StatementViewModel ViewModel
        {
            get { return DataContext as StatementViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public StatementPage()
        {
            InitializeComponent();
        }
    }
}
