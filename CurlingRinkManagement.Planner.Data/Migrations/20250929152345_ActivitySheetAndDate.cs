using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations;

/// <inheritdoc />
public partial class ActivitySheetAndDate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges");

        migrationBuilder.AddColumn<Guid>(
            name: "ActivityTimeId",
            table: "SheetActivity",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_SheetActivity_ActivityTimeId",
            table: "SheetActivity",
            column: "ActivityTimeId");

        migrationBuilder.CreateIndex(
            name: "IX_DateTimeRanges_ActivityId",
            table: "DateTimeRanges",
            column: "ActivityId");

        migrationBuilder.AddForeignKey(
            name: "FK_SheetActivity_DateTimeRanges_ActivityTimeId",
            table: "SheetActivity",
            column: "ActivityTimeId",
            principalTable: "DateTimeRanges",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SheetActivity_DateTimeRanges_ActivityTimeId",
            table: "SheetActivity");

        migrationBuilder.DropIndex(
            name: "IX_SheetActivity_ActivityTimeId",
            table: "SheetActivity");

        migrationBuilder.DropPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges");

        migrationBuilder.DropIndex(
            name: "IX_DateTimeRanges_ActivityId",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "ActivityTimeId",
            table: "SheetActivity");

        migrationBuilder.AddPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges",
            columns: new[] { "ActivityId", "Id" });
    }
}
