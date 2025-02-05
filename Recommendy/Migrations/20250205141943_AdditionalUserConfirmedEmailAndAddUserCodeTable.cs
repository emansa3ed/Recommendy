using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserConfirmedEmailAndAddUserCodeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "userCodes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userCodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fc9764c-9d8b-4671-80a0-192aa93d7784");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "766c9b56-6b10-4afa-bc45-92fec1598d55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b126403c-4564-44a7-9467-62680cab3d83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdc487d2-ff15-4be4-90d2-8f205c053216");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "354dc2ae-f936-478c-9f73-3bb4d246ccc8", null, "Company", "COMPANY" },
                    { "7e0b5fc4-5a72-480f-a76c-4d5415208a95", null, "Admin", "ADMIN" },
                    { "c6d336ab-f06c-4118-b120-31cdec5b31c9", null, "University", "UNIVERSITY" },
                    { "f02ce53a-223e-4ee2-9e12-0fbe2e329505", null, "Student", "STUDENT" }
                });
        }
    }
}
