using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editinintershipyable1 : Migration
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
                keyValue: "4cf9e018-d5bc-464d-af68-ac50938d405c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fe3caa9-b3cc-4197-9c7e-86ee79de3d66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad4b9f6c-e26e-4157-86df-ed2959511524");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba20ec96-ff56-44b8-b040-074414386d19");

            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "077cab49-f766-4b67-8056-30c49665d63b", null, "Student", "STUDENT" },
                    { "18d0c478-675e-467d-881a-45f97877f87d", null, "Admin", "ADMIN" },
                    { "93ff7328-a0bb-44f3-8266-b1083b128083", null, "Company", "COMPANY" },
                    { "9f6dc5e7-ffad-4cf9-abc9-9f856339097a", null, "University", "UNIVERSITY" }
                });

           
        }
    }
}
