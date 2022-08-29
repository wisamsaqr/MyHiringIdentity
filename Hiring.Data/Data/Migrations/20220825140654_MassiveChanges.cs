using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiring.Data.Migrations
{
    public partial class MassiveChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foundations_AspNetUsers_UserId",
                table: "Foundations");

            migrationBuilder.DropIndex(
                name: "IX_Foundations_UserId",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Foundations");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Foundations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Foundations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FoundationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FoundationId",
                table: "AspNetUsers",
                column: "FoundationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Foundations_FoundationId",
                table: "AspNetUsers",
                column: "FoundationId",
                principalTable: "Foundations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Foundations_FoundationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FoundationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "FoundationId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Foundations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_UserId",
                table: "Foundations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foundations_AspNetUsers_UserId",
                table: "Foundations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
