using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData
            (
                new Position { Id = 1, Name = "Software Engineer" },
            new Position { Id = 2, Name = "Data Scientist" },
            new Position { Id = 3, Name = "Product Manager" },
            new Position { Id = 4, Name = "DevOps Engineer" },
            new Position { Id = 5, Name = "UI/UX Designer" },
            new Position { Id = 6, Name = "Quality Assurance Engineer" },
            new Position { Id = 7, Name = "Systems Administrator" },
            new Position { Id = 8, Name = "Network Engineer" },
            new Position { Id = 9, Name = "Database Administrator" },
            new Position { Id = 10, Name = "Business Analyst" },
            new Position { Id = 11, Name = "Technical Support Specialist" },
            new Position { Id = 12, Name = "Cybersecurity Analyst" },
            new Position { Id = 13, Name = "Cloud Architect" },
            new Position { Id = 14, Name = "Machine Learning Engineer" },
            new Position { Id = 15, Name = "Mobile Application Developer" },
            new Position { Id = 16, Name = "Web Developer" },
            new Position { Id = 17, Name = "Scrum Master" },
            new Position { Id = 18, Name = "IT Project Manager" },
            new Position { Id = 19, Name = "Technical Writer" },
            new Position { Id = 20, Name = "Chief Technology Officer (CTO)" },
               new Position { Id = 21, Name = "Frontend Developer" },
        new Position { Id = 22, Name = "Backend Developer" },
        new Position { Id = 23, Name = "Full Stack Developer" },
        new Position { Id = 24, Name = "Data Engineer" },
        new Position { Id = 25, Name = "AI Researcher" }
            );
        }
    }
}
