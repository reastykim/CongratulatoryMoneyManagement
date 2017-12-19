using CongratulatoryMoneyManagement.Data;
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
			modelBuilder
				.HasAnnotation("ProductVersion", "1.1.0");

			modelBuilder.Entity("CongratulatoryMoneyManagement.Models.CongratulatoryMoney", b =>
			{
				b.Property<int>("CongratulatoryMoneyId")
					.ValueGeneratedOnAdd();

				b.Property<double>("Sum");

				b.Property<string>("GuestName");

				b.Property<string>("RecognizedText");

				b.Property<DateTime>("Created");

				b.Property<string>("EnvelopeImageUri");

				b.Property<int>("ReturnPresentId");

				b.HasOne("CongratulatoryMoneyManagement.Models.ReturnPresent");

				b.HasKey("CongratulatoryMoneyId");

				b.ToTable("CongratulatoryMoney");
			});


			modelBuilder.Entity("CongratulatoryMoneyManagement.Models.ReturnPresent", b =>
			{
				b.Property<int>("ReturnPresentId")
					.ValueGeneratedOnAdd();

				b.Property<ReturnPresentType>("Type");

				b.Property<uint>("Quantity");

				b.HasKey("ReturnPresentId");

				b.ToTable("ReturnPresents");
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

			modelBuilder.Entity("CongratulatoryMoneyManagement.Models.Spending", b =>
			{
				b.Property<int>("SpendingId")
					.ValueGeneratedOnAdd();

				b.Property<string>("Details");

				b.Property<double>("Sum");

				b.Property<DateTime>("Created");

				b.HasKey("SpendingId");

				b.ToTable("Spendings");
			});
		}
	}
}
