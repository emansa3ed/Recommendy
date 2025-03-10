using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class addFristlastname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f78af5-b6e3-4456-87c8-ebeddadf1ce7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45a39ec2-afc4-40b8-bec7-1bd338ba4579");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a200ad26-ab1f-4500-b001-c79651e5f3d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ccc680-de33-4fc2-b62f-d1162d4a7397");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "289de362-c0fb-4f58-8ae3-4fd9db601c08", null, "Admin", "ADMIN" },
                    { "38ad5aaa-3444-4950-a565-2d681d940c29", null, "Company", "COMPANY" },
                    { "60007fd7-b46d-4903-9341-36f080b9b2d8", null, "University", "UNIVERSITY" },
                    { "7ca566a8-e901-4301-986c-5a2527ceb4a3", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "289de362-c0fb-4f58-8ae3-4fd9db601c08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38ad5aaa-3444-4950-a565-2d681d940c29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60007fd7-b46d-4903-9341-36f080b9b2d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca566a8-e901-4301-986c-5a2527ceb4a3");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39f78af5-b6e3-4456-87c8-ebeddadf1ce7", null, "Student", "STUDENT" },
                    { "45a39ec2-afc4-40b8-bec7-1bd338ba4579", null, "University", "UNIVERSITY" },
                    { "a200ad26-ab1f-4500-b001-c79651e5f3d3", null, "Company", "COMPANY" },
                    { "e3ccc680-de33-4fc2-b62f-d1162d4a7397", null, "Admin", "ADMIN" }
                });
        }
    }
}
