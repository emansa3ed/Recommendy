﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class add_admin_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsBanned", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "SubscriptionId", "TwoFactorEnabled", "UrlPicture", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "System Administrator - Recommendy Platform", "64d84hg4-863b-44ce-9cac-75ec544afg45", new DateTime(2025, 6, 8, 12, 2, 36, 0, DateTimeKind.Utc), "Admin", "admin@recommendy.com", true, "Recommendy", false, "Team", false, null, "ADMIN@RECOMMENDY.COM", "RECOMMENDYADMIN", "AQAAAAIAAYagAAAAED5EMl1JrRjJZ6FYXLiT0q7FN47V0/GDJUH9THBaYFchiD65t+YAp/DcIIsIAu2/xA==", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "54GFNHD4564HNFG34FG", null, false, null, "RecommendyAdmin" });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "AdminId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            
        }
    }
}
