using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongratulatoryMoneyManagement.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CongratulatoryMoneyManagement.Converters
{
    public class StringToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string key)
            {
                return key.GetLocalized();
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
