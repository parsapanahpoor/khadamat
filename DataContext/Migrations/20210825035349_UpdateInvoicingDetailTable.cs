using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateInvoicingDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicingDetails_jobCategories_JobCategoryID",
                table: "InvoicingDetails");

            migrationBuilder.RenameColumn(
                name: "JobCategoryID",
                table: "InvoicingDetails",
                newName: "TariffID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicingDetails_JobCategoryID",
                table: "InvoicingDetails",
                newName: "IX_InvoicingDetails_TariffID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicingDetails_Tariffs_TariffID",
                table: "InvoicingDetails",
                column: "TariffID",
                principalTable: "Tariffs",
                principalColumn: "TariffId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicingDetails_Tariffs_TariffID",
                table: "InvoicingDetails");

            migrationBuilder.RenameColumn(
                name: "TariffID",
                table: "InvoicingDetails",
                newName: "JobCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicingDetails_TariffID",
                table: "InvoicingDetails",
                newName: "IX_InvoicingDetails_JobCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicingDetails_jobCategories_JobCategoryID",
                table: "InvoicingDetails",
                column: "JobCategoryID",
                principalTable: "jobCategories",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
