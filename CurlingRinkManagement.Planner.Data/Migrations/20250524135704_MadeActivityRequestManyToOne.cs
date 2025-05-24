using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlingRinkManagement.Planner.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeActivityRequestManyToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_CustomerRequests_CustomerRequestId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CustomerRequestId",
                table: "Activities");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "CustomerRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRequests_ActivityId",
                table: "CustomerRequests",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRequests_Activities_ActivityId",
                table: "CustomerRequests",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRequests_Activities_ActivityId",
                table: "CustomerRequests");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRequests_ActivityId",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "CustomerRequests");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CustomerRequestId",
                table: "Activities",
                column: "CustomerRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_CustomerRequests_CustomerRequestId",
                table: "Activities",
                column: "CustomerRequestId",
                principalTable: "CustomerRequests",
                principalColumn: "Id");
        }
    }
}
