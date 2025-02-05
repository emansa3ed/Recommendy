using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }
         

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<UserCode> userCodes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            // add data to test 
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());





            base.OnModelCreating(builder);

            ///// Composite keys
            builder.Entity<SavedPost>()
                .HasKey(sp => new { sp.StudentId, sp.PostId });

            builder.Entity<UserInterest>()
                .HasKey(ui => new { ui.StudentId, ui.InterestId });

            builder.Entity<InternshipPosition>()
                .HasKey(ip => new { ip.InternshipId, ip.PositionId });
            builder.Entity<UserCode>()
                .HasKey(ui => new { ui.Id });
            
                

            // Configure Dictionary as JSON

            builder.Entity<Scholarship>()
                .Property(s => s.Requirements)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions())
                );


            ////relations 
            builder.Entity<User>()
                .HasOne(u => u.Company)
                .WithOne(c => c.User)
                .HasForeignKey<Company>(u => u.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasOne(u => u.University)
                .WithOne(u => u.User)
                .HasForeignKey<University>(u => u.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(u => u.StudentId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<University>()
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Scholarship>()
                .HasOne<University>()
                .WithMany()
                .HasForeignKey(s => s.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

          

             builder.Entity<Company>()
             .HasMany(c => c.Internships)
             .WithOne(i => i.Company) 
             .HasForeignKey(i => i.CompanyId)
             .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Internship>()
                .HasMany(i => i.InternshipPositions)
                .WithOne(ip => ip.Internship)
                .HasForeignKey(ip => ip.InternshipId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InternshipPosition>()
                .HasOne(ip => ip.Position)
                .WithMany()
                .HasForeignKey(ip => ip.PositionId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<Feedback>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Notification>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Report>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SavedPost>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(sp => sp.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserInterest>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(ui => ui.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserInterest>()
                .HasOne<Interest>()
                .WithMany()
                .HasForeignKey(ui => ui.InterestId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}