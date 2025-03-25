using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class edit13chatmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatUsers_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_FirstUserId",
                table: "ChatUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatUsers_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "ChatUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_AspNetUsers_FirstUserId",
                table: "ChatUsers",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatUsers_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_FirstUserId",
                table: "ChatUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatUsers_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "ChatUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_AspNetUsers_FirstUserId",
                table: "ChatUsers",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
