using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recommendy.Migrations
{
    /// <inheritdoc />
    public partial class add_courses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDM9/B+XJ8SOWPryYUDPkzl8bo9tfM52qvgEEmmyswS4PN0oyi3vr2Qn+WhBXC3/Mg==");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseLink", "Description", "DifficultyLevel", "Duration", "Name", "Platform", "Skills" },
                values: new object[,]
                {
                    { 1, "https://udemy.com/python-for-beginners", "Learn Python from scratch. Perfect for beginners.", "Beginner", 20, "Python for Beginners", "Udemy", "python, programming, basics" },
                    { 2, "https://coursera.org/advanced-javascript", "Deep dive into advanced JavaScript concepts and ES6+ features.", "Advanced", 25, "Advanced JavaScript", "Coursera", "javascript, es6, web development" },
                    { 3, "https://edx.org/data-science-python", "Analyze data and build machine learning models using Python.", "Intermediate", 30, "Data Science with Python", "edX", "python, data science, machine learning" },
                    { 4, "https://udemy.com/web-development-bootcamp", "Become a full stack web developer with HTML, CSS, JS, and Node.js.", "Beginner", 40, "Web Development Bootcamp", "Udemy", "html, css, javascript, node.js, web development" },
                    { 5, "https://udemy.com/machine-learning-a-z", "Hands-on Python & R in Data Science. Learn ML algorithms.", "Intermediate", 35, "Machine Learning A-Z", "Udemy", "machine learning, python, r, data science" },
                    { 6, "https://udemy.com/react-the-complete-guide", "Build powerful, fast, user-friendly web apps with React.", "Intermediate", 28, "React - The Complete Guide", "Udemy", "react, javascript, frontend, web development" },
                    { 7, "https://coursera.org/sql-for-data-analysis", "Analyze data and build reports using SQL.", "Beginner", 15, "SQL for Data Analysis", "Coursera", "sql, data analysis, databases" },
                    { 8, "https://edx.org/devops-fundamentals", "Learn the basics of DevOps, CI/CD, Docker, and Kubernetes.", "Beginner", 18, "DevOps Fundamentals", "edX", "devops, ci/cd, docker, kubernetes" },
                    { 9, "https://udemy.com/cloud-computing-aws", "Master AWS cloud services and architecture.", "Advanced", 32, "Cloud Computing with AWS", "Udemy", "aws, cloud computing, devops" },
                    { 10, "https://coursera.org/cybersecurity-essentials", "Understand the fundamentals of cybersecurity and network security.", "Beginner", 22, "Cybersecurity Essentials", "Coursera", "cybersecurity, network security, information security" },
                    { 11, "https://edx.org/ai-intro", "Explore the basics of AI, neural networks, and intelligent systems.", "Beginner", 24, "Introduction to Artificial Intelligence", "edX", "ai, artificial intelligence, neural networks" },
                    { 12, "https://udemy.com/ios-app-development-swift", "Build iOS apps from scratch using Swift and Xcode.", "Intermediate", 27, "iOS App Development with Swift", "Udemy", "ios, swift, mobile development" },
                    { 13, "https://coursera.org/android-development-beginners", "Create Android apps using Java and Android Studio.", "Beginner", 21, "Android Development for Beginners", "Coursera", "android, java, mobile development" },
                    { 14, "https://edx.org/business-analysis-fundamentals", "Learn the key concepts and tools for business analysis.", "Beginner", 19, "Business Analysis Fundamentals", "edX", "business analysis, requirements, process modeling" },
                    { 15, "https://udemy.com/agile-project-management", "Master agile methodologies, Scrum, and Kanban for project management.", "Intermediate", 26, "Agile Project Management", "Udemy", "agile, scrum, kanban, project management" },
                    { 16, "https://coursera.org/unity-game-development", "Create 2D and 3D games using Unity and C#.", "Intermediate", 34, "Unity Game Development", "Coursera", "unity, c#, game development" },
                    { 17, "https://edx.org/tableau-data-visualization", "Visualize and analyze data using Tableau Desktop.", "Beginner", 16, "Tableau for Data Visualization", "edX", "tableau, data visualization, business intelligence" },
                    { 18, "https://udemy.com/linux-administration-essentials", "Manage Linux systems, users, and permissions.", "Intermediate", 23, "Linux Administration Essentials", "Udemy", "linux, system administration, bash" },
                    { 19, "https://coursera.org/digital-marketing-masterclass", "Learn SEO, SEM, social media, and email marketing.", "Beginner", 20, "Digital Marketing Masterclass", "Coursera", "digital marketing, seo, sem, social media" },
                    { 20, "https://edx.org/blockchain-cryptocurrency-explained", "Understand blockchain technology and cryptocurrencies.", "Advanced", 29, "Blockchain and Cryptocurrency Explained", "edX", "blockchain, cryptocurrency, distributed ledger" },
                    { 21, "https://udemy.com/docker-mastery", "Master Docker for DevOps and development.", "Intermediate", 19, "Docker Mastery", "Udemy", "docker, devops, containers" },
                    { 22, "https://coursera.org/kubernetes-for-developers", "Learn Kubernetes from scratch for developers.", "Advanced", 24, "Kubernetes for Developers", "Coursera", "kubernetes, devops, containers" },
                    { 23, "https://edx.org/power-bi-essentials", "Analyze and visualize data with Power BI.", "Beginner", 18, "Power BI Essentials", "edX", "power bi, data visualization, business intelligence" },
                    { 24, "https://udemy.com/spring-boot-in-action", "Build REST APIs with Spring Boot.", "Intermediate", 22, "Spring Boot in Action", "Udemy", "spring boot, java, rest api" },
                    { 25, "https://coursera.org/csharp-fundamentals", "Learn the basics of C# programming.", "Beginner", 17, "C# Fundamentals", "Coursera", "c#, programming, .net" },
                    { 26, "https://edx.org/aspnet-core-web-development", "Develop web apps with ASP.NET Core.", "Intermediate", 27, "ASP.NET Core Web Development", "edX", "asp.net core, c#, web development" },
                    { 27, "https://udemy.com/photoshop-for-beginners", "Learn Photoshop from scratch.", "Beginner", 14, "Photoshop for Beginners", "Udemy", "photoshop, design, graphics" },
                    { 28, "https://coursera.org/illustrator-masterclass", "Master Adobe Illustrator for design.", "Intermediate", 20, "Illustrator Masterclass", "Coursera", "illustrator, design, graphics" },
                    { 29, "https://edx.org/excel-data-analysis", "Analyze data using Microsoft Excel.", "Beginner", 13, "Excel Data Analysis", "edX", "excel, data analysis, spreadsheets" },
                    { 30, "https://udemy.com/financial-markets", "Understand the basics of financial markets.", "Beginner", 21, "Financial Markets", "Udemy", "finance, markets, investing" },
                    { 31, "https://coursera.org/ethical-hacking", "Learn ethical hacking and penetration testing.", "Advanced", 28, "Ethical Hacking", "Coursera", "ethical hacking, cybersecurity, penetration testing" },
                    { 32, "https://edx.org/big-data-analytics", "Analyze big data with Hadoop and Spark.", "Advanced", 33, "Big Data Analytics", "edX", "big data, hadoop, spark" },
                    { 33, "https://udemy.com/mobile-app-marketing", "Market your mobile apps effectively.", "Beginner", 12, "Mobile App Marketing", "Udemy", "mobile marketing, app store, advertising" },
                    { 34, "https://coursera.org/nodejs-api-development", "Build RESTful APIs with Node.js.", "Intermediate", 23, "Node.js API Development", "Coursera", "node.js, api, backend" },
                    { 35, "https://edx.org/vuejs-crash-course", "Get started with Vue.js for frontend development.", "Beginner", 16, "Vue.js Crash Course", "edX", "vue.js, javascript, frontend" },
                    { 36, "https://udemy.com/python-for-data-engineering", "Use Python for ETL and data pipelines.", "Intermediate", 29, "Python for Data Engineering", "Udemy", "python, data engineering, etl" },
                    { 37, "https://coursera.org/gcp-essentials", "Learn the basics of GCP.", "Beginner", 18, "Google Cloud Platform Essentials", "Coursera", "gcp, cloud, devops" },
                    { 38, "https://edx.org/sas-programming", "Learn SAS for data analysis.", "Intermediate", 22, "SAS Programming", "edX", "sas, data analysis, statistics" },
                    { 39, "https://udemy.com/r-for-data-science", "Analyze data and build models with R.", "Intermediate", 24, "R for Data Science", "Udemy", "r, data science, statistics" },
                    { 40, "https://coursera.org/penetration-testing-kali-linux", "Master penetration testing using Kali Linux.", "Advanced", 27, "Penetration Testing with Kali Linux", "Coursera", "penetration testing, kali linux, cybersecurity" },
                    { 41, "https://edx.org/azure-fundamentals", "Get started with Microsoft Azure cloud.", "Beginner", 20, "Microsoft Azure Fundamentals", "edX", "azure, cloud, devops" },
                    { 42, "https://udemy.com/jenkins-for-cicd", "Automate builds and deployments with Jenkins.", "Intermediate", 17, "Jenkins for CI/CD", "Udemy", "jenkins, ci/cd, devops" },
                    { 43, "https://coursera.org/git-github-essentials", "Learn version control with Git and GitHub.", "Beginner", 13, "Git & GitHub Essentials", "Coursera", "git, github, version control" },
                    { 44, "https://edx.org/scrum-master-certification-prep", "Prepare for Scrum Master certification.", "Intermediate", 21, "Scrum Master Certification Prep", "edX", "scrum, agile, project management" },
                    { 45, "https://udemy.com/python-for-finance", "Use Python for financial analysis and trading.", "Intermediate", 26, "Python for Finance", "Udemy", "python, finance, trading" },
                    { 46, "https://coursera.org/nosql-databases", "Learn about MongoDB, Cassandra, and NoSQL databases.", "Intermediate", 19, "NoSQL Databases", "Coursera", "nosql, mongodb, cassandra, databases" },
                    { 47, "https://edx.org/photoshop-advanced-techniques", "Master advanced Photoshop techniques.", "Advanced", 25, "Photoshop Advanced Techniques", "edX", "photoshop, design, graphics" },
                    { 48, "https://udemy.com/business-intelligence-qlikview", "Analyze data with QlikView.", "Intermediate", 18, "Business Intelligence with QlikView", "Udemy", "qlikview, business intelligence, data analysis" },
                    { 49, "https://coursera.org/swift-for-beginners", "Learn Swift programming for iOS development.", "Beginner", 14, "Swift for Beginners", "Coursera", "swift, ios, programming" },
                    { 50, "https://edx.org/tensorflow-deep-learning", "Build deep learning models with TensorFlow.", "Advanced", 31, "TensorFlow for Deep Learning", "edX", "tensorflow, deep learning, ai" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEB97pYsl/13yz6RQy86n3WL/APQoa9V6pocBkB35pUyYqs34l+QkvuRR32ouTVE+VA==");
        }
    }
}
