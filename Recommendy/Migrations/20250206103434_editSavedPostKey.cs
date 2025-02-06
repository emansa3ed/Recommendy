using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editSavedPostKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scholarships_Universities_UniversityId",
                table: "Scholarships");

            

         

            migrationBuilder.AddForeignKey(
                name: "FK_Scholarships_Universities_UniversityId",
                table: "Scholarships",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scholarships_Universities_UniversityId",
                table: "Scholarships");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3db5302f-e6d8-4468-9e8c-b2d8a67566fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "551543eb-c61a-4c21-a27d-dd87c4d2e6c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4712b3d-f929-4723-942b-41091e5f3d16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0d10804-4b43-4e55-9631-14ba2fb1942d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0604c49e-1d9b-4190-9ae8-a72d7c397853", null, "Admin", "ADMIN" },
                    { "1054053f-8ecd-4d6b-b530-d167946d4424", null, "University", "UNIVERSITY" },
                    { "3a6a98b8-dd94-48a3-b57a-6c7bcbf94be2", null, "Company", "COMPANY" },
                    { "d79ac938-6896-4cb3-967f-3fcbef8cf0ad", null, "Student", "STUDENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Scholarships_Universities_UniversityId",
                table: "Scholarships",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId");
        }
    }
}
