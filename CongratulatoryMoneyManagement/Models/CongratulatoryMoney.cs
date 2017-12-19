using CongratulatoryMoneyManagement.Data;
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
    public class CongratulatoryMoney : ObservableObject, IStatementItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CongratulatoryMoneyId { get; set; }

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

        public string RecognizedText
        {
            get => recognizedText;
            set => Set(ref recognizedText, value);
        }
        private string recognizedText;

        /// <summary>
        /// 생성 날짜/시간
        /// </summary>
        public DateTime Created
        {
            get => created;
            set => Set(ref created, value);
        }
        private DateTime created;

        /// <summary>
        /// 봉투 사진 경로
        /// </summary>
        public string EnvelopeImageUri
        {
            get => envelopeImageUri;
            set => Set(ref envelopeImageUri, value);
        }
        private string envelopeImageUri;

        [ForeignKey("ReturnPresent")]
        public int ReturnPresentId
        {
            get => returnPresentId;
            set => Set(ref returnPresentId, value);
        }
        private int returnPresentId;

        /// <summary>
        /// 답례품
        /// </summary>
        public virtual ReturnPresent ReturnPresent
        {
            get
            {
                if (returnPresent == null)
                {
                    using (var db = new CongratulatoryMoneyContext())
                    {
                        returnPresent = db.ReturnPresents.Find(ReturnPresentId);
                    }
                }

                return returnPresent;
            }
            set
            {
                Set(ref returnPresent, value);
            }
        }
        private ReturnPresent returnPresent;

        [NotMapped]
        public string ItemTypeDisplay => "\uE944";

        [NotMapped]
        public string Details => $"{GuestName}({RecognizedText})";

        [NotMapped]
        public double SumForSummary => Sum;

        public CongratulatoryMoney()
        {
            Created = DateTime.Now;
        }
    }
}
