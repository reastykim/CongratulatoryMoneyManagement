using CongratulatoryMoneyManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Data
{
    public class CongratulatoryMoneyContext : DbContext
    {
        public DbSet<CongratulatoryMoney> CongratulatoryMoney { get; set; }

        public DbSet<MoneyOption> MoneyOptions { get; set; }

        public DbSet<ReturnPresent> ReturnPresents { get; set; }

        public DbSet<Spending> Spendings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=congratulatoryMoney.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
