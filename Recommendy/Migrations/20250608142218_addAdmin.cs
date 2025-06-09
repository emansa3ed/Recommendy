using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELO8TTQae/cUKAG4fT7/UYIiA9gn3RPgJPHH4Qp5j/VBQJw6JGzzLkdZkKjzZSjybw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELnkO5ayp7ebVoaXv0oujE/xEXDrBfsN3eq5gxY9pKolBFXVoIbu8KF5nwaLh1aM7Q==");
        }
    }
}
