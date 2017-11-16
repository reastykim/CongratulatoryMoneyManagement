using CongratulatoryMoneyManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Services.DataService
{
    public interface IDataService
    {
        IReadOnlyList<MoneyOption> AllMoneyOptions();

        void SaveCongratulatoryMoney(CongratulatoryMoney item);

        // Will be remove
        ObservableCollection<SampleOrder> GetGridSampleData();
    }
}
