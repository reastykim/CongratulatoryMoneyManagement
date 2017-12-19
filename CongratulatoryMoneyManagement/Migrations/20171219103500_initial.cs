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
                name: "MoneyOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Display = table.Column<string>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturnPresents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<uint>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPresents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CongratulatoryMoney",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    EnvelopeImageUri = table.Column<string>(nullable: true),
                    GuestName = table.Column<string>(nullable: true),
                    RecognizedText = table.Column<string>(nullable: true),
                    ReturnPresentId = table.Column<int>(nullable: true),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongratulatoryMoney", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
                        column: x => x.ReturnPresentId,
                        principalTable: "ReturnPresents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CongratulatoryMoney_ReturnPresentId",
                table: "CongratulatoryMoney",
                column: "ReturnPresentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CongratulatoryMoney");

            migrationBuilder.DropTable(
                name: "MoneyOptions");

            migrationBuilder.DropTable(
                name: "Spendings");

            migrationBuilder.DropTable(
                name: "ReturnPresents");
        }
    }
}
