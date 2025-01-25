using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class edits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42a4432f-ebbf-4470-803b-353cc89ce165");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "665c6067-efc3-48c5-b4a7-d954aa730db7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9213a6f5-f191-4a74-a68b-86b90f95e193");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1947d8a-a151-438f-a542-cd6c717695da");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Discriminator",
                table: "AspNetUsers",
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
                    { "42a4432f-ebbf-4470-803b-353cc89ce165", null, "University", "UNIVERSITY" },
                    { "665c6067-efc3-48c5-b4a7-d954aa730db7", null, "Student", "STUDENT" },
                    { "9213a6f5-f191-4a74-a68b-86b90f95e193", null, "Company", "COMPANY" },
                    { "f1947d8a-a151-438f-a542-cd6c717695da", null, "Admin", "ADMIN" }
                });
        }
    }
}
