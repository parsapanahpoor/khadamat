using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class InitialRelationuseEmployeeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeStatusID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeStatusID",
                table: "AspNetUsers",
                column: "EmployeeStatusID",
                unique: true,
                filter: "[EmployeeStatusID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmployeeStatuses_EmployeeStatusID",
                table: "AspNetUsers",
                column: "EmployeeStatusID",
                principalTable: "EmployeeStatuses",
                principalColumn: "EmployeeStatusID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmployeeStatuses_EmployeeStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeStatusID",
                table: "AspNetUsers");
        }
    }
}
