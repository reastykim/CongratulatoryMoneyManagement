using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongratulatoryMoneyManagement.Helpers;
using GalaSoft.MvvmLight;

namespace CongratulatoryMoneyManagement.Models
{
    /// <summary>
    /// 금액 선택 옵션
    /// </summary>
    public class MoneyOption : ObservableObject, ISelectable
    {
        public static MoneyOption Input => input;
        private static MoneyOption input = new MoneyOption(Decimal.Zero, "MoneyOption_Input".GetLocalized());

        public static double SmallChange => double.Parse("MoneyOption_SmallChange".GetLocalized());

        public string Display
        {
            get => display;
            set => Set(ref display, value);
        }
        private string display;

        public decimal Sum
        {
            get => sum;
            set => Set(ref sum, value);
        }
        private decimal sum;

        public bool IsSelected
        {
            get => isSelected;
            set => Set(ref isSelected, value);
        }
        private bool isSelected;
        
        public MoneyOption(decimal sum, string display = null)
        {
            Sum = sum;
            Display = display ?? sum.ToString("C");            
        }
    }
}
