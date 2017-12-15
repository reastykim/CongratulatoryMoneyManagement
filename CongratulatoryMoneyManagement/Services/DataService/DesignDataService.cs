using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongratulatoryMoneyManagement.Models;

namespace CongratulatoryMoneyManagement.Services.DataService
{
    public class DesignDataService : IDataService
    {
        public Task<IEnumerable<MoneyOption>> AllMoneyOptionsAsync()
        {
            return Task.FromResult(Enumerable.Empty<MoneyOption>());
        }

        public Task<IEnumerable<ReturnPresent>> GetAllReturnPresentsAsync()
        {
            return Task.FromResult(Enumerable.Empty<ReturnPresent>());
        }

        public Task<IEnumerable<IStatementItem>> GetAllStatementsAsync()
        {
            return Task.FromResult(Enumerable.Empty<IStatementItem>());
        }

        public Task<int> SaveCongratulatoryMoneyAsync(CongratulatoryMoney item)
        {
            return Task.FromResult(0);
        }

        public Task<int> SaveSpendingAsync(Spending item)
        {
            return Task.FromResult(0);
        }
    }
}
