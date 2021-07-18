using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class InitialEmployeeInfoPossitionForSecondeTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeeInformationPossitions",
                columns: table => new
                {
                    PossitionId = table.Column<int>(type: "int", nullable: false),
                    PossitionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeInformationPossitions", x => x.PossitionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeInformationPossitions");
        }
    }
}
