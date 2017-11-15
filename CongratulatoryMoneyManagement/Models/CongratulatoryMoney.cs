using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Models
{
    public class CongratulatoryMoney : ObservableObject
    {
        /// <summary>
        /// 금액
        /// </summary>
        public double Money
        {
            get => money;
            set => Set(ref money, value);
        }
        private double money;

        /// <summary>
        /// 주신 분
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        private string name;

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
        public ReturnPresent ReturnPresent
        {
            get => returnPresent;
            set => Set(ref returnPresent, value);
        }
        private ReturnPresent returnPresent = new ReturnPresent();
    }
}
