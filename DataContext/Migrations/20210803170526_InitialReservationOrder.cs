using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class InitialReservationOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationOrders",
                columns: table => new
                {
                    ReservationOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserReservationStatus = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobCategoryID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    HoureReservationID = table.Column<int>(type: "int", nullable: false),
                    DateReservationID = table.Column<int>(type: "int", nullable: false),
                    DateTimeReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationOrders", x => x.ReservationOrderID);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_DataReservation_DateReservationID",
                        column: x => x.DateReservationID,
                        principalTable: "DataReservation",
                        principalColumn: "DataReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_HourReservation_HoureReservationID",
                        column: x => x.HoureReservationID,
                        principalTable: "HourReservation",
                        principalColumn: "HourReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_jobCategories_JobCategoryID",
                        column: x => x.JobCategoryID,
                        principalTable: "jobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationOrders_UserReserveStatus_UserReservationStatus",
                        column: x => x.UserReservationStatus,
                        principalTable: "UserReserveStatus",
                        principalColumn: "UserReservationStatus",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_DateReservationID",
                table: "ReservationOrders",
                column: "DateReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_EmployeeID",
                table: "ReservationOrders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_HoureReservationID",
                table: "ReservationOrders",
                column: "HoureReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_JobCategoryID",
                table: "ReservationOrders",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_LocationID",
                table: "ReservationOrders",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_UserID",
                table: "ReservationOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOrders_UserReservationStatus",
                table: "ReservationOrders",
                column: "UserReservationStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationOrders");
        }
    }
}
