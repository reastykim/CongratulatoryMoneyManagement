using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CongratulatoryMoneyManagement.Converters
{
    public class NullToCollapsedConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (String.Equals(parameter, "Inverse"))
            {
                return value == null ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
