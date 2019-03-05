using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDiary.Data.Migrations
{
    public partial class ChangedEventResourcesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetEventsResources_Events_EventId",
                table: "GetEventsResources");

            migrationBuilder.DropForeignKey(
                name: "FK_GetEventsResources_Resources_ResourceId",
                table: "GetEventsResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetEventsResources",
                table: "GetEventsResources");

            migrationBuilder.RenameTable(
                name: "GetEventsResources",
                newName: "EventsResources");

            migrationBuilder.RenameIndex(
                name: "IX_GetEventsResources_ResourceId",
                table: "EventsResources",
                newName: "IX_EventsResources_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsResources",
                table: "EventsResources",
                columns: new[] { "EventId", "ResourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventsResources_Events_EventId",
                table: "EventsResources",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsResources_Resources_ResourceId",
                table: "EventsResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsResources_Events_EventId",
                table: "EventsResources");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsResources_Resources_ResourceId",
                table: "EventsResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsResources",
                table: "EventsResources");

            migrationBuilder.RenameTable(
                name: "EventsResources",
                newName: "GetEventsResources");

            migrationBuilder.RenameIndex(
                name: "IX_EventsResources_ResourceId",
                table: "GetEventsResources",
                newName: "IX_GetEventsResources_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetEventsResources",
                table: "GetEventsResources",
                columns: new[] { "EventId", "ResourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GetEventsResources_Events_EventId",
                table: "GetEventsResources",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetEventsResources_Resources_ResourceId",
                table: "GetEventsResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
