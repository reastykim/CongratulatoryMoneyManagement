using CongratulatoryMoneyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Data;

namespace CongratulatoryMoneyManagement.Converters
{
    public class GridGroupToSummeryConverter : IValueConverter
    {
        public string StringFormat
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var context = value as GroupHeaderContext;
            if (context == null)
                return null;

            return String.Format(StringFormat, context.Group.ChildItems.OfType<StatementItem>().Sum(SI => SI.Sum));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
