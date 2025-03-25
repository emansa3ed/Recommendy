using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editchatmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_SenderId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatGroupMembers");

            migrationBuilder.AddColumn<string>(
                name: "FirstUserId",
                table: "ChatGroups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondUserId",
                table: "ChatGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroups_FirstUserId",
                table: "ChatGroups",
                column: "FirstUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatGroups_AspNetUsers_FirstUserId",
                table: "ChatGroups",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "ChatGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatGroups_AspNetUsers_FirstUserId",
                table: "ChatGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_SenderId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatGroups_FirstUserId",
                table: "ChatGroups");

            migrationBuilder.DropColumn(
                name: "FirstUserId",
                table: "ChatGroups");

            migrationBuilder.DropColumn(
                name: "SecondUserId",
                table: "ChatGroups");

            migrationBuilder.CreateTable(
                name: "ChatGroupMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatGroupMembers_ChatGroups_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupMembers_ChatId",
                table: "ChatGroupMembers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupMembers_UserId",
                table: "ChatGroupMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatGroups_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "ChatGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
