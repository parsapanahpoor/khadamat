using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateReservationOrder8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationOrders_DataReservation_DataReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationOrders_HourReservation_HourReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReservationOrders_DataReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReservationOrders_HourReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropColumn(
                name: "DataReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropColumn(
                name: "DateReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropColumn(
                name: "HourReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropColumn(
                name: "HoureReservationID",
                table: "ReservationOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DateReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HourReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HoureReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_DataReservationID",
                table: "ReservationOrders",
                column: "DataReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_HourReservationID",
                table: "ReservationOrders",
                column: "HourReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationOrders_DataReservation_DataReservationID",
                table: "ReservationOrders",
                column: "DataReservationID",
                principalTable: "DataReservation",
                principalColumn: "DataReservationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationOrders_HourReservation_HourReservationID",
                table: "ReservationOrders",
                column: "HourReservationID",
                principalTable: "HourReservation",
                principalColumn: "HourReservationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
