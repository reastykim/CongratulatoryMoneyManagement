using CongratulatoryMoneyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Data;
using CongratulatoryMoneyManagement.Helpers;

namespace CongratulatoryMoneyManagement.Converters
{
    public class GridGroupToSummaryConverter : IValueConverter
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

            return String.Format($"{"Summary".GetLocalized()} : {StringFormat}", context.Group.ChildItems.OfType<IStatementItem>().Sum(SI => SI.SumForSummary));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
