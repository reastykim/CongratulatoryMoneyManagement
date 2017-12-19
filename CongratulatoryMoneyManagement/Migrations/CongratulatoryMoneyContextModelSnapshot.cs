using CongratulatoryMoneyManagement.Data;
using CongratulatoryMoneyManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Migrations
{
    [DbContext(typeof(CongratulatoryMoneyContext))]
    partial class CongratulatoryMoneyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CongratulatoryMoneyManagement.Models.CongratulatoryMoney", b =>
            {
                b.Property<int>("CongratulatoryMoneyId")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("Created");

                b.Property<string>("EnvelopeImageUri");

                b.Property<string>("GuestName");

                b.Property<string>("RecognizedText");

                b.Property<int>("ReturnPresentId");

                b.Property<double>("Sum");

                b.HasKey("CongratulatoryMoneyId");

                b.HasIndex("ReturnPresentId");

                b.ToTable("CongratulatoryMoney");
            });

            modelBuilder.Entity("CongratulatoryMoneyManagement.Models.MoneyOption", b =>
            {
                b.Property<int>("MoneyOptionId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Display");

                b.Property<double>("Sum");

                b.HasKey("MoneyOptionId");

                b.ToTable("MoneyOptions");
            });

            modelBuilder.Entity("CongratulatoryMoneyManagement.Models.ReturnPresent", b =>
            {
                b.Property<int>("ReturnPresentId")
                    .ValueGeneratedOnAdd();

                b.Property<uint>("Quantity");

                b.Property<int>("Type");

                b.HasKey("ReturnPresentId");

                b.ToTable("ReturnPresents");
            });

            modelBuilder.Entity("CongratulatoryMoneyManagement.Models.Spending", b =>
            {
                b.Property<int>("SpendingId")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("Created");

                b.Property<string>("Details");

                b.Property<double>("Sum");

                b.HasKey("SpendingId");

                b.ToTable("Spendings");
            });

            modelBuilder.Entity("CongratulatoryMoneyManagement.Models.CongratulatoryMoney", b =>
            {
                b.HasOne("CongratulatoryMoneyManagement.Models.ReturnPresent", "ReturnPresent")
                    .WithMany()
                    .HasForeignKey("ReturnPresentId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}
