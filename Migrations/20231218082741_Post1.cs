using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace final_project.Migrations
{
    /// <inheritdoc />
    public partial class Post1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Post",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Users_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Users_UserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UserId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Post");

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Content", "DateTime", "Username" },
                values: new object[,]
                {
                    { 1, "Freshly pulled double espresso with steamed milk", new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Local), "Cappuccino" },
                    { 2, "Freshly pulled double espresso with steamed milk", new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Local), "Cappuccino2" }
                });
        }
    }
}
