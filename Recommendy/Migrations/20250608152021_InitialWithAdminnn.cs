using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithAdminnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDRY0i6L7HhjxGLB+Q6swKwxQvA/lvTfnAKW2gBk4kG2aCfncz1/kDcwqGkqixvswQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKKYoNJf3owBSpE50jRs1vEBEMlbdZ33Zw5j1zizSEHf208ZB4qxoo/pJ7fuGRGKYA==");
        }
    }
}
