using System;
using System.Collections.ObjectModel;

using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Services;

using GalaSoft.MvvmLight;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class StatementViewModel : ViewModelBase
    {
        public ObservableCollection<SampleOrder> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return null;// SampleDataService.GetGridSampleData();
            }
        }
    }
}
