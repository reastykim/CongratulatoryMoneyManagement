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
        Task<IEnumerable<MoneyOption>> AllMoneyOptionsAsync();

        Task<int> SaveCongratulatoryMoneyAsync(CongratulatoryMoney item);

        Task<int> SaveSpendingAsync(Spending item);

        Task<IEnumerable<IStatementItem>> GetAllStatementAsync();
    }
}
