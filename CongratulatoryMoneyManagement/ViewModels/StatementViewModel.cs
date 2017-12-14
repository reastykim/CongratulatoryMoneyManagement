using System;
using System.Collections.ObjectModel;

using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Services;

using GalaSoft.MvvmLight;
using CongratulatoryMoneyManagement.Services.DataService;
using System.Collections.Generic;
using CongratulatoryMoneyManagement.Views;
using GalaSoft.MvvmLight.Command;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class StatementViewModel : ViewModelBase
    {
        #region Properties

        public IEnumerable<IStatementItem> Source
        {
            get { return source; }
            private set { Set(ref source, value); }
        }
        private IEnumerable<IStatementItem> source;

        #endregion

        #region Fields

        private IDataService dataService;

        #endregion

        #region Constructors & Initialize

        public StatementViewModel(IDataService dataService)
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
            UpdateAllConCongratulatoryMoney();
        }

        public RelayCommand UnloadedCommand
        {
            get => unloadedCommand ?? (unloadedCommand = new RelayCommand(ExecuteUnloaded));
        }
        private RelayCommand unloadedCommand;
        private void ExecuteUnloaded()
        {

        }

        #endregion
        
        private async void UpdateAllConCongratulatoryMoney()
        {
            Source = await dataService.GetAllStatementAsync();
        }
    }
}
