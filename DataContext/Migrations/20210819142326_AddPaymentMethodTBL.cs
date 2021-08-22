using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddPaymentMethodTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoicings",
                columns: table => new
                {
                    InvoicingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationOrderID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobCategoryID = table.Column<int>(type: "int", nullable: false),
                    HoureReservationID = table.Column<int>(type: "int", nullable: true),
                    DateReservationID = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsFinaly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoicings", x => x.InvoicingID);
                    table.ForeignKey(
                        name: "FK_Invoicings_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoicings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoicings_DataReservation_DateReservationID",
                        column: x => x.DateReservationID,
                        principalTable: "DataReservation",
                        principalColumn: "DataReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoicings_HourReservation_HoureReservationID",
                        column: x => x.HoureReservationID,
                        principalTable: "HourReservation",
                        principalColumn: "HourReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoicings_jobCategories_JobCategoryID",
                        column: x => x.JobCategoryID,
                        principalTable: "jobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoicings_ReservationOrders_ReservationOrderID",
                        column: x => x.ReservationOrderID,
                        principalTable: "ReservationOrders",
                        principalColumn: "ReservationOrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    MethodTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "InvoicingDetails",
                columns: table => new
                {
                    InvoicingDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoicingID = table.Column<int>(type: "int", nullable: false),
                    JobCategoryID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerCent = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicingDetails", x => x.InvoicingDetailID);
                    table.ForeignKey(
                        name: "FK_InvoicingDetails_Invoicings_InvoicingID",
                        column: x => x.InvoicingID,
                        principalTable: "Invoicings",
                        principalColumn: "InvoicingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoicingDetails_jobCategories_JobCategoryID",
                        column: x => x.JobCategoryID,
                        principalTable: "jobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicingDetails_InvoicingID",
                table: "InvoicingDetails",
                column: "InvoicingID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicingDetails_JobCategoryID",
                table: "InvoicingDetails",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_DateReservationID",
                table: "Invoicings",
                column: "DateReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_EmployeeID",
                table: "Invoicings",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_HoureReservationID",
                table: "Invoicings",
                column: "HoureReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_JobCategoryID",
                table: "Invoicings",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_ReservationOrderID",
                table: "Invoicings",
                column: "ReservationOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoicings_UserID",
                table: "Invoicings",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicingDetails");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Invoicings");
        }
    }
}
