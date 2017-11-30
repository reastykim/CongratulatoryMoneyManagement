using CongratulatoryMoneyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Extensions
{
    public static class ModelExtensions
    {
        public static StatementItem AsStatementItem(this CongratulatoryMoney congratulatoryMoney)
        {
            return new StatementItem
            {
                ItemType = typeof(CongratulatoryMoney),
                Id = congratulatoryMoney.Id,
                Sum = congratulatoryMoney.Sum,
                Details = $"{congratulatoryMoney.GuestName}({congratulatoryMoney.RecognizedText}",
                Created = congratulatoryMoney.Created
            };
        }

        public static StatementItem AsStatementItem(this Spending spending)
        {
            return new StatementItem
            {
                ItemType = typeof(Spending),
                Id = spending.Id,
                Sum = -spending.Sum,
                Details = spending.Details,
                Created = spending.Created
            };
        }
    }
}
