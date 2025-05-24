using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeContactsAndTagsManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ContactId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "ContactTag",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTag", x => new { x.ContactId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ContactTag_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactTag_TagsId",
                table: "ContactTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactTag");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Tags",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ContactId",
                table: "Tags",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
