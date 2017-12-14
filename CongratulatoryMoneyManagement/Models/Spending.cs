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
    /// 지출
    /// </summary>
    public class Spending : ObservableObject, IStatementItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 상세내용
        /// </summary>
        public string Details
        {
            get => details;
            set => Set(ref details, value);
        }
        private string details;

        /// <summary>
        /// 금액
        /// </summary>
        public double Sum
        {
            get => sum;
            set => Set(ref sum, value);
        }
        private double sum;

        /// <summary>
        /// 생성 날짜/시간
        /// </summary>
        public DateTime Created
        {
            get => created;
            set => Set(ref created, value);
        }
        private DateTime created;

        [NotMapped]
        public string ItemTypeDisplay => "\uE8A7";

        [NotMapped]
        public double SumForSummary => -Sum;

        public Spending()
        {
            Created = DateTime.Now;
        }
    }
}
