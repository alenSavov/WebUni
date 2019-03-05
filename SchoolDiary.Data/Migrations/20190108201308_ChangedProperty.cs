using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDiary.Data.Migrations
{
    public partial class ChangedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ArticleVersionPicture",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleVersionPicture",
                table: "Articles");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Articles",
                nullable: true);
        }
    }
}
