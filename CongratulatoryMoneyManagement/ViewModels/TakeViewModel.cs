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

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class TakeViewModel : ViewModelBase
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
            }
        }
        
        public decimal Sum
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
        private decimal sum;
        
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
            //newItem.ReturnPresent = new ReturnPresent();
            dataService.SaveCongratulatoryMoney(newItem);
            ResetCommand.Execute(null);
        }
        private bool CanExecuteSave()
        {
            return Sum > 0 && String.IsNullOrWhiteSpace(GuestName) != true;
        }

        public RelayCommand ResetCommand
        {
            get => resetCommand ?? (resetCommand = new RelayCommand(ExecuteReset));
        }
        private RelayCommand resetCommand;
        private void ExecuteReset()
        {
            GuestName = String.Empty;
            SelectedMoneyOption = MoneyOptions.FirstOrDefault(MO => MO.Sum == Decimal.Zero);
        }

        #endregion
    }
}
