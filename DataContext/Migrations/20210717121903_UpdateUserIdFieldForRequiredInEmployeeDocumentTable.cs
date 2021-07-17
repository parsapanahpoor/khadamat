using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateUserIdFieldForRequiredInEmployeeDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employeeDocument_Userid",
                table: "employeeDocument");

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "employeeDocument",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employeeDocument_Userid",
                table: "employeeDocument",
                column: "Userid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employeeDocument_Userid",
                table: "employeeDocument");

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "employeeDocument",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_employeeDocument_Userid",
                table: "employeeDocument",
                column: "Userid",
                unique: true,
                filter: "[Userid] IS NOT NULL");
        }
    }
}
