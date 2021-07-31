using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateHourReservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HourReservation_DataReservationID",
                table: "HourReservation");

            migrationBuilder.CreateIndex(
                name: "IX_HourReservation_DataReservationID",
                table: "HourReservation",
                column: "DataReservationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HourReservation_DataReservationID",
                table: "HourReservation");

            migrationBuilder.CreateIndex(
                name: "IX_HourReservation_DataReservationID",
                table: "HourReservation",
                column: "DataReservationID",
                unique: true);
        }
    }
}
