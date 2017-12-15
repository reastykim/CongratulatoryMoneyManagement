using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        public double TotalCongratulatoryMoney
        {
            get { return totalCongratulatoryMoney; }
            private set { Set(ref totalCongratulatoryMoney, value); }
        }
        private double totalCongratulatoryMoney;

        public double TotalSpending
        {
            get { return totalSpending; }
            private set { Set(ref totalSpending, value); }
        }
        private double totalSpending;

        public double TotalSummary
        {
            get { return totalSummary; }
            private set { Set(ref totalSummary, value); }
        }
        private double totalSummary;

        public long MealTicketsCount
        {
            get { return mealTicketsCount; }
            private set { Set(ref mealTicketsCount, value); }
        }
        private long mealTicketsCount;

        public long PresentsCount
        {
            get { return presentsCount; }
            private set { Set(ref presentsCount, value); }
        }
        private long presentsCount;

        public long FaresCount
        {
            get { return faresCount; }
            private set { Set(ref faresCount, value); }
        }
        private long faresCount;

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
            Update();
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
        
        private async void Update()
        {
            try
            {
                Source = await dataService.GetAllStatementsAsync();
                TotalCongratulatoryMoney = Source.OfType<CongratulatoryMoney>().Sum(CM => CM.Sum);
                TotalSpending = Source.OfType<Spending>().Sum(CM => CM.Sum);
                TotalSummary = Source.Sum(S => S.SumForSummary);
                var returnPresents = await dataService.GetAllReturnPresentsAsync();
                MealTicketsCount = returnPresents.Where(RP => RP.Type == ReturnPresentType.MealTickets).Sum(RP => RP.Quantity);
                PresentsCount = returnPresents.Where(RP => RP.Type == ReturnPresentType.Present).Sum(RP => RP.Quantity);
                FaresCount = returnPresents.Where(RP => RP.Type == ReturnPresentType.Fare).Sum(RP => RP.Quantity);
            }
            catch { }
        }
    }
}
