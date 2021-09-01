using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddRequestForCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestForCheckoutStatus",
                columns: table => new
                {
                    RequestForCheckoutStatusID = table.Column<int>(type: "int", nullable: false),
                    StatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForCheckoutStatus", x => x.RequestForCheckoutStatusID);
                });

            migrationBuilder.CreateTable(
                name: "RequestForCheckout",
                columns: table => new
                {
                    RequestForCheckoutID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestForCheckoutStatusID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartBankNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerBankCart = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForCheckout", x => x.RequestForCheckoutID);
                    table.ForeignKey(
                        name: "FK_RequestForCheckout_RequestForCheckoutStatus_RequestForCheckoutStatusID",
                        column: x => x.RequestForCheckoutStatusID,
                        principalTable: "RequestForCheckoutStatus",
                        principalColumn: "RequestForCheckoutStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestForCheckout_RequestForCheckoutStatusID",
                table: "RequestForCheckout",
                column: "RequestForCheckoutStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestForCheckout");

            migrationBuilder.DropTable(
                name: "RequestForCheckoutStatus");
        }
    }
}
