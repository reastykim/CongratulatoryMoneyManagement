using CongratulatoryMoneyManagement.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public sealed partial class CameraPage : Page
    {
        private CameraViewModel ViewModel
        {
            get { return DataContext as CameraViewModel; }
        }

        public CameraPage()
        {
            InitializeComponent();
        }
    }
}
