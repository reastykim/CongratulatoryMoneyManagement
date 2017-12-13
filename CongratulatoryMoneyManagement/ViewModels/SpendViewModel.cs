using System;

using GalaSoft.MvvmLight;
using CongratulatoryMoneyManagement.Services.DataService;
using CongratulatoryMoneyManagement.Views;
using GalaSoft.MvvmLight.Command;
using CongratulatoryMoneyManagement.Models;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class SpendViewModel : ViewModelBase
    {
        #region Properties

        public string Details
        {
            get { return details; }
            set
            {
                if (Set(ref details, value))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string details;

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

        public bool ShowBottomAppBar
        {
            get => showBottomAppBar;
            set => Set(ref showBottomAppBar, value);
        }
        private bool showBottomAppBar;

        #endregion

        #region Fields

        private IDataService dataService;

        #endregion

        #region Constructors & Initialize

        public SpendViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        #endregion

        #region Commands

        public RelayCommand LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new RelayCommand(ExecuteLoaded));
        }
        private RelayCommand loadedCommand;
        private void ExecuteLoaded()
        {
            ShowBottomAppBar = true;
        }

        public RelayCommand UnloadedCommand
        {
            get => unloadedCommand ?? (unloadedCommand = new RelayCommand(ExecuteUnloaded));
        }
        private RelayCommand unloadedCommand;
        private void ExecuteUnloaded()
        {
            ShowBottomAppBar = false;
        }

        #region AppBar Commands

        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(ExecuteSave, CanExecuteSave));
        }
        private RelayCommand saveCommand;
        private async void ExecuteSave()
        {
            var newItem = new Spending();
            newItem.Sum = Sum;
            newItem.Details = Details;

            await dataService.SaveSpendingAsync(newItem);
            ResetCommand.Execute(null);
        }
        private bool CanExecuteSave()
        {
            return Sum > 0 && String.IsNullOrWhiteSpace(Details) != true;
        }

        public RelayCommand ResetCommand
        {
            get => resetCommand ?? (resetCommand = new RelayCommand(ExecuteReset));
        }
        private RelayCommand resetCommand;
        private void ExecuteReset()
        {
            Sum = 0;
            Details = null;
        }

        #endregion

        #endregion
    }
}
