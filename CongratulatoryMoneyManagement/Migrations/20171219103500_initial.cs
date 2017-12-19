using CongratulatoryMoneyManagement.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CongratulatoryMoney",
                columns: table => new
                {
                    CongratulatoryMoneyId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Sum = table.Column<double>(nullable: false),
                    GuestName = table.Column<string>(nullable: true),
                    RecognizedText = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    EnvelopeImageUri = table.Column<string>(nullable: true),
                    ReturnPresentId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongratulatoryMoney", x => x.CongratulatoryMoneyId);
                    table.ForeignKey(
                        name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
                        column: x => x.ReturnPresentId,
                        principalTable: "ReturnPresents",
                        principalColumn: "ReturnPresentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnPresents",
                columns: table => new
                {
                    ReturnPresentId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<ReturnPresentType>(nullable: false),
                    Quantity = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPresents", x => x.ReturnPresentId);
                });

            migrationBuilder.CreateTable(
                name: "MoneyOptions",
                columns: table => new
                {
                    MoneyOptionId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Display = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOptions", x => x.MoneyOptionId);
                });

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    SpendingId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Details = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.SpendingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CongratulatoryMoney");

            migrationBuilder.DropTable(
                name: "ReturnPresents");

            migrationBuilder.DropTable(
                name: "MoneyOptions");

            migrationBuilder.DropTable(
                name: "Spendings");
        }
    }
}
