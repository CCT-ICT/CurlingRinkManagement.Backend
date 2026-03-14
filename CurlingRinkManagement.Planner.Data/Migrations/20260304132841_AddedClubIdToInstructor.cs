using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedClubIdToInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "LinkedInstructor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "LinkedInstructor");
        }
    }
}
