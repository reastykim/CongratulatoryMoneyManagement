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

        Task SaveCongratulatoryMoneyAsync(CongratulatoryMoney item);

        Task<IReadOnlyList<CongratulatoryMoney>> AllCongratulatoryMoneyAsync();

        Task SaveSpendingAsync(Spending item);
    }
}
