﻿using CongratulatoryMoneyManagement.Data;
using CongratulatoryMoneyManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Migrations
{
	[DbContext(typeof(CongratulatoryMoneyContext))]
	[Migration("20171219103500_initial")]
	partial class initial
	{
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CongratulatoryMoneyContext.Models.CongratulatoryMoney", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("Created");

                b.Property<string>("EnvelopeImageUri");

                b.Property<string>("GuestName");

                b.Property<string>("RecognizedText");

                b.Property<int?>("ReturnPresentId");

                b.Property<double>("Sum");

                b.HasKey("Id");

                b.HasIndex("ReturnPresentId");

                b.ToTable("CongratulatoryMoney");
            });

            modelBuilder.Entity("CongratulatoryMoneyContext.Models.MoneyOption", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Display");

                b.Property<bool>("IsSelected");

                b.Property<double>("Sum");

                b.HasKey("Id");

                b.ToTable("MoneyOptions");
            });

            modelBuilder.Entity("CongratulatoryMoneyContext.Models.ReturnPresent", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<uint>("Quantity");

                b.Property<int>("Type");

                b.HasKey("Id");

                b.ToTable("ReturnPresents");
            });

            modelBuilder.Entity("CongratulatoryMoneyContext.Models.Spending", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("Created");

                b.Property<string>("Details");

                b.Property<double>("Sum");

                b.HasKey("Id");

                b.ToTable("Spendings");
            });

            modelBuilder.Entity("CongratulatoryMoneyContext.Models.CongratulatoryMoney", b =>
            {
                b.HasOne("WebApplication1.Models.ReturnPresent", "ReturnPresent")
                    .WithMany()
                    .HasForeignKey("ReturnPresentId");
            });
#pragma warning restore 612, 618
        }
    }
}