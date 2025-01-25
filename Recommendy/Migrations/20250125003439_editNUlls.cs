using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editNUlls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39fa8749-5edf-4dc9-813f-21ada07f3b73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "429e4d0d-c773-4574-bed4-797ce7b7afc3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d7c943-f1de-44c1-8f16-0fe9e1634e75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73720853-9b49-41f7-98a5-de8591843979");

            migrationBuilder.AlterColumn<string>(
                name: "UrlResume",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UrlResume",
                table: "Students",
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
                    { "39fa8749-5edf-4dc9-813f-21ada07f3b73", null, "University", "UNIVERSITY" },
                    { "429e4d0d-c773-4574-bed4-797ce7b7afc3", null, "Admin", "ADMIN" },
                    { "69d7c943-f1de-44c1-8f16-0fe9e1634e75", null, "Company", "COMPANY" },
                    { "73720853-9b49-41f7-98a5-de8591843979", null, "Student", "STUDENT" }
                });
        }
    }
}
