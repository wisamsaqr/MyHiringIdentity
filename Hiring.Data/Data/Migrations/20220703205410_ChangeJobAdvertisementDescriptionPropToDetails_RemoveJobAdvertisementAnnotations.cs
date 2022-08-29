using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiring.Data.Migrations
{
    public partial class ChangeJobAdvertisementDescriptionPropToDetails_RemoveJobAdvertisementAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "JobAdvertisements",
                newName: "Details");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "JobAdvertisements",
                newName: "Description");
        }
    }
}
