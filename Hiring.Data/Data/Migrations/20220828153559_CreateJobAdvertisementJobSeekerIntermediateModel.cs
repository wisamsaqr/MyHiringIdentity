using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiring.Data.Migrations
{
    public partial class CreateJobAdvertisementJobSeekerIntermediateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobAdvertisementJobSeekers",
                columns: table => new
                {
                    JobAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvertisementJobSeekers", x => new { x.JobAdvertisementId, x.JobSeekerId });
                    table.ForeignKey(
                        name: "FK_JobAdvertisementJobSeekers_JobAdvertisements_JobAdvertisementId",
                        column: x => x.JobAdvertisementId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAdvertisementJobSeekers_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisementJobSeekers_JobSeekerId",
                table: "JobAdvertisementJobSeekers",
                column: "JobSeekerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAdvertisementJobSeekers");
        }
    }
}
