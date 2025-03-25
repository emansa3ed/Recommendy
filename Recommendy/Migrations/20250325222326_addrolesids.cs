using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class addrolesids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "033f0c98-d0e1-43d9-9ac8-eae84e77bb9e", null, "Company", "COMPANY" },
                    { "2ad1177d-4cd8-4a92-8a58-a22077d8c901", null, "University", "UNIVERSITY" },
                    { "711ed7a1-df38-4447-bd87-dd0acbcf5735", null, "Admin", "ADMIN" },
                    { "9c091140-5430-4fb7-9875-3b764f5b51ed", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "033f0c98-d0e1-43d9-9ac8-eae84e77bb9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad1177d-4cd8-4a92-8a58-a22077d8c901");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "711ed7a1-df38-4447-bd87-dd0acbcf5735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c091140-5430-4fb7-9875-3b764f5b51ed");
        }
    }
}
