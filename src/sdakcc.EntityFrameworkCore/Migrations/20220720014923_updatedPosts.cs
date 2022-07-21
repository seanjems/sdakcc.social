using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sdakcc.Migrations
{
    public partial class updatedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "posts",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<int>(
                name: "PostType",
                table: "posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "posts",
                newName: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "PostType",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
