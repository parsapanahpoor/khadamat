using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class InitialUserSelectedJobTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSelectedJobs",
                columns: table => new
                {
                    JobCategorySelectedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSelectedJobs", x => x.JobCategorySelectedID);
                    table.ForeignKey(
                        name: "FK_UserSelectedJobs_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSelectedJobs_jobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "jobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedJobs_JobCategoryId",
                table: "UserSelectedJobs",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedJobs_Userid",
                table: "UserSelectedJobs",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSelectedJobs");
        }
    }
}
