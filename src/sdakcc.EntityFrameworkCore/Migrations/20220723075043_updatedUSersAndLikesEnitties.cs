using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sdakcc.Migrations
{
    public partial class updatedUSersAndLikesEnitties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_followers_users_UserId1",
                table: "followers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_followers_UserId1",
                table: "followers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "followers");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Like",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Aboutme",
                table: "AbpUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AbpUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "AbpUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "AbpUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavouriteVerse",
                table: "AbpUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Followers",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalChurch",
                table: "AbpUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "AbpUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_UsersId",
                table: "Like",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AbpUsers_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AbpUsers_UsersId",
                table: "Like",
                column: "UsersId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_AbpUsers_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AbpUsers_UsersId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UsersId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "Aboutme",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FavouriteVerse",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Followers",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LocalChurch",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "AbpUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "followers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aboutme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Family = table.Column<int>(type: "int", nullable: false),
                    FavouriteVerse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalChurch = table.Column<int>(type: "int", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Profession = table.Column<int>(type: "int", nullable: false),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_followers_UserId1",
                table: "followers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_followers_users_UserId1",
                table: "followers",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
