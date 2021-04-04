using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class fix7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<long>>(
                name: "LinkIds",
                table: "Users",
                type: "bigint[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Links",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId1",
                table: "Links",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_UserModelId1",
                table: "Links",
                column: "UserModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Users_UserModelId1",
                table: "Links",
                column: "UserModelId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Users_UserModelId1",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_UserModelId1",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "LinkIds",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "UserModelId1",
                table: "Links");
        }
    }
}
