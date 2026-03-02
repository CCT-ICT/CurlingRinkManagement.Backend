using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInstructorLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedInstrutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SheetActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    SheetActivityActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserIdentity = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedInstrutor", x => new { x.SheetActivityActivityId, x.SheetActivityId, x.Id });
                    table.ForeignKey(
                        name: "FK_LinkedInstrutor_SheetActivity_SheetActivityActivityId_Sheet~",
                        columns: x => new { x.SheetActivityActivityId, x.SheetActivityId },
                        principalTable: "SheetActivity",
                        principalColumns: new[] { "ActivityId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedInstrutor");
        }
    }
}
