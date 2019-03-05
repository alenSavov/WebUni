using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDiary.Data.Migrations
{
    public partial class AddedCategoryPictureVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryVersionPicture",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryVersionPicture",
                table: "Categories");
        }
    }
}
