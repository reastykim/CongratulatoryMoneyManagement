using CongratulatoryMoneyManagement.Helpers;
using CongratulatoryMoneyManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CongratulatoryMoneyManagement.Data
{
    public class CongratulatoryMoneyContext : DbContext
    {
        const string OldMainDbFileName = "congratulatoryMoney.db";
        const string MainDbFileName = "new_congratulatoryMoney.db";

        #region Properties

        public DbSet<CongratulatoryMoney> CongratulatoryMoney
        {
            get
            {
                return congratulatoryMoney;
            }

            set
            {
                congratulatoryMoney = value;
            }
        }
        private DbSet<CongratulatoryMoney> congratulatoryMoney;
        
        public DbSet<MoneyOption> MoneyOptions
        {
            get
            {
                return moneyOptions;
            }

            set
            {
                moneyOptions = value;
            }
        }
        private DbSet<MoneyOption> moneyOptions;

        public DbSet<ReturnPresent> ReturnPresents
        {
            get
            {
                return returnPresents;
            }

            set
            {
                returnPresents = value;
            }
        }
        private DbSet<ReturnPresent> returnPresents;

        public DbSet<Spending> Spendings
        {
            get
            {
                return spendings;
            }

            set
            {
                spendings = value;
            }
        }
        private DbSet<Spending> spendings;

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={MainDbFileName}");
        }

        public static void CheckMigrations()
        {
            using (var db = new CongratulatoryMoneyContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
