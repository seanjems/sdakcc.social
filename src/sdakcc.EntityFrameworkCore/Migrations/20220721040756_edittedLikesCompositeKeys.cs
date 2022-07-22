using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sdakcc.Migrations
{
    public partial class edittedLikesCompositeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "likes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "likes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "likes");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "likes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "likes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "likes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "likes",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
