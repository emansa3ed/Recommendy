using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class SkillsRepository : ISkillsRepository
	{
		private readonly List<string> _skills;

		public SkillsRepository()
		{
			// In a real application, this would be loaded from a database or file
			_skills = new List<string>
			{
				// Programming Languages
				"C#", "Java", "Python", "JavaScript", "TypeScript", "Ruby", "PHP", "Go", "Rust", "Swift",
				"Kotlin", "C++", "C", "Objective-C", "Scala", "Perl", "Shell", "PowerShell", "Bash",
				"Dart", "Julia", "R", "MATLAB", "F#", "Haskell", "Elm", "Lua",

				// Web Technologies
				"HTML", "CSS", "Sass", "Less", "React", "Angular", "Vue.js", "Svelte", "Next.js", "Nuxt.js",
				"Gatsby", "Astro", "Alpine.js", "Node.js", "Express", "ASP.NET", "ASP.NET Core",
				"Django", "Flask", "Spring", "Spring Boot", "jQuery", "Bootstrap", "Material UI", "Tailwind CSS",
				"Webpack", "Rollup", "Vite", "Parcel", "REST API", "GraphQL", "JSON", "XML", "SOAP",
				"WebSockets", "PWA", "SPA",

				// Databases
				"SQL", "MySQL", "PostgreSQL", "MariaDB", "Oracle", "SQL Server", "SQLite", "MongoDB",
				"Cassandra", "Redis", "DynamoDB", "Elasticsearch", "Neo4j", "CouchDB", "InfluxDB",
				"Hadoop", "Hive", "HBase", "Snowflake", "BigQuery", "Presto", "Firebase", "CosmosDB",

				// Cloud & DevOps
				"AWS", "Azure", "GCP", "AWS CloudFormation", "Azure DevOps", "Docker", "Kubernetes",
				"EKS", "ECS", "AKS", "Helm", "Terraform", "Ansible", "Chef", "Puppet", "Puppet",
				"Serverless", "Lambda", "Azure Functions", "Microservices", "CI/CD", "Jenkins",
				"GitLab CI/CD", "GitHub Actions", "CircleCI", "Travis CI", "Vagrant", "IaC", "Nomad", "Consul",

				// Development Tools & Practices
				"Git", "SVN", "Jira", "Confluence", "Agile", "Scrum", "Kanban", "DevSecOps", "TDD", "BDD",
				"SOLID", "Design Patterns", "OOP", "Functional Programming", "Pair Programming",
				"Code Review", "Unit Testing", "Integration Testing", "E2E Testing", "Selenium",
				"Cypress", "Prometheus", "Grafana", "ELK (Elasticsearch/Logstash/Kibana)",

				// Data Science & ML
				"Machine Learning", "Deep Learning", "TensorFlow", "PyTorch", "Keras", "scikit-learn",
				"Pandas", "NumPy", "Data Analysis", "Data Visualization", "NLP", "Computer Vision",
				"R", "MATLAB", "SAS", "XGBoost", "LightGBM", "Spark", "Hadoop", "Airflow", "Kubeflow",

				// Mobile
				"Android", "iOS", "React Native", "Flutter", "Xamarin", "Ionic", "Cordova", "SwiftUI",
				"Jetpack Compose", "Unity", "Unreal Engine",

				// Other Technical Skills
				"Linux", "Windows", "macOS", "Networking", "Security", "OAuth2", "JWT", "SAML", "OpenID Connect",
				"Blockchain", "Ethereum", "Solidity", "Hyperledger", "Kafka", "RabbitMQ", "MQTT", "Solr",
				"Ceph", "AR/VR", "IoT"
			};

		}

		public List<string> GetAllSkills()
		{
			return _skills;
		}
	}
}
