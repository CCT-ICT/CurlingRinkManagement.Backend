using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubName = table.Column<string>(type: "text", nullable: false),
                    ClubAbbriviation = table.Column<string>(type: "text", nullable: false),
                    ClubGroup = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
