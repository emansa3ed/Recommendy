using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editinternship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Internships");

            migrationBuilder.AlterColumn<string>(
                name: "UrlPicture",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Approach",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1998a2-5138-4d99-bda5-064b2c2f2f51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89849f5c-cdbe-4ef6-97e9-d297aead7ce0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2e71825-4660-425c-965a-ca08f8e28b6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df995461-3b08-45c4-956f-f3cb62849637");

            migrationBuilder.AlterColumn<string>(
                name: "UrlPicture",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Approach",
                table: "Internships",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Internships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Internships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
