using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class addUniversityUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<string>(
                name: "UniversityUrl",
                table: "Universities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29251520-9478-4406-9c70-06c4cf7a126e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f9dc469-33c5-495d-8d0b-6d80862e5bbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b8704c4-fb32-429d-92c3-dcd46124394e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8015004c-f6f5-451e-a426-aba84d949623");

            migrationBuilder.DropColumn(
                name: "UniversityUrl",
                table: "Universities");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12ca8165-11b3-447c-91d5-e6e15796c02d", null, "Admin", "ADMIN" },
                    { "25ca81fc-040a-46de-9382-06effc90c3ae", null, "University", "UNIVERSITY" },
                    { "974f9ba0-6377-482f-addd-f0cac85c7d13", null, "Student", "STUDENT" },
                    { "f3b45040-6423-4c9b-b179-5a18c62141f6", null, "Company", "COMPANY" }
                });
        }
    }
}
