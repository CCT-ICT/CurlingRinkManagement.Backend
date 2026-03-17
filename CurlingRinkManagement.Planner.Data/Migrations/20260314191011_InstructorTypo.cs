using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations;

/// <inheritdoc />
public partial class InstructorTypo : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "AmountOfInstructor",
            table: "SheetActivity",
            newName: "AmountOfInstructors");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "AmountOfInstructors",
            table: "SheetActivity",
            newName: "AmountOfInstructor");
    }
}
