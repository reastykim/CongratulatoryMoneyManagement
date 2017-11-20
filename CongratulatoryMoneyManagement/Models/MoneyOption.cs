using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongratulatoryMoneyManagement.Helpers;
using GalaSoft.MvvmLight;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongratulatoryMoneyManagement.Models
{
    /// <summary>
    /// 금액 선택 옵션
    /// </summary>
    public class MoneyOption : ObservableObject, ISelectable
    {
        public static double SmallChange => double.Parse("MoneyOption_SmallChange".GetLocalized());

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Display
        {
            get => display;
            set => Set(ref display, value);
        }
        private string display;

        public double Sum
        {
            get => sum;
            set => Set(ref sum, value);
        }
        private double sum;

        public bool IsSelected
        {
            get => isSelected;
            set => Set(ref isSelected, value);
        }
        private bool isSelected;

        public MoneyOption() { }
        public MoneyOption(double sum, string display = null, bool isSelected = false)
        {
            Sum = sum;
            Display = display ?? sum.ToString("C");            
        }
    }
}
