using System;
using System.Collections.ObjectModel;

using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Services;

using GalaSoft.MvvmLight;
using CongratulatoryMoneyManagement.Services.DataService;
using System.Collections.Generic;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class StatementViewModel : ViewModelBase
    {
        #region Fields

        private IDataService dataService;

        #endregion

        #region Constructors & Initialize

        public StatementViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        #endregion

        public IReadOnlyList<CongratulatoryMoney> Source
        {
            get
            {
                return dataService.AllCongratulatoryMoney();
            }
        }
    }
}
