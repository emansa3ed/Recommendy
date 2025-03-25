using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class edit12chatmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatGroups_AspNetUsers_FirstUserId",
                table: "ChatGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups");

            migrationBuilder.RenameTable(
                name: "ChatGroups",
                newName: "ChatUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ChatGroups_FirstUserId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_FirstUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatUsers_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_FirstUserId",
                table: "ChatUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers");

            migrationBuilder.RenameTable(
                name: "ChatUsers",
                newName: "ChatGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_FirstUserId",
                table: "ChatGroups",
                newName: "IX_ChatGroups_FirstUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatGroups_AspNetUsers_FirstUserId",
                table: "ChatGroups",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "ChatGroups",
                principalColumn: "Id");
        }
    }
}
