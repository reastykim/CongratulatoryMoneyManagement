using CongratulatoryMoneyManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Views
{
    public class PageCommandBarTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TakePageCommandBarTemplate { get; set; }
        public DataTemplate SpendPageCommandBarTemplate { get; set; }
        public DataTemplate StatementPageCommandBarTemplate { get; set; }
        public DataTemplate SettingsPageCommandBarTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var itemType = item?.GetType();
            if (item == null)
                return null;
            
            switch (itemType)
            {
                case Type c when itemType == typeof(TakeViewModel):
                    return TakePageCommandBarTemplate;
                case Type c when itemType == typeof(SpendViewModel):
                    return SpendPageCommandBarTemplate;
                case Type c when itemType == typeof(StatementViewModel):
                    return StatementPageCommandBarTemplate;
                case Type c when itemType == typeof(SettingsViewModel):
                    return SettingsPageCommandBarTemplate;
                default:
                    return base.SelectTemplateCore(item);
            }
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
