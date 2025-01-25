using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editss1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "CompanyUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03835126-bde0-45a4-99d0-43ca03fbfffe", null, "University", "UNIVERSITY" },
                    { "38848543-f4bb-469d-942b-078b5b9e647e", null, "Company", "COMPANY" },
                    { "a4d48f37-882c-4fbc-a4fb-429c481da514", null, "Student", "STUDENT" },
                    { "fe6fd204-0a72-4ebc-afc8-c20b9aa5e569", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03835126-bde0-45a4-99d0-43ca03fbfffe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38848543-f4bb-469d-942b-078b5b9e647e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4d48f37-882c-4fbc-a4fb-429c481da514");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe6fd204-0a72-4ebc-afc8-c20b9aa5e569");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
