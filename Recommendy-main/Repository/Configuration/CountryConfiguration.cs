using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Repository.Configuration
{
    public  class CountryConfiguration : IEntityTypeConfiguration<Country>
    {

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData
            (
            new Country
            {
                Id =    5,
                Name = "Eyg",

            },
            new Country
            {
                Id = 6,

                Name = "moroc",
            
            }
            );
        }
    }
}
