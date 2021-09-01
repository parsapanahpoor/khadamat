using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddRequestForCheckout3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "RequestForCheckout",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForCheckout_EmployeeID",
                table: "RequestForCheckout",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForCheckout_AspNetUsers_EmployeeID",
                table: "RequestForCheckout",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForCheckout_AspNetUsers_EmployeeID",
                table: "RequestForCheckout");

            migrationBuilder.DropIndex(
                name: "IX_RequestForCheckout_EmployeeID",
                table: "RequestForCheckout");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "RequestForCheckout");
        }
    }
}
