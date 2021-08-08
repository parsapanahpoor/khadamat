using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateReservationOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationOrders_DataReservation_DateReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationOrders_HourReservation_HoureReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReservationOrders_DateReservationID",
                table: "ReservationOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReservationOrders_HoureReservationID",
                table: "ReservationOrders");

            migrationBuilder.AddColumn<int>(
                name: "DataReservationID",
                table: "ReservationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HourReservationID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "HourReservationID",
                table: "ReservationOrders");

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
    }
}
