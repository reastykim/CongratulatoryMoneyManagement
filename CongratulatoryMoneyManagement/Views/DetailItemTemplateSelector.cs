using CongratulatoryMoneyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public class DetailItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CongratulatoryMoneyDataTemplate { get; set; }
        public DataTemplate SpendingDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var itemType = item?.GetType();
            if (item == null)
                return null;

            switch (itemType)
            {
                case Type c when itemType == typeof(CongratulatoryMoney):
                    return CongratulatoryMoneyDataTemplate;
                case Type c when itemType == typeof(Spending):
                    return SpendingDataTemplate;
                default:
                    return base.SelectTemplateCore(item);
            }
        }
    }
}
