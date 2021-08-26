using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateInvoicing3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Invoicings",
                newName: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_PaymentMethodID",
                table: "Invoicings",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoicings_PaymentMethods_PaymentMethodID",
                table: "Invoicings",
                column: "PaymentMethodID",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoicings_PaymentMethods_PaymentMethodID",
                table: "Invoicings");

            migrationBuilder.DropIndex(
                name: "IX_Invoicings_PaymentMethodID",
                table: "Invoicings");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodID",
                table: "Invoicings",
                newName: "PaymentMethod");
        }
    }
}
