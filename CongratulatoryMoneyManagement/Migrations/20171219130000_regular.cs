using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Migrations
{
    public partial class regular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CongratulatoryMoney");

            migrationBuilder.DropTable(
                name: "MoneyOptions");

            migrationBuilder.DropTable(
                name: "Spendings");

            migrationBuilder.DropTable(
                name: "ReturnPresents");

            migrationBuilder.CreateTable(
                name: "MoneyOptions",
                columns: table => new
                {
                    MoneyOptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Display = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOptions", x => x.MoneyOptionId);
                });

            migrationBuilder.CreateTable(
                name: "ReturnPresents",
                columns: table => new
                {
                    ReturnPresentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<uint>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPresents", x => x.ReturnPresentId);
                });

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    SpendingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.SpendingId);
                });

            migrationBuilder.CreateTable(
                name: "CongratulatoryMoney",
                columns: table => new
                {
                    CongratulatoryMoneyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    EnvelopeImageUri = table.Column<string>(nullable: true),
                    GuestName = table.Column<string>(nullable: true),
                    RecognizedText = table.Column<string>(nullable: true),
                    ReturnPresentId = table.Column<int>(nullable: false),
                    Sum = table.Column<double>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CongratulatoryMoney_ReturnPresentId",
                table: "CongratulatoryMoney",
                column: "ReturnPresentId");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
            //    table: "CongratulatoryMoney");

            //migrationBuilder.DropColumn(
            //    name: "IsSelected",
            //    table: "MoneyOptions");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "Spendings",
            //    newName: "SpendingId");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "ReturnPresents",
            //    newName: "ReturnPresentId");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "MoneyOptions",
            //    newName: "MoneyOptionId");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "CongratulatoryMoney",
            //    newName: "CongratulatoryMoneyId");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ReturnPresentId",
            //    table: "CongratulatoryMoney",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
            //    table: "CongratulatoryMoney",
            //    column: "ReturnPresentId",
            //    principalTable: "ReturnPresents",
            //    principalColumn: "ReturnPresentId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
            //    table: "CongratulatoryMoney");

            //migrationBuilder.RenameColumn(
            //    name: "SpendingId",
            //    table: "Spendings",
            //    newName: "Id");

            //migrationBuilder.RenameColumn(
            //    name: "ReturnPresentId",
            //    table: "ReturnPresents",
            //    newName: "Id");

            //migrationBuilder.RenameColumn(
            //    name: "MoneyOptionId",
            //    table: "MoneyOptions",
            //    newName: "Id");

            //migrationBuilder.RenameColumn(
            //    name: "CongratulatoryMoneyId",
            //    table: "CongratulatoryMoney",
            //    newName: "Id");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsSelected",
            //    table: "MoneyOptions",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ReturnPresentId",
            //    table: "CongratulatoryMoney",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CongratulatoryMoney_ReturnPresents_ReturnPresentId",
            //    table: "CongratulatoryMoney",
            //    column: "ReturnPresentId",
            //    principalTable: "ReturnPresents",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
