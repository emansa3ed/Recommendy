using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
           new IdentityRole
            {
            Name = "Student",
            NormalizedName = "STUDENT"
            },
                    new IdentityRole
                    {
                       // Id = IdentityConstants.Roles.AdminRoleId,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                     new IdentityRole
               {
                   Name = "Company",
                   NormalizedName = "COMPANY"
               },
                 new IdentityRole
                 {
                     Name = "University",
                     NormalizedName = "UNIVERSITY"
                 },

                 new IdentityRole
				 {
					 Name = "PremiumUser",
                    NormalizedName = "PREMIUMUSER"
		        }
         );

        }
    }
}
