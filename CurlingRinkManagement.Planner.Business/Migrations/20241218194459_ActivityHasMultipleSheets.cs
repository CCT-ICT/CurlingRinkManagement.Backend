using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class ActivityHasMultipleSheets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Sheets_SheetId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SheetId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SheetId",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "SheetActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SheetId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetActivity_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheetActivity_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheetActivity_ActivityId",
                table: "SheetActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetActivity_SheetId",
                table: "SheetActivity",
                column: "SheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetActivity");

            migrationBuilder.AddColumn<Guid>(
                name: "SheetId",
                table: "Activities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SheetId",
                table: "Activities",
                column: "SheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Sheets_SheetId",
                table: "Activities",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
