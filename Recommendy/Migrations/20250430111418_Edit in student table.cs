using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class Editinstudenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Students");
        }
    }
}
