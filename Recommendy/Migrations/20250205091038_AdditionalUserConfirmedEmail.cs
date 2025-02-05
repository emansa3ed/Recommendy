using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserConfirmedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39e80fff-3c6f-4350-866f-986bf7176817");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "709c9100-aac8-4e48-9efc-686c450b6c57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e6c423c-f083-405c-a646-bb479e80d9e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d282f2c5-64b9-490e-bcab-435edb37a8c5");

            migrationBuilder.DropColumn(
                name: "ConfirmedEmail",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1936befe-5ca4-4ad1-a544-3f94ff025dca", null, "Company", "COMPANY" },
                    { "2770273b-615c-4f1c-8d22-d489e094c64a", null, "Student", "STUDENT" },
                    { "da024573-2d08-438c-a2dd-3e669c7fe68e", null, "Admin", "ADMIN" },
                    { "f583c322-c57e-440d-897b-c9388aeba8ab", null, "University", "UNIVERSITY" }
                });
        }
    }
}
