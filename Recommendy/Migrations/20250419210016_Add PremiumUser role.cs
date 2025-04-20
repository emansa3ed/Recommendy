using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class AddPremiumUserrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c2c57af-5ed6-45cb-92ed-cb1906d624e8", null, "PremiumUser", "PREMIUMUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "070d0997-5ba0-4ffc-b99f-03b5adfb894a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c2c57af-5ed6-45cb-92ed-cb1906d624e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed032f9-eb97-499a-b130-17a8a3c4bda4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6458d3f6-9389-4436-85a5-62b7da8dd113");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb096a2d-45c6-4bba-81f0-9b8bef6e7070");

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
    }
}
