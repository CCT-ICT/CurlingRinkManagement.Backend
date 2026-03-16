using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations;

/// <inheritdoc />
public partial class RemovedTimeMargin : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MinutesBlockedAfter",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "MinutesBlockedBefore",
            table: "DateTimeRanges");

        migrationBuilder.DropColumn(
            name: "RecommendedMinutesBlockedAfter",
            table: "ActivityTypes");

        migrationBuilder.DropColumn(
            name: "RecommendedMinutesBlockedBefore",
            table: "ActivityTypes");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "MinutesBlockedAfter",
            table: "DateTimeRanges",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "MinutesBlockedBefore",
            table: "DateTimeRanges",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "RecommendedMinutesBlockedAfter",
            table: "ActivityTypes",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "RecommendedMinutesBlockedBefore",
            table: "ActivityTypes",
            type: "integer",
            nullable: false,
            defaultValue: 0);
    }
}
