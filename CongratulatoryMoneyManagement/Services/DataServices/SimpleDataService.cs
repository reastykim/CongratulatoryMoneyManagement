﻿using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.Services.DataServices
{
    public class SimpleDataService : IDataService
    {
        private List<CongratulatoryMoney> congratulatoryMoneyList = new List<CongratulatoryMoney>();
        public SimpleDataService()
        {

        }

        public IReadOnlyList<MoneyOption> AllMoneyOptions()
        {
            return new List<MoneyOption>
            {
                new MoneyOption(0, "MoneyOption_Input".GetLocalized(), true),
                new MoneyOption(30000),
                new MoneyOption(50000),
                new MoneyOption(70000),
                new MoneyOption(100000),
                new MoneyOption(150000),
                new MoneyOption(200000),
            };
        }

        public void SaveCongratulatoryMoney(CongratulatoryMoney item)
        {
            congratulatoryMoneyList.Add(item);
        }

        private IEnumerable<SampleOrder> AllOrders()
        {
            // The following is order summary data
            var data = new ObservableCollection<SampleOrder>
            {
                new SampleOrder
                {
                    OrderId = 70,
                    OrderDate = new DateTime(2017, 05, 24),
                    Company = "Company F",
                    ShipTo = "Francisco Pérez-Olaeta",
                    OrderTotal = 2490.00,
                    Status = "Closed",
                    Symbol = Symbol.Globe
                },
                new SampleOrder
                {
                    OrderId = 71,
                    OrderDate = new DateTime(2017, 05, 24),
                    Company = "Company CC",
                    ShipTo = "Soo Jung Lee",
                    OrderTotal = 1760.00,
                    Status = "Closed",
                    Symbol = Symbol.Audio
                },
                new SampleOrder
                {
                    OrderId = 72,
                    OrderDate = new DateTime(2017, 06, 03),
                    Company = "Company Z",
                    ShipTo = "Run Liu",
                    OrderTotal = 2310.00,
                    Status = "Closed",
                    Symbol = Symbol.Calendar
                },
                new SampleOrder
                {
                    OrderId = 73,
                    OrderDate = new DateTime(2017, 06, 05),
                    Company = "Company Y",
                    ShipTo = "John Rodman",
                    OrderTotal = 665.00,
                    Status = "Closed",
                    Symbol = Symbol.Camera
                },
                new SampleOrder
                {
                    OrderId = 74,
                    OrderDate = new DateTime(2017, 06, 07),
                    Company = "Company H",
                    ShipTo = "Elizabeth Andersen",
                    OrderTotal = 560.00,
                    Status = "Shipped",
                    Symbol = Symbol.Clock
                },
                new SampleOrder
                {
                    OrderId = 75,
                    OrderDate = new DateTime(2017, 06, 07),
                    Company = "Company F",
                    ShipTo = "Francisco Pérez-Olaeta",
                    OrderTotal = 810.00,
                    Status = "Shipped",
                    Symbol = Symbol.Contact
                },
                new SampleOrder
                {
                    OrderId = 76,
                    OrderDate = new DateTime(2017, 06, 11),
                    Company = "Company I",
                    ShipTo = "Sven Mortensen",
                    OrderTotal = 196.50,
                    Status = "Shipped",
                    Symbol = Symbol.Favorite
                },
                new SampleOrder
                {
                    OrderId = 77,
                    OrderDate = new DateTime(2017, 06, 14),
                    Company = "Company BB",
                    ShipTo = "Amritansh Raghav",
                    OrderTotal = 270.00,
                    Status = "New",
                    Symbol = Symbol.Home
                },
                new SampleOrder
                {
                    OrderId = 78,
                    OrderDate = new DateTime(2017, 06, 14),
                    Company = "Company A",
                    ShipTo = "Anna Bedecs",
                    OrderTotal = 736.00,
                    Status = "New",
                    Symbol = Symbol.Mail
                },
                new SampleOrder
                {
                    OrderId = 79,
                    OrderDate = new DateTime(2017, 06, 18),
                    Company = "Company K",
                    ShipTo = "Peter Krschne",
                    OrderTotal = 800.00,
                    Status = "New",
                    Symbol = Symbol.OutlineStar
                },
            };

            return data;
        }

        // TODO WTS: Remove this once your grid page is displaying real data
        public ObservableCollection<SampleOrder> GetGridSampleData()
        {
            return new ObservableCollection<SampleOrder>(AllOrders());
        }
    }
}
