using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class fix9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Links",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_UserId1",
                table: "Links",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Users_UserId1",
                table: "Links",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Users_UserId1",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_UserId1",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Links");
        }
    }
}
