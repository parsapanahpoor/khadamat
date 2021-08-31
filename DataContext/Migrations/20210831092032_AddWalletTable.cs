using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddWalletTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminWallet",
                columns: table => new
                {
                    AdminWalletID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebtAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminWallet", x => x.AdminWalletID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWallet",
                columns: table => new
                {
                    EmployeeWalletID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebtAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWallet", x => x.EmployeeWalletID);
                    table.ForeignKey(
                        name: "FK_EmployeeWallet_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTrnsactions",
                columns: table => new
                {
                    FinancialTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialTransactionStatusID = table.Column<int>(type: "int", nullable: false),
                    InvoicingId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BankRecepiet = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BankTransferNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReciverPerson = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DepositeFromPerson = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTrnsactions", x => x.FinancialTransactionID);
                    table.ForeignKey(
                        name: "FK_FinancialTrnsactions_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTrnsactions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTrnsactions_FinancialTransactionStatus_FinancialTransactionStatusID",
                        column: x => x.FinancialTransactionStatusID,
                        principalTable: "FinancialTransactionStatus",
                        principalColumn: "FinancialTransactionStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTrnsactions_Invoicings_InvoicingId",
                        column: x => x.InvoicingId,
                        principalTable: "Invoicings",
                        principalColumn: "InvoicingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWallet_EmployeeId",
                table: "EmployeeWallet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTrnsactions_EmployeeID",
                table: "FinancialTrnsactions",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTrnsactions_FinancialTransactionStatusID",
                table: "FinancialTrnsactions",
                column: "FinancialTransactionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTrnsactions_InvoicingId",
                table: "FinancialTrnsactions",
                column: "InvoicingId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTrnsactions_UserID",
                table: "FinancialTrnsactions",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminWallet");

            migrationBuilder.DropTable(
                name: "EmployeeWallet");

            migrationBuilder.DropTable(
                name: "FinancialTrnsactions");
        }
    }
}
