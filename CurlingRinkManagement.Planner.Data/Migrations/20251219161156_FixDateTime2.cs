using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations;

/// <inheritdoc />
public partial class FixDateTime2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DateTimeRanges_Activities_ActivityId",
            table: "DateTimeRanges");

        migrationBuilder.DropIndex(
            name: "IX_DateTimeRanges_ActivityId",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "ActivityId",
            table: "DateTimeRanges");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "ActivityId",
            table: "DateTimeRanges",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateIndex(
            name: "IX_DateTimeRanges_ActivityId",
            table: "DateTimeRanges",
            column: "ActivityId");

        migrationBuilder.AddForeignKey(
            name: "FK_DateTimeRanges_Activities_ActivityId",
            table: "DateTimeRanges",
            column: "ActivityId",
            principalTable: "Activities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
