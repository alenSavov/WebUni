using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDiary.Data.Migrations
{
    public partial class setCloudinaryForCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "CourseVersionPicture",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseVersionPicture",
                table: "Courses");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Categories",
                nullable: true);
        }
    }
}
