using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var adminUser = new User
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // Fixed GUID for admin
                UserName = "RecommendyAdmin",
                NormalizedUserName = "RECOMMENDYADMIN",
                Email = "admin@recommendy.com",
                NormalizedEmail = "ADMIN@RECOMMENDY.COM",
                FirstName = "Recommendy",
                LastName = "Team",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = "54GFNHD4564HNFG34FG",
                ConcurrencyStamp = "64d84hg4-863b-44ce-9cac-75ec544afg45",
                Discriminator = "Admin",
                CreatedAt = new DateTime(2025, 06, 08, 12, 02, 36, DateTimeKind.Utc), // Using your provided timestamp
                Bio = "System Administrator - Recommendy Platform"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123456");

            builder.HasData(adminUser);
        }
    }
}