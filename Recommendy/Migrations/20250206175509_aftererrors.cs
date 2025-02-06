using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class aftererrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72706342-42bd-419a-97b1-7cbb6d2e3987");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b6a7853-f1c8-47a4-9ae5-64d977d1f296");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936ff487-c4b5-42de-8291-91aec7a8c033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fef184f3-a478-4f1d-85c8-6be05cf7425a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3db5302f-e6d8-4468-9e8c-b2d8a67566fb", null, "Admin", "ADMIN" },
                    { "551543eb-c61a-4c21-a27d-dd87c4d2e6c5", null, "University", "UNIVERSITY" },
                    { "b4712b3d-f929-4723-942b-41091e5f3d16", null, "Company", "COMPANY" },
                    { "e0d10804-4b43-4e55-9631-14ba2fb1942d", null, "Student", "STUDENT" }
                });
        }
    }
}
