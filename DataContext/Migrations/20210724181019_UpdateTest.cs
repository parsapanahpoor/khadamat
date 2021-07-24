using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCategorySelected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategorySelected",
                columns: table => new
                {
                    JobCategorySelectedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategorySelected", x => x.JobCategorySelectedID);
                    table.ForeignKey(
                        name: "FK_JobCategorySelected_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobCategorySelected_jobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "jobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCategorySelected_JobCategoryId",
                table: "JobCategorySelected",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategorySelected_Userid",
                table: "JobCategorySelected",
                column: "Userid");
        }
    }
}
