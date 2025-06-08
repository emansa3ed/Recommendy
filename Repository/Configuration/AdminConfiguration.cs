using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            var adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9";

            builder.HasData(new Admin
            {
                AdminId = adminId
            });
        }
    }
}