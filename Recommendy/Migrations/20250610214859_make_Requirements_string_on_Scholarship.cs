using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class make_Requirements_string_on_Scholarship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEB97pYsl/13yz6RQy86n3WL/APQoa9V6pocBkB35pUyYqs34l+QkvuRR32ouTVE+VA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELEott2LVJ+UO/RThgoS1Dsy3s0fRcQv3d76F9wYlUIAiCExyutA5/DpLPH5NilPJg==");
        }
    }
}
