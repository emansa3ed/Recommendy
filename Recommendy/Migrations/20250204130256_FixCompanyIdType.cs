using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class FixCompanyIdType : Migration
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
                keyValue: "0647c24b-5147-4adf-b950-7d2ed1a8b438");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a673550-7810-4568-9712-338a26807238");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74506bd9-7e33-4113-9520-62b45b3a9766");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "994ece81-664e-4e3f-a0b2-b340a5355e8d");

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
