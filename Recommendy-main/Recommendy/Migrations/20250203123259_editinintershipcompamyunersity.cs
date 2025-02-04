using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class editinintershipcompamyunersity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Internships",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanysCompanyId",
                table: "Internships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

           

          

     

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Companies_CompanyId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Companies_CompanysCompanyId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_CompanyId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_CompanysCompanyId",
                table: "Internships");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a74b6317-af2b-40ce-910f-bcea4be55957");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c083ff85-764c-446a-abd9-4f234852d31c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d036c778-ef81-4932-9a1e-7b22516668ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d830ab28-f1f1-459a-bbef-73dd5ec771fc");

            migrationBuilder.DropColumn(
                name: "CompanysCompanyId",
                table: "Internships");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71898a5d-7cef-4fe6-a97d-6e10c17362a7", null, "Student", "STUDENT" },
                    { "757115a8-712e-4b57-a77c-a6f24aa25d47", null, "Company", "COMPANY" },
                    { "d38242f2-c822-4d73-a0d7-411ed8298457", null, "University", "UNIVERSITY" },
                    { "fc10006f-734e-461f-aeda-ee4f2e4d4177", null, "Admin", "ADMIN" }
                });
        }
    }
}
