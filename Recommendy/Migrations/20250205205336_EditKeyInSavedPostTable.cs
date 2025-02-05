using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class EditKeyInSavedPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts");

            ;

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts",
                columns: new[] { "StudentId", "PostId", "Type" });

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0604c49e-1d9b-4190-9ae8-a72d7c397853");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1054053f-8ecd-4d6b-b530-d167946d4424");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a6a98b8-dd94-48a3-b57a-6c7bcbf94be2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d79ac938-6896-4cb3-967f-3fcbef8cf0ad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts",
                columns: new[] { "StudentId", "PostId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16886ca6-5747-47e7-b1eb-56fd80bb08db", null, "Admin", "ADMIN" },
                    { "7c8b7db3-2cc7-45db-8c22-7090b4a767b8", null, "University", "UNIVERSITY" },
                    { "f5de4cdd-d3b7-4dcc-9003-e0b2eb70a4ea", null, "Company", "COMPANY" },
                    { "f9709014-a642-42eb-a8fc-c49ed9e705e4", null, "Student", "STUDENT" }
                });
        }
    }
}
