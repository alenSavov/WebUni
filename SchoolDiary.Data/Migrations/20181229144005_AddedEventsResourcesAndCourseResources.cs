using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDiary.Data.Migrations
{
    public partial class AddedEventsResourcesAndCourseResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Courses_CourseId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Events_EventId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_CourseId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_EventId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Resources");

            migrationBuilder.CreateTable(
                name: "CoursesResources",
                columns: table => new
                {
                    CourseId = table.Column<string>(nullable: false),
                    ResourceId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesResources", x => new { x.CourseId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_CoursesResources_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GetEventsResources",
                columns: table => new
                {
                    EventId = table.Column<string>(nullable: false),
                    ResourceId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetEventsResources", x => new { x.EventId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_GetEventsResources_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetEventsResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesResources_ResourceId",
                table: "CoursesResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_GetEventsResources_ResourceId",
                table: "GetEventsResources",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesResources");

            migrationBuilder.DropTable(
                name: "GetEventsResources");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Resources",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventId",
                table: "Resources",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CourseId",
                table: "Resources",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_EventId",
                table: "Resources",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Courses_CourseId",
                table: "Resources",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Events_EventId",
                table: "Resources",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
