using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class EditinFeedbackmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05d7b25e-fded-4bbb-9d0a-54344ec24dd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61dcd296-81ca-45f1-a7c4-a4daad315d95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d08b72d-7f84-4f9a-bf48-377b4d648601");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff21ccc5-a313-4893-8ef5-21a3fbb6b37f");

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

            migrationBuilder.DropColumn(
                name: "TotalRating",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Feedbacks",
                newName: "PostId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Feedbacks",
                newName: "TypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Feedbacks",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TotalRating",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05d7b25e-fded-4bbb-9d0a-54344ec24dd9", null, "Company", "COMPANY" },
                    { "61dcd296-81ca-45f1-a7c4-a4daad315d95", null, "University", "UNIVERSITY" },
                    { "6d08b72d-7f84-4f9a-bf48-377b4d648601", null, "Student", "STUDENT" },
                    { "ff21ccc5-a313-4893-8ef5-21a3fbb6b37f", null, "Admin", "ADMIN" }
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
    }
}
