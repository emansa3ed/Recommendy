using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class addrolesagian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d784168-0f41-404e-b724-3337537d1ccd", null, "Admin", "ADMIN" },
                    { "2dc96a1d-30e7-45b0-9764-1292922e2ef8", null, "Company", "COMPANY" },
                    { "d703d2b9-4dbf-4ec4-8f85-77b4507adebe", null, "University", "UNIVERSITY" },
                    { "e4f4eab9-cb61-4d7e-b9f2-c66fd1947918", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Eyg" },
                    { 6, "moroc" }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Software Engineer" },
                    { 2, "Data Scientist" },
                    { 3, "Product Manager" },
                    { 4, "DevOps Engineer" },
                    { 5, "UI/UX Designer" },
                    { 6, "Quality Assurance Engineer" },
                    { 7, "Systems Administrator" },
                    { 8, "Network Engineer" },
                    { 9, "Database Administrator" },
                    { 10, "Business Analyst" },
                    { 11, "Technical Support Specialist" },
                    { 12, "Cybersecurity Analyst" },
                    { 13, "Cloud Architect" },
                    { 14, "Machine Learning Engineer" },
                    { 15, "Mobile Application Developer" },
                    { 16, "Web Developer" },
                    { 17, "Scrum Master" },
                    { 18, "IT Project Manager" },
                    { 19, "Technical Writer" },
                    { 20, "Chief Technology Officer (CTO)" },
                    { 21, "Frontend Developer" },
                    { 22, "Backend Developer" },
                    { 23, "Full Stack Developer" },
                    { 24, "Data Engineer" },
                    { 25, "AI Researcher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d784168-0f41-404e-b724-3337537d1ccd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dc96a1d-30e7-45b0-9764-1292922e2ef8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d703d2b9-4dbf-4ec4-8f85-77b4507adebe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4f4eab9-cb61-4d7e-b9f2-c66fd1947918");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
