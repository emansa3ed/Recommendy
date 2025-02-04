using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13478e0b-a0e5-4dcf-b8e3-0ec3efcb7826");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44d08b12-4b5b-4f66-83c6-d7a150bd7865");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "456c684c-12ca-40c2-9a2c-0dcc8d285e31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56bbfc1a-f637-4cc8-ac94-ca6d77b3e946");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Universities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1672c939-808f-438d-b166-dea528915eb6", null, "Company", "COMPANY" },
                    { "7a04098b-fd16-4d69-aada-dee9c148facd", null, "University", "UNIVERSITY" },
                    { "d4505a6c-e6e3-46a3-ae57-34a02982d64e", null, "Admin", "ADMIN" },
                    { "fdb5c7c0-d6bc-47c7-b35a-f93ea612ca18", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1672c939-808f-438d-b166-dea528915eb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a04098b-fd16-4d69-aada-dee9c148facd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4505a6c-e6e3-46a3-ae57-34a02982d64e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdb5c7c0-d6bc-47c7-b35a-f93ea612ca18");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Universities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13478e0b-a0e5-4dcf-b8e3-0ec3efcb7826", null, "University", "UNIVERSITY" },
                    { "44d08b12-4b5b-4f66-83c6-d7a150bd7865", null, "Student", "STUDENT" },
                    { "456c684c-12ca-40c2-9a2c-0dcc8d285e31", null, "Company", "COMPANY" },
                    { "56bbfc1a-f637-4cc8-ac94-ca6d77b3e946", null, "Admin", "ADMIN" }
                });
        }
    }
}
