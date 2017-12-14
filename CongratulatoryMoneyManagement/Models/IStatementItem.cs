using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Models
{
    public interface IStatementItem
    {
        string ItemTypeDisplay { get; }
        int Id { get; }
        string Details { get; }
        double SumForSummary { get; }
        DateTime Created { get; }
    }
}
