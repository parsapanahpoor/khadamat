using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateFianncialTransaction2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "FinancialTrnsactions",
                newName: "IsActiveForEmployeePay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActiveForEmployeePay",
                table: "FinancialTrnsactions",
                newName: "IsActive");
        }
    }
}
