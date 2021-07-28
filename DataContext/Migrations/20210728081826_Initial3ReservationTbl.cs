using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class Initial3ReservationTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataReservation",
                columns: table => new
                {
                    DataReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataReservation", x => x.DataReservationID);
                    table.ForeignKey(
                        name: "FK_DataReservation_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatus",
                columns: table => new
                {
                    ReservationStatusID = table.Column<int>(type: "int", nullable: false),
                    PersianStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatus", x => x.ReservationStatusID);
                });

            migrationBuilder.CreateTable(
                name: "HourReservation",
                columns: table => new
                {
                    HourReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartHourReservation = table.Column<int>(type: "int", nullable: false),
                    EndHourReservation = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationStatusID = table.Column<int>(type: "int", nullable: false),
                    DataReservationID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourReservation", x => x.HourReservationID);
                    table.ForeignKey(
                        name: "FK_HourReservation_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HourReservation_DataReservation_DataReservationID",
                        column: x => x.DataReservationID,
                        principalTable: "DataReservation",
                        principalColumn: "DataReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HourReservation_ReservationStatus_ReservationStatusID",
                        column: x => x.ReservationStatusID,
                        principalTable: "ReservationStatus",
                        principalColumn: "ReservationStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataReservation_EmployeeID",
                table: "DataReservation",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_HourReservation_DataReservationID",
                table: "HourReservation",
                column: "DataReservationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HourReservation_EmployeeID",
                table: "HourReservation",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_HourReservation_ReservationStatusID",
                table: "HourReservation",
                column: "ReservationStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourReservation");

            migrationBuilder.DropTable(
                name: "DataReservation");

            migrationBuilder.DropTable(
                name: "ReservationStatus");
        }
    }
}
