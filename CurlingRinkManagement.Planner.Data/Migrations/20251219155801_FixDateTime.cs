using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations;

/// <inheritdoc />
public partial class FixDateTime : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropColumn(
            name: "ActivityTimeId",
            table: "SheetActivity");

        migrationBuilder.AddColumn<Guid>(
            name: "SheetActivityActivityId",
            table: "DateTimeRanges",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "SheetActivityId",
            table: "DateTimeRanges",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges",
            columns: new[] { "SheetActivityActivityId", "SheetActivityId" });

        migrationBuilder.AddForeignKey(
            name: "FK_DateTimeRanges_SheetActivity_SheetActivityActivityId_SheetA~",
            table: "DateTimeRanges",
            columns: new[] { "SheetActivityActivityId", "SheetActivityId" },
            principalTable: "SheetActivity",
            principalColumns: new[] { "ActivityId", "Id" },
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DateTimeRanges_SheetActivity_SheetActivityActivityId_SheetA~",
            table: "DateTimeRanges");

        migrationBuilder.DropPrimaryKey(
            name: "PK_DateTimeRanges",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "SheetActivityActivityId",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "SheetActivityId",
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

        migrationBuilder.AddForeignKey(
            name: "FK_SheetActivity_DateTimeRanges_ActivityTimeId",
            table: "SheetActivity",
            column: "ActivityTimeId",
            principalTable: "DateTimeRanges",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
