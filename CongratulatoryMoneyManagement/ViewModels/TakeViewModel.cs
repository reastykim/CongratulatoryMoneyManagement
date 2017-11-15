using System;

using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media.Imaging;
using CongratulatoryMoneyManagement.EventHandlers;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using CongratulatoryMoneyManagement.Models;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class TakeViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// 축의금 정보
        /// </summary>
        public CongratulatoryMoney CongratulatoryMoney
        {
            get => congratulatoryMoney;
            set => Set(ref congratulatoryMoney, value);
        }
        private CongratulatoryMoney congratulatoryMoney;
        
        #endregion

        #region Constructor & Initialize

        public TakeViewModel()
        {
        }

        #endregion

        #region Commands
        
        #endregion
    }
}
