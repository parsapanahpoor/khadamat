using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class RalationBetweenEmployeeAndEmployeeInfoPossition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PossitionId",
                table: "employeeDocument",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_employeeDocument_PossitionId",
                table: "employeeDocument",
                column: "PossitionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employeeDocument_employeeInformationPossitions_PossitionId",
                table: "employeeDocument",
                column: "PossitionId",
                principalTable: "employeeInformationPossitions",
                principalColumn: "PossitionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeDocument_employeeInformationPossitions_PossitionId",
                table: "employeeDocument");

            migrationBuilder.DropIndex(
                name: "IX_employeeDocument_PossitionId",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "PossitionId",
                table: "employeeDocument");
        }
    }
}
