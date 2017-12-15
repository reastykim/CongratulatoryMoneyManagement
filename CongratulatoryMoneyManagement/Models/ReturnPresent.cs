using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Models
{
    /// <summary>
    /// 답례품 종류
    /// </summary>
    public enum ReturnPresentType
    {
        /// <summary>
        /// 식권
        /// </summary>
        MealTickets,
        /// <summary>
        /// 선물
        /// </summary>
        Present,
        /// <summary>
        /// 교통비
        /// </summary>
        Fare
    }

    /// <summary>
    /// 답례품
    /// </summary>
    public class ReturnPresent : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReturnPresentId { get; set; }

        /// <summary>
        /// 답례품 종류
        /// </summary>
        public ReturnPresentType Type
        {
            get => type;
            set => Set(ref type, value);
        }
        private ReturnPresentType type;

        /// <summary>
        /// 답례품 수량
        /// </summary>
        public uint Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }
        private uint quantity = 1;
    }
}
