using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class sp3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ac13f3b-f286-4f4f-8090-889383cb8d06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2109bae5-c039-43ea-98a2-99df924a5b0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "463aac3c-974d-4b7b-a4f1-b9bb8ba5a1ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51f305e8-df06-4da1-9d5c-6f07801bb2a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "230ce90d-12de-435b-a69d-bbb60b59cc67", null, "Admin", "ADMIN" },
                    { "5415e598-7b20-4258-b073-f95ca49c573c", null, "Company", "COMPANY" },
                    { "5d954a50-e302-4356-b79f-09e1be1797a3", null, "University", "UNIVERSITY" },
                    { "e4186d1c-5fe5-4d59-a64d-cdcf28fd9e3c", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "230ce90d-12de-435b-a69d-bbb60b59cc67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5415e598-7b20-4258-b073-f95ca49c573c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d954a50-e302-4356-b79f-09e1be1797a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4186d1c-5fe5-4d59-a64d-cdcf28fd9e3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ac13f3b-f286-4f4f-8090-889383cb8d06", null, "University", "UNIVERSITY" },
                    { "2109bae5-c039-43ea-98a2-99df924a5b0d", null, "Company", "COMPANY" },
                    { "463aac3c-974d-4b7b-a4f1-b9bb8ba5a1ad", null, "Admin", "ADMIN" },
                    { "51f305e8-df06-4da1-9d5c-6f07801bb2a7", null, "Student", "STUDENT" }
                });
        }
    }
}
