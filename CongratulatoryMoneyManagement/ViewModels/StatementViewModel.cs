using System;
using System.Collections.ObjectModel;

using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Services;

using GalaSoft.MvvmLight;
using CongratulatoryMoneyManagement.Services.DataService;
using System.Collections.Generic;
using CongratulatoryMoneyManagement.Views;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class StatementViewModel : ViewModelBase, IPivotItemActivate
    {
        #region Properties

        public IReadOnlyList<CongratulatoryMoney> Source
        {
            get { return source; }
            private set { Set(ref source, value); }
        }
        private IReadOnlyList<CongratulatoryMoney> source;

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

        #region Implemented IPivotItemActivate Interface

        public void OnPivotItemActived()
        {
            UpdateAllConCongratulatoryMoney();
        }

        public void OnPivotItemDeactived()
        {

        }

        #endregion

        private async void UpdateAllConCongratulatoryMoney()
        {
            Source = await dataService.AllCongratulatoryMoneyAsync();
        }
    }
}
