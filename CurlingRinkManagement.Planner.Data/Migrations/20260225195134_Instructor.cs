using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Instructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfInstructors",
                table: "ActivityTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorCalculationType",
                table: "ActivityTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerSelectedValue",
                table: "ActivityTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfInstructors",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "InstructorCalculationType",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "PerSelectedValue",
                table: "ActivityTypes");
        }
    }
}
