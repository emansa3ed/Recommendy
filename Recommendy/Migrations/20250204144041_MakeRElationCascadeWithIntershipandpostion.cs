using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class MakeRElationCascadeWithIntershipandpostion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPosition_Internships_InternshipId",
                table: "InternshipPosition");

           

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPosition_Internships_InternshipId",
                table: "InternshipPosition",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPosition_Internships_InternshipId",
                table: "InternshipPosition");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d55a03b-cbe0-4e7e-8990-1b5d20ed74e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dfd146f-21e6-4f66-af7c-31a71bf3a2df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "764136ed-de2b-440e-9990-e1536bd5b4bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98a6eb19-474b-40b3-8a9c-fdb368526f93");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0647c24b-5147-4adf-b950-7d2ed1a8b438", null, "Admin", "ADMIN" },
                    { "4a673550-7810-4568-9712-338a26807238", null, "Student", "STUDENT" },
                    { "74506bd9-7e33-4113-9520-62b45b3a9766", null, "University", "UNIVERSITY" },
                    { "994ece81-664e-4e3f-a0b2-b340a5355e8d", null, "Company", "COMPANY" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPosition_Internships_InternshipId",
                table: "InternshipPosition",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id");
        }
    }
}
