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
    public class CongratulatoryMoney : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        /// 주신 분
        /// </summary>
        public string GuestName
        {
            get => guestName;
            set => Set(ref guestName, value);
        }
        private string guestName;

        /// <summary>
        /// 봉투 사진 경로
        /// </summary>
        public string EnvelopeImageUri
        {
            get => envelopeImageUri;
            set => Set(ref envelopeImageUri, value);
        }
        private string envelopeImageUri;

        /// <summary>
        /// 답례품
        /// </summary>
        public virtual ReturnPresent ReturnPresent
        {
            get => returnPresent;
            set => Set(ref returnPresent, value);
        }
        private ReturnPresent returnPresent = new ReturnPresent();
    }
}
