using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class ActivityOwnsSheetLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SheetActivity",
                table: "SheetActivity");

            migrationBuilder.DropIndex(
                name: "IX_SheetActivity_ActivityId",
                table: "SheetActivity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheetActivity",
                table: "SheetActivity",
                columns: new[] { "ActivityId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SheetActivity",
                table: "SheetActivity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheetActivity",
                table: "SheetActivity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SheetActivity_ActivityId",
                table: "SheetActivity",
                column: "ActivityId");
        }
    }
}
