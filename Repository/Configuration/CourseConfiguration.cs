using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repository.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Python for Beginners",
                    Description = "Learn Python from scratch. Perfect for beginners.",
                    CourseLink = "https://udemy.com/python-for-beginners",
                    DifficultyLevel = "Beginner",
                    Platform = "Udemy",
                    Duration = 20,
                    Skills = "python, programming, basics"
                },
                new Course
                {
                    Id = 2,
                    Name = "Advanced JavaScript",
                    Description = "Deep dive into advanced JavaScript concepts and ES6+ features.",
                    CourseLink = "https://coursera.org/advanced-javascript",
                    DifficultyLevel = "Advanced",
                    Platform = "Coursera",
                    Duration = 25,
                    Skills = "javascript, es6, web development"
                },
                new Course
                {
                    Id = 3,
                    Name = "Data Science with Python",
                    Description = "Analyze data and build machine learning models using Python.",
                    CourseLink = "https://edx.org/data-science-python",
                    DifficultyLevel = "Intermediate",
                    Platform = "edX",
                    Duration = 30,
                    Skills = "python, data science, machine learning"
                },
                new Course
                {
                    Id = 4,
                    Name = "Web Development Bootcamp",
                    Description = "Become a full stack web developer with HTML, CSS, JS, and Node.js.",
                    CourseLink = "https://udemy.com/web-development-bootcamp",
                    DifficultyLevel = "Beginner",
                    Platform = "Udemy",
                    Duration = 40,
                    Skills = "html, css, javascript, node.js, web development"
                },
                new Course
                {
                    Id = 5,
                    Name = "Machine Learning A-Z",
                    Description = "Hands-on Python & R in Data Science. Learn ML algorithms.",
                    CourseLink = "https://udemy.com/machine-learning-a-z",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 35,
                    Skills = "machine learning, python, r, data science"
                },
                new Course
                {
                    Id = 6,
                    Name = "React - The Complete Guide",
                    Description = "Build powerful, fast, user-friendly web apps with React.",
                    CourseLink = "https://udemy.com/react-the-complete-guide",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 28,
                    Skills = "react, javascript, frontend, web development"
                },
                new Course
                {
                    Id = 7,
                    Name = "SQL for Data Analysis",
                    Description = "Analyze data and build reports using SQL.",
                    CourseLink = "https://coursera.org/sql-for-data-analysis",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 15,
                    Skills = "sql, data analysis, databases"
                },
                new Course
                {
                    Id = 8,
                    Name = "DevOps Fundamentals",
                    Description = "Learn the basics of DevOps, CI/CD, Docker, and Kubernetes.",
                    CourseLink = "https://edx.org/devops-fundamentals",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 18,
                    Skills = "devops, ci/cd, docker, kubernetes"
                },
                new Course
                {
                    Id = 9,
                    Name = "Cloud Computing with AWS",
                    Description = "Master AWS cloud services and architecture.",
                    CourseLink = "https://udemy.com/cloud-computing-aws",
                    DifficultyLevel = "Advanced",
                    Platform = "Udemy",
                    Duration = 32,
                    Skills = "aws, cloud computing, devops"
                },
                new Course
                {
                    Id = 10,
                    Name = "Cybersecurity Essentials",
                    Description = "Understand the fundamentals of cybersecurity and network security.",
                    CourseLink = "https://coursera.org/cybersecurity-essentials",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 22,
                    Skills = "cybersecurity, network security, information security"
                },
                // Additional 10 custom courses
                new Course
                {
                    Id = 11,
                    Name = "Introduction to Artificial Intelligence",
                    Description = "Explore the basics of AI, neural networks, and intelligent systems.",
                    CourseLink = "https://edx.org/ai-intro",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 24,
                    Skills = "ai, artificial intelligence, neural networks"
                },
                new Course
                {
                    Id = 12,
                    Name = "iOS App Development with Swift",
                    Description = "Build iOS apps from scratch using Swift and Xcode.",
                    CourseLink = "https://udemy.com/ios-app-development-swift",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 27,
                    Skills = "ios, swift, mobile development"
                },
                new Course
                {
                    Id = 13,
                    Name = "Android Development for Beginners",
                    Description = "Create Android apps using Java and Android Studio.",
                    CourseLink = "https://coursera.org/android-development-beginners",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 21,
                    Skills = "android, java, mobile development"
                },
                new Course
                {
                    Id = 14,
                    Name = "Business Analysis Fundamentals",
                    Description = "Learn the key concepts and tools for business analysis.",
                    CourseLink = "https://edx.org/business-analysis-fundamentals",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 19,
                    Skills = "business analysis, requirements, process modeling"
                },
                new Course
                {
                    Id = 15,
                    Name = "Agile Project Management",
                    Description = "Master agile methodologies, Scrum, and Kanban for project management.",
                    CourseLink = "https://udemy.com/agile-project-management",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 26,
                    Skills = "agile, scrum, kanban, project management"
                },
                new Course
                {
                    Id = 16,
                    Name = "Unity Game Development",
                    Description = "Create 2D and 3D games using Unity and C#.",
                    CourseLink = "https://coursera.org/unity-game-development",
                    DifficultyLevel = "Intermediate",
                    Platform = "Coursera",
                    Duration = 34,
                    Skills = "unity, c#, game development"
                },
                new Course
                {
                    Id = 17,
                    Name = "Tableau for Data Visualization",
                    Description = "Visualize and analyze data using Tableau Desktop.",
                    CourseLink = "https://edx.org/tableau-data-visualization",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 16,
                    Skills = "tableau, data visualization, business intelligence"
                },
                new Course
                {
                    Id = 18,
                    Name = "Linux Administration Essentials",
                    Description = "Manage Linux systems, users, and permissions.",
                    CourseLink = "https://udemy.com/linux-administration-essentials",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 23,
                    Skills = "linux, system administration, bash"
                },
                new Course
                {
                    Id = 19,
                    Name = "Digital Marketing Masterclass",
                    Description = "Learn SEO, SEM, social media, and email marketing.",
                    CourseLink = "https://coursera.org/digital-marketing-masterclass",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 20,
                    Skills = "digital marketing, seo, sem, social media"
                },
                new Course
                {
                    Id = 20,
                    Name = "Blockchain and Cryptocurrency Explained",
                    Description = "Understand blockchain technology and cryptocurrencies.",
                    CourseLink = "https://edx.org/blockchain-cryptocurrency-explained",
                    DifficultyLevel = "Advanced",
                    Platform = "edX",
                    Duration = 29,
                    Skills = "blockchain, cryptocurrency, distributed ledger"
                },

                // 30 more custom courses for a total of 50
                new Course
                {
                    Id = 21,
                    Name = "Docker Mastery",
                    Description = "Master Docker for DevOps and development.",
                    CourseLink = "https://udemy.com/docker-mastery",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 19,
                    Skills = "docker, devops, containers"
                },
                new Course
                {
                    Id = 22,
                    Name = "Kubernetes for Developers",
                    Description = "Learn Kubernetes from scratch for developers.",
                    CourseLink = "https://coursera.org/kubernetes-for-developers",
                    DifficultyLevel = "Advanced",
                    Platform = "Coursera",
                    Duration = 24,
                    Skills = "kubernetes, devops, containers"
                },
                new Course
                {
                    Id = 23,
                    Name = "Power BI Essentials",
                    Description = "Analyze and visualize data with Power BI.",
                    CourseLink = "https://edx.org/power-bi-essentials",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 18,
                    Skills = "power bi, data visualization, business intelligence"
                },
                new Course
                {
                    Id = 24,
                    Name = "Spring Boot in Action",
                    Description = "Build REST APIs with Spring Boot.",
                    CourseLink = "https://udemy.com/spring-boot-in-action",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 22,
                    Skills = "spring boot, java, rest api"
                },
                new Course
                {
                    Id = 25,
                    Name = "C# Fundamentals",
                    Description = "Learn the basics of C# programming.",
                    CourseLink = "https://coursera.org/csharp-fundamentals",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 17,
                    Skills = "c#, programming, .net"
                },
                new Course
                {
                    Id = 26,
                    Name = "ASP.NET Core Web Development",
                    Description = "Develop web apps with ASP.NET Core.",
                    CourseLink = "https://edx.org/aspnet-core-web-development",
                    DifficultyLevel = "Intermediate",
                    Platform = "edX",
                    Duration = 27,
                    Skills = "asp.net core, c#, web development"
                },
                new Course
                {
                    Id = 27,
                    Name = "Photoshop for Beginners",
                    Description = "Learn Photoshop from scratch.",
                    CourseLink = "https://udemy.com/photoshop-for-beginners",
                    DifficultyLevel = "Beginner",
                    Platform = "Udemy",
                    Duration = 14,
                    Skills = "photoshop, design, graphics"
                },
                new Course
                {
                    Id = 28,
                    Name = "Illustrator Masterclass",
                    Description = "Master Adobe Illustrator for design.",
                    CourseLink = "https://coursera.org/illustrator-masterclass",
                    DifficultyLevel = "Intermediate",
                    Platform = "Coursera",
                    Duration = 20,
                    Skills = "illustrator, design, graphics"
                },
                new Course
                {
                    Id = 29,
                    Name = "Excel Data Analysis",
                    Description = "Analyze data using Microsoft Excel.",
                    CourseLink = "https://edx.org/excel-data-analysis",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 13,
                    Skills = "excel, data analysis, spreadsheets"
                },
                new Course
                {
                    Id = 30,
                    Name = "Financial Markets",
                    Description = "Understand the basics of financial markets.",
                    CourseLink = "https://udemy.com/financial-markets",
                    DifficultyLevel = "Beginner",
                    Platform = "Udemy",
                    Duration = 21,
                    Skills = "finance, markets, investing"
                },
                new Course
                {
                    Id = 31,
                    Name = "Ethical Hacking",
                    Description = "Learn ethical hacking and penetration testing.",
                    CourseLink = "https://coursera.org/ethical-hacking",
                    DifficultyLevel = "Advanced",
                    Platform = "Coursera",
                    Duration = 28,
                    Skills = "ethical hacking, cybersecurity, penetration testing"
                },
                new Course
                {
                    Id = 32,
                    Name = "Big Data Analytics",
                    Description = "Analyze big data with Hadoop and Spark.",
                    CourseLink = "https://edx.org/big-data-analytics",
                    DifficultyLevel = "Advanced",
                    Platform = "edX",
                    Duration = 33,
                    Skills = "big data, hadoop, spark"
                },
                new Course
                {
                    Id = 33,
                    Name = "Mobile App Marketing",
                    Description = "Market your mobile apps effectively.",
                    CourseLink = "https://udemy.com/mobile-app-marketing",
                    DifficultyLevel = "Beginner",
                    Platform = "Udemy",
                    Duration = 12,
                    Skills = "mobile marketing, app store, advertising"
                },
                new Course
                {
                    Id = 34,
                    Name = "Node.js API Development",
                    Description = "Build RESTful APIs with Node.js.",
                    CourseLink = "https://coursera.org/nodejs-api-development",
                    DifficultyLevel = "Intermediate",
                    Platform = "Coursera",
                    Duration = 23,
                    Skills = "node.js, api, backend"
                },
                new Course
                {
                    Id = 35,
                    Name = "Vue.js Crash Course",
                    Description = "Get started with Vue.js for frontend development.",
                    CourseLink = "https://edx.org/vuejs-crash-course",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 16,
                    Skills = "vue.js, javascript, frontend"
                },
                new Course
                {
                    Id = 36,
                    Name = "Python for Data Engineering",
                    Description = "Use Python for ETL and data pipelines.",
                    CourseLink = "https://udemy.com/python-for-data-engineering",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 29,
                    Skills = "python, data engineering, etl"
                },
                new Course
                {
                    Id = 37,
                    Name = "Google Cloud Platform Essentials",
                    Description = "Learn the basics of GCP.",
                    CourseLink = "https://coursera.org/gcp-essentials",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 18,
                    Skills = "gcp, cloud, devops"
                },
                new Course
                {
                    Id = 38,
                    Name = "SAS Programming",
                    Description = "Learn SAS for data analysis.",
                    CourseLink = "https://edx.org/sas-programming",
                    DifficultyLevel = "Intermediate",
                    Platform = "edX",
                    Duration = 22,
                    Skills = "sas, data analysis, statistics"
                },
                new Course
                {
                    Id = 39,
                    Name = "R for Data Science",
                    Description = "Analyze data and build models with R.",
                    CourseLink = "https://udemy.com/r-for-data-science",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 24,
                    Skills = "r, data science, statistics"
                },
                new Course
                {
                    Id = 40,
                    Name = "Penetration Testing with Kali Linux",
                    Description = "Master penetration testing using Kali Linux.",
                    CourseLink = "https://coursera.org/penetration-testing-kali-linux",
                    DifficultyLevel = "Advanced",
                    Platform = "Coursera",
                    Duration = 27,
                    Skills = "penetration testing, kali linux, cybersecurity"
                },
                new Course
                {
                    Id = 41,
                    Name = "Microsoft Azure Fundamentals",
                    Description = "Get started with Microsoft Azure cloud.",
                    CourseLink = "https://edx.org/azure-fundamentals",
                    DifficultyLevel = "Beginner",
                    Platform = "edX",
                    Duration = 20,
                    Skills = "azure, cloud, devops"
                },
                new Course
                {
                    Id = 42,
                    Name = "Jenkins for CI/CD",
                    Description = "Automate builds and deployments with Jenkins.",
                    CourseLink = "https://udemy.com/jenkins-for-cicd",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 17,
                    Skills = "jenkins, ci/cd, devops"
                },
                new Course
                {
                    Id = 43,
                    Name = "Git & GitHub Essentials",
                    Description = "Learn version control with Git and GitHub.",
                    CourseLink = "https://coursera.org/git-github-essentials",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 13,
                    Skills = "git, github, version control"
                },
                new Course
                {
                    Id = 44,
                    Name = "Scrum Master Certification Prep",
                    Description = "Prepare for Scrum Master certification.",
                    CourseLink = "https://edx.org/scrum-master-certification-prep",
                    DifficultyLevel = "Intermediate",
                    Platform = "edX",
                    Duration = 21,
                    Skills = "scrum, agile, project management"
                },
                new Course
                {
                    Id = 45,
                    Name = "Python for Finance",
                    Description = "Use Python for financial analysis and trading.",
                    CourseLink = "https://udemy.com/python-for-finance",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 26,
                    Skills = "python, finance, trading"
                },
                new Course
                {
                    Id = 46,
                    Name = "NoSQL Databases",
                    Description = "Learn about MongoDB, Cassandra, and NoSQL databases.",
                    CourseLink = "https://coursera.org/nosql-databases",
                    DifficultyLevel = "Intermediate",
                    Platform = "Coursera",
                    Duration = 19,
                    Skills = "nosql, mongodb, cassandra, databases"
                },
                new Course
                {
                    Id = 47,
                    Name = "Photoshop Advanced Techniques",
                    Description = "Master advanced Photoshop techniques.",
                    CourseLink = "https://edx.org/photoshop-advanced-techniques",
                    DifficultyLevel = "Advanced",
                    Platform = "edX",
                    Duration = 25,
                    Skills = "photoshop, design, graphics"
                },
                new Course
                {
                    Id = 48,
                    Name = "Business Intelligence with QlikView",
                    Description = "Analyze data with QlikView.",
                    CourseLink = "https://udemy.com/business-intelligence-qlikview",
                    DifficultyLevel = "Intermediate",
                    Platform = "Udemy",
                    Duration = 18,
                    Skills = "qlikview, business intelligence, data analysis"
                },
                new Course
                {
                    Id = 49,
                    Name = "Swift for Beginners",
                    Description = "Learn Swift programming for iOS development.",
                    CourseLink = "https://coursera.org/swift-for-beginners",
                    DifficultyLevel = "Beginner",
                    Platform = "Coursera",
                    Duration = 14,
                    Skills = "swift, ios, programming"
                },
                new Course
                {
                    Id = 50,
                    Name = "TensorFlow for Deep Learning",
                    Description = "Build deep learning models with TensorFlow.",
                    CourseLink = "https://edx.org/tensorflow-deep-learning",
                    DifficultyLevel = "Advanced",
                    Platform = "edX",
                    Duration = 31,
                    Skills = "tensorflow, deep learning, ai"
                }
            };
            builder.HasData(courses);
        }
    }
} 