using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sdakcc.Migrations
{
    public partial class UpdatedLikesFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_AbpUsers_UsersId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UsersId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Like");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Like",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_UsersId",
                table: "Like",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AbpUsers_UsersId",
                table: "Like",
                column: "UsersId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
