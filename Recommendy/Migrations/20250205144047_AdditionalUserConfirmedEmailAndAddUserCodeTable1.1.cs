using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserConfirmedEmailAndAddUserCodeTable11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           ;

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "userCodes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userCodes",
                table: "userCodes",
                column: "Id");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userCodes",
                table: "userCodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16886ca6-5747-47e7-b1eb-56fd80bb08db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c8b7db3-2cc7-45db-8c22-7090b4a767b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5de4cdd-d3b7-4dcc-9003-e0b2eb70a4ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9709014-a642-42eb-a8fc-c49ed9e705e4");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userCodes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fc9764c-9d8b-4671-80a0-192aa93d7784", null, "Student", "STUDENT" },
                    { "766c9b56-6b10-4afa-bc45-92fec1598d55", null, "Admin", "ADMIN" },
                    { "b126403c-4564-44a7-9467-62680cab3d83", null, "Company", "COMPANY" },
                    { "fdc487d2-ff15-4be4-90d2-8f205c053216", null, "University", "UNIVERSITY" }
                });
        }
    }
}
