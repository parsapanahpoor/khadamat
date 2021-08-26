using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateInvoivingTavle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Invoicings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_LocationID",
                table: "Invoicings",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoicings_locations_LocationID",
                table: "Invoicings",
                column: "LocationID",
                principalTable: "locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoicings_locations_LocationID",
                table: "Invoicings");

            migrationBuilder.DropIndex(
                name: "IX_Invoicings_LocationID",
                table: "Invoicings");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Invoicings");
        }
    }
}
