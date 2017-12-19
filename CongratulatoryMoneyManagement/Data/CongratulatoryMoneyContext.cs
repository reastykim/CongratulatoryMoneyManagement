﻿using CongratulatoryMoneyManagement.Helpers;
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

        private static AsyncInitilizer<CongratulatoryMoneyContext> initializer = new AsyncInitilizer<CongratulatoryMoneyContext>();

        #region Properties

        public DbSet<CongratulatoryMoney> CongratulatoryMoney
        {
            get
            {
                initializer.CheckInitialized();
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
                initializer.CheckInitialized();
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
                initializer.CheckInitialized();
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
                initializer.CheckInitialized();
                return spendings;
            }

            set
            {
                spendings = value;
            }
        }
        private DbSet<Spending> spendings;

        #endregion

        #region Constructors

        static CongratulatoryMoneyContext()
        {
            initializer.InitializeWith(CheckForDatabase);
        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={MainDbFileName}");
        }

        private static async Task CheckForDatabase()
        {
            var data = Windows.Storage.ApplicationData.Current.LocalFolder;

            var exists = await data.TryGetItemAsync(OldMainDbFileName);

            if (exists != null) // Renamined db file name for 1.0.0 version app.
            {
                await exists.RenameAsync(OldMainDbFileName + ".bak");
            }

            // Commentted, Because the table was made by Migration code
            //if (exists == null)
            //{
            //    var mainDbAssetPath = $"ms-appx:///Assets/{OldMainDbFileName}";
            //    var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(mainDbAssetPath)).AsTask().ConfigureAwait(false);
            //    var database = await file.CopyAsync(data).AsTask().ConfigureAwait(false);
            //}
        }

        public static void CheckMigrations()
        {
            initializer.CheckInitialized();
            using (var db = new CongratulatoryMoneyContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
