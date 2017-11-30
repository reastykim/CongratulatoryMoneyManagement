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
        Task<IReadOnlyList<MoneyOption>> AllMoneyOptionsAsync();

        Task<int> SaveCongratulatoryMoneyAsync(CongratulatoryMoney item);

        Task<IReadOnlyList<CongratulatoryMoney>> AllCongratulatoryMoneyAsync();

        Task<int> SaveSpendingAsync(Spending item);

        Task<IReadOnlyList<StatementItem>> GetAllStatementAsync();
    }
}
