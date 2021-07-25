using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddResumeFieldToTheUserSelectedJobTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResumeDescription",
                table: "UserSelectedJobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAvatar",
                table: "UserSelectedJobs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeDescription",
                table: "UserSelectedJobs");

            migrationBuilder.DropColumn(
                name: "UserAvatar",
                table: "UserSelectedJobs");
        }
    }
}
