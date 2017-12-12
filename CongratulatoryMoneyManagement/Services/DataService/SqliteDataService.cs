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
using CongratulatoryMoneyManagement.Extensions;
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
            CongratulatoryMoneyContext.CheckMigrations();

            var allMoneyOptions = await AllMoneyOptionsAsync();
            if (allMoneyOptions.Count() == 0)
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
                var result = db.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS 'MoneyOptions' ('Id' INTEGER PRIMARY KEY ASC, 'Display' TEXT, 'Sum' INTEGER, 'IsSelected' INTEGER)");

                result = db.SaveChanges();
                return result;
            }
        }

        private int CreateReturnPresentsTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                var result = db.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS 'ReturnPresents' ('Id' INTEGER PRIMARY KEY ASC, 'Type' INTEGER, 'Quantity' INTEGER)");

                result = db.SaveChanges();
                return result;
            }
        }

        private int CreateCongratulatoryMoneyTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                var result = db.Database.ExecuteSqlCommand(
                    "CREATE TABLE IF NOT EXISTS 'CongratulatoryMoney' ('Id' INTEGER PRIMARY KEY ASC, 'Sum' INTEGER, 'GuestName' TEXT, 'RecognizedText' TEXT, 'EnvelopeImageUri' TEXT, 'ReturnPresentId' INTEGER NOT NULL, 'Created' datetime, " +
                    "FOREIGN KEY(ReturnPresentId) REFERENCES ReturnPresents(Id))");

                result = db.SaveChanges();
                return result;
            }
        }

        private int CreateSpendingsTableIfNotExists()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                var result = db.Database.ExecuteSqlCommand(
                    "CREATE TABLE IF NOT EXISTS 'Spendings' ('Id' INTEGER PRIMARY KEY ASC, 'Details' TEXT, 'Sum' INTEGER, 'Created' datetime)");

                result = db.SaveChanges();
                return result;
            }
        }

        private void DropAllTables()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS 'MoneyOptions'");
                db.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS 'ReturnPresents'");
                db.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS 'CongratulatoryMoney'");
                db.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS 'Spendings'");

                db.SaveChanges();
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

        public async Task<IEnumerable<MoneyOption>> AllMoneyOptionsAsync()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                return await db.MoneyOptions.ToListAsync();
            }
        }

        public Task<int> SaveCongratulatoryMoneyAsync(CongratulatoryMoney item)
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.CongratulatoryMoney.Add(item);
                return db.SaveChangesAsync();
            }
        }

        public Task<int> SaveSpendingAsync(Spending item)
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.Spendings.Add(item);
                return db.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<StatementItem>> GetAllStatementAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<StatementItem>>();
            Task.Factory.StartNew(() =>
            {
                using (var db = new CongratulatoryMoneyContext())
                {
                    var statementItems = db.CongratulatoryMoney.Select(CM => CM.AsStatementItem())
                                        .Union(db.Spendings.Select(S => S.AsStatementItem()))
                                        .OrderByDescending(SI => SI.Created);

                    tcs.SetResult(statementItems.ToList());
                }
            });

            return tcs.Task;
        }
    }
}
