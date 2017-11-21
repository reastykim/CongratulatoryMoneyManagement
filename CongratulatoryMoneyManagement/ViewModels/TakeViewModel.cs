using System;
using System.Linq;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media.Imaging;
using CongratulatoryMoneyManagement.EventHandlers;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using CongratulatoryMoneyManagement.Models;
using System.Collections.Generic;
using CongratulatoryMoneyManagement.Services.DataService;
using CongratulatoryMoneyManagement.Views;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class TakeViewModel : ViewModelBase, IPivotItemActivate
    {
        #region Properties
        
        public IReadOnlyList<MoneyOption> MoneyOptions { get; private set; }

        public MoneyOption SelectedMoneyOption
        {
            get => MoneyOptions.FirstOrDefault(MO => MO.IsSelected);
            set
            {
                foreach (var moneyOption in MoneyOptions)
                {
                    var isSelected = moneyOption == value;
                    moneyOption.IsSelected = isSelected;
                }
                RaisePropertyChanged("SelectedMoneyOption");
            }
        }

        public Uri PhotoUri
        {
            get { return photoUri; }
            set { Set(ref photoUri, value); }
        }
        private Uri photoUri;

        public double Sum
        {
            get { return sum; }
            set
            {
                if (Set(ref sum, value))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private double sum;
        
        public string GuestName
        {
            get { return guestName; }
            set
            {
                if (Set(ref guestName, value))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string guestName;

        public ReturnPresentType SelectedReturnPresentType
        {
            get => selectedReturnPresentType;
            set => Set(ref selectedReturnPresentType, value);
        }
        private ReturnPresentType selectedReturnPresentType;

        public uint ReturnPresentQuantity
        {
            get => returnPresentQuantity;
            set => Set(ref returnPresentQuantity, value);
        }
        private uint returnPresentQuantity = 1;

        #endregion

        #region Fields

        private IDataService dataService;

        #endregion

        #region Constructor & Initialize

        public TakeViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            Initialize();            
        }

        private void Initialize()
        {
            MoneyOptions = dataService.AllMoneyOptions();
        }

        #endregion

        #region Commands

        #region Select Commands

        public RelayCommand<MoneyOption> SelectMoneyOptionCommand
        {
            get => selectMoneyOptionCommand ?? (selectMoneyOptionCommand = new RelayCommand<MoneyOption>(ExecuteSelectMoneyOption));
        }
        private RelayCommand<MoneyOption> selectMoneyOptionCommand;
        private void ExecuteSelectMoneyOption(MoneyOption moneyOption)
        {
            SelectedMoneyOption = moneyOption;
            Sum = SelectedMoneyOption.Sum;
        }

        public RelayCommand<ReturnPresentType> SelectReturnPresentTypeCommand
        {
            get => selectReturnPresentTypeCommand ?? (selectReturnPresentTypeCommand = new RelayCommand<ReturnPresentType>(ExecuteSelectReturnPresentType));
        }
        private RelayCommand<ReturnPresentType> selectReturnPresentTypeCommand;
        private void ExecuteSelectReturnPresentType(ReturnPresentType returnPresentType)
        {
            SelectedReturnPresentType = returnPresentType;
        }

        #endregion


        #region Page Commands

        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(ExecuteSave, CanExecuteSave));
        }
        private RelayCommand saveCommand;
        private void ExecuteSave()
        {
            var newItem = new CongratulatoryMoney();
            newItem.GuestName = GuestName;
            newItem.Sum = Sum;
            newItem.EnvelopeImageUri = PhotoUri?.AbsolutePath;
            newItem.ReturnPresent.Type = selectedReturnPresentType;
            newItem.ReturnPresent.Quantity = ReturnPresentQuantity;

            dataService.SaveCongratulatoryMoney(newItem);
            ResetCommand.Execute(null);
        }
        private bool CanExecuteSave()
        {
            return Sum > 0;// && String.IsNullOrWhiteSpace(GuestName) != true;
        }

        public RelayCommand ResetCommand
        {
            get => resetCommand ?? (resetCommand = new RelayCommand(ExecuteReset));
        }
        private RelayCommand resetCommand;
        private void ExecuteReset()
        {
            SelectMoneyOptionCommand.Execute(MoneyOptions.FirstOrDefault(MO => MO.Sum == 0d));
            GuestName = String.Empty;
            SelectedReturnPresentType = ReturnPresentType.MealTickets;
            ReturnPresentQuantity = 1;
        }

        #endregion

        #endregion

        #region Implemented IPivotItemActivate Interface

        public void OnPivotItemActived()
        {

        }

        public void OnPivotItemDeactived()
        {

        }

        #endregion
    }
}
