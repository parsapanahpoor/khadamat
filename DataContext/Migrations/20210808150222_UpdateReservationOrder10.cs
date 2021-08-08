using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateReservationOrder10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "DateReservationID",
            table: "ReservationOrders",
            type: "int",
            nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HoureReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_DateReservationID",
                table: "ReservationOrders",
                column: "DateReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_HoureReservationID",
                table: "ReservationOrders",
                column: "HoureReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationOrders_DataReservation_DateReservationID",
                table: "ReservationOrders",
                column: "DateReservationID",
                principalTable: "DataReservation",
                principalColumn: "DataReservationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationOrders_HourReservation_HoureReservationID",
                table: "ReservationOrders",
                column: "HoureReservationID",
                principalTable: "HourReservation",
                principalColumn: "HourReservationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
