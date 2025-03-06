using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class Editinnotificationmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d784168-0f41-404e-b724-3337537d1ccd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dc96a1d-30e7-45b0-9764-1292922e2ef8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d703d2b9-4dbf-4ec4-8f85-77b4507adebe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4f4eab9-cb61-4d7e-b9f2-c66fd1947918");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "ReceiverID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_ReceiverID");

            migrationBuilder.AddColumn<string>(
                name: "ActorID",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39f78af5-b6e3-4456-87c8-ebeddadf1ce7", null, "Student", "STUDENT" },
                    { "45a39ec2-afc4-40b8-bec7-1bd338ba4579", null, "University", "UNIVERSITY" },
                    { "a200ad26-ab1f-4500-b001-c79651e5f3d3", null, "Company", "COMPANY" },
                    { "e3ccc680-de33-4fc2-b62f-d1162d4a7397", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ReceiverID",
                table: "Notifications",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ReceiverID",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f78af5-b6e3-4456-87c8-ebeddadf1ce7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45a39ec2-afc4-40b8-bec7-1bd338ba4579");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a200ad26-ab1f-4500-b001-c79651e5f3d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ccc680-de33-4fc2-b62f-d1162d4a7397");

            migrationBuilder.DropColumn(
                name: "ActorID",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ReceiverID",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ReceiverID",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d784168-0f41-404e-b724-3337537d1ccd", null, "Admin", "ADMIN" },
                    { "2dc96a1d-30e7-45b0-9764-1292922e2ef8", null, "Company", "COMPANY" },
                    { "d703d2b9-4dbf-4ec4-8f85-77b4507adebe", null, "University", "UNIVERSITY" },
                    { "e4f4eab9-cb61-4d7e-b9f2-c66fd1947918", null, "Student", "STUDENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
