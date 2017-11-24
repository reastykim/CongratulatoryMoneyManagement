using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using CongratulatoryMoneyManagement.Data;
using Microsoft.EntityFrameworkCore;
using Windows.Globalization;
using Windows.System.UserProfile;

namespace CongratulatoryMoneyManagement.Services.DataService
{
    public class SqliteDataService : IDataService
    {
        public SqliteDataService()
        {
            Initialize();
        }

        private async void Initialize()
        {
            CreateMoneyOptionsTableIfNotExists();
            CreateReturnPresentsTableIfNotExists();
            CreateCongratulatoryMoneyTableIfNotExists();

            using (var db = new CongratulatoryMoneyContext())
            {
                db.Database.Migrate();
            }

            var allMoneyOptions = await AllMoneyOptionsAsync();
            if (allMoneyOptions.Count == 0)
            {
                switch (GlobalizationPreferences.Languages[0].ToUpper())
                {
                    case "EN":
                        AddDefaultUSMoneyOptions();
                        break;
                    case "KO":
                        AddDefaultKoreaMoneyOptions();
                        break;
                }
            }
        }

        private int CreateMoneyOptionsTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                //db.Database.ExecuteSqlCommand("DROP TABLE 'MoneyOptions'");
                var result = db.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS 'MoneyOptions' ('Id' INTEGER PRIMARY KEY ASC, 'Display' TEXT, 'Sum' INTEGER, 'IsSelected' INTEGER)");

                result = db.SaveChanges();
                return result;
            }
        }

        private int CreateReturnPresentsTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                //db.Database.ExecuteSqlCommand("DROP TABLE 'ReturnPresents'");
                var result = db.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS 'ReturnPresents' ('Id' INTEGER PRIMARY KEY ASC, 'Type' INTEGER, 'Quantity' INTEGER)");

                result = db.SaveChanges();
                return result;
            }
        }

        private int CreateCongratulatoryMoneyTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                //db.Database.ExecuteSqlCommand("DROP TABLE 'CongratulatoryMoney'");
                var result = db.Database.ExecuteSqlCommand(
                    "CREATE TABLE IF NOT EXISTS 'CongratulatoryMoney' ('Id' INTEGER PRIMARY KEY ASC, 'Sum' INTEGER, 'GuestName' TEXT, 'EnvelopeImageUri' TEXT, 'ReturnPresentId' INTEGER NOT NULL," +
                    "FOREIGN KEY(ReturnPresentId) REFERENCES ReturnPresents(Id))");

                result = db.SaveChanges();
                return result;
            }
        }

        private void AddDefaultKoreaMoneyOptions()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.MoneyOptions.Add(new MoneyOption(0, "MoneyOption_Input".GetLocalized(), true));
                db.MoneyOptions.Add(new MoneyOption(30000));
                db.MoneyOptions.Add(new MoneyOption(50000));
                db.MoneyOptions.Add(new MoneyOption(70000));
                db.MoneyOptions.Add(new MoneyOption(100000));
                db.MoneyOptions.Add(new MoneyOption(150000));
                db.MoneyOptions.Add(new MoneyOption(200000));
                db.SaveChanges();
            }
        }

        private void AddDefaultUSMoneyOptions()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.MoneyOptions.Add(new MoneyOption(0, "MoneyOption_Input".GetLocalized(), true));
                db.MoneyOptions.Add(new MoneyOption(30000));
                db.MoneyOptions.Add(new MoneyOption(50000));
                db.MoneyOptions.Add(new MoneyOption(70000));
                db.MoneyOptions.Add(new MoneyOption(100000));
                db.MoneyOptions.Add(new MoneyOption(150000));
                db.MoneyOptions.Add(new MoneyOption(200000));
                db.SaveChanges();
            }
        }

        public Task<IReadOnlyList<MoneyOption>> AllMoneyOptionsAsync()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                return db.MoneyOptions.ToListAsync().ContinueWith(t => (IReadOnlyList <MoneyOption>)t.Result);
            }
        }

        public Task SaveCongratulatoryMoneyAsync(CongratulatoryMoney item)
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.CongratulatoryMoney.Add(item);
                return db.SaveChangesAsync();
            }
        }

        public Task<IReadOnlyList<CongratulatoryMoney>> AllCongratulatoryMoneyAsync()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                return db.CongratulatoryMoney.ToListAsync().ContinueWith(t => (IReadOnlyList<CongratulatoryMoney>)t.Result);
            }
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
