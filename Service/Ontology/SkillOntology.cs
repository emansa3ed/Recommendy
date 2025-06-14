using Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Service.Ontology
{
    public class SkillOntology : ISkillOntology
    {
        // Map each skill to a set of related skills/concepts (including synonyms and hierarchy)
        public Dictionary<string, List<string>> RelatedSkills { get; } = new()
        {
            // Machine Learning & AI
            { "machine learning", new List<string> { "deep learning", "data science", "artificial intelligence", "ai", "python", "scikit-learn", "supervised learning", "unsupervised learning" } },
            { "deep learning", new List<string> { "machine learning", "neural networks", "tensorflow", "pytorch", "ai", "cnn", "rnn" } },
            { "ai", new List<string> { "artificial intelligence", "machine learning", "deep learning", "nlp", "computer vision" } },
            { "artificial intelligence", new List<string> { "ai", "machine learning", "deep learning" } },
            { "neural networks", new List<string> { "deep learning", "machine learning", "cnn", "rnn" } },
            { "cnn", new List<string> { "convolutional neural networks", "deep learning", "neural networks" } },
            { "rnn", new List<string> { "recurrent neural networks", "deep learning", "neural networks" } },
            { "nlp", new List<string> { "natural language processing", "ai", "machine learning", "text mining" } },
            { "computer vision", new List<string> { "ai", "image processing", "deep learning" } },
            { "scikit-learn", new List<string> { "machine learning", "python" } },
            { "tensorflow", new List<string> { "deep learning", "python" } },
            { "pytorch", new List<string> { "deep learning", "python" } },
            { "supervised learning", new List<string> { "machine learning" } },
            { "unsupervised learning", new List<string> { "machine learning" } },

            // Data Science & Analytics
            { "data science", new List<string> { "machine learning", "python", "statistics", "data analysis", "data mining", "big data" } },
            { "data analysis", new List<string> { "data science", "statistics", "excel", "data visualization" } },
            { "data mining", new List<string> { "data science", "big data" } },
            { "big data", new List<string> { "data science", "hadoop", "spark" } },
            { "statistics", new List<string> { "data science", "data analysis", "probability" } },
            { "data visualization", new List<string> { "tableau", "power bi", "matplotlib", "seaborn", "data analysis" } },
            { "tableau", new List<string> { "data visualization", "business intelligence" } },
            { "power bi", new List<string> { "data visualization", "business intelligence" } },
            { "matplotlib", new List<string> { "data visualization", "python" } },
            { "seaborn", new List<string> { "data visualization", "python" } },

            // Programming Languages
            { "python", new List<string> { "machine learning", "data science", "django", "flask", "pandas", "numpy" } },
            { "java", new List<string> { "spring", "android", "oop" } },
            { "c#", new List<string> { ".net", "asp.net", "unity" } },
            { "javascript", new List<string> { "react", "node.js", "frontend", "web development", "typescript" } },
            { "typescript", new List<string> { "javascript", "angular", "react" } },
            { "sql", new List<string> { "database", "mysql", "postgresql", "oracle", "sql server" } },
            { "mysql", new List<string> { "sql", "database" } },
            { "postgresql", new List<string> { "sql", "database" } },
            { "oracle", new List<string> { "sql", "database" } },
            { "sql server", new List<string> { "sql", "database" } },
            { "pandas", new List<string> { "python", "data analysis" } },
            { "numpy", new List<string> { "python", "data analysis" } },

            // Web Development
            { "react", new List<string> { "javascript", "frontend", "web development", "redux" } },
            { "angular", new List<string> { "typescript", "javascript", "frontend", "web development" } },
            { "vue", new List<string> { "javascript", "frontend", "web development" } },
            { "node.js", new List<string> { "javascript", "backend", "web development", "express" } },
            { "express", new List<string> { "node.js", "backend" } },
            { "frontend", new List<string> { "react", "angular", "vue", "javascript", "web development" } },
            { "backend", new List<string> { "node.js", "express", "django", "flask", "spring", "web development" } },
            { "web development", new List<string> { "frontend", "backend", "javascript", "react", "node.js", "html", "css" } },
            { "html", new List<string> { "web development", "frontend" } },
            { "css", new List<string> { "web development", "frontend" } },
            { "django", new List<string> { "python", "backend", "web development" } },
            { "flask", new List<string> { "python", "backend", "web development" } },

            // Cloud & DevOps
            { "aws", new List<string> { "cloud", "devops", "amazon web services" } },
            { "azure", new List<string> { "cloud", "devops", "microsoft azure" } },
            { "gcp", new List<string> { "cloud", "devops", "google cloud" } },
            { "docker", new List<string> { "devops", "containers", "kubernetes" } },
            { "kubernetes", new List<string> { "devops", "docker", "containers" } },
            { "devops", new List<string> { "ci/cd", "cloud", "docker", "kubernetes" } },
            { "ci/cd", new List<string> { "devops", "jenkins", "github actions", "gitlab ci" } },
            { "jenkins", new List<string> { "ci/cd", "devops" } },
            { "github actions", new List<string> { "ci/cd", "devops" } },
            { "gitlab ci", new List<string> { "ci/cd", "devops" } },

            // Mobile Development
            { "android", new List<string> { "java", "kotlin", "mobile development" } },
            { "ios", new List<string> { "swift", "objective-c", "mobile development" } },
            { "mobile development", new List<string> { "android", "ios", "react native", "flutter" } },
            { "react native", new List<string> { "react", "mobile development" } },
            { "flutter", new List<string> { "dart", "mobile development" } },

            // Business & Management
            { "project management", new List<string> { "scrum", "agile", "kanban" } },
            { "scrum", new List<string> { "agile", "project management" } },
            { "agile", new List<string> { "scrum", "kanban", "project management" } },
            { "kanban", new List<string> { "agile", "project management" } },
            { "business intelligence", new List<string> { "tableau", "power bi", "data analysis" } },

            // Security
            { "cybersecurity", new List<string> { "security", "network security", "information security" } },
            { "network security", new List<string> { "cybersecurity", "security" } },
            { "information security", new List<string> { "cybersecurity", "security" } },
            { "security", new List<string> { "cybersecurity", "network security", "information security" } }
        };

        public HashSet<string> ExpandSkills(IEnumerable<string> skills)
        {
            var expanded = new HashSet<string>(skills, System.StringComparer.OrdinalIgnoreCase);
            var toProcess = new Queue<string>(skills.Select(s => s.ToLower()));

            while (toProcess.Count > 0)
            {
                var skill = toProcess.Dequeue();
                if (RelatedSkills.TryGetValue(skill, out var related))
                {
                    foreach (var rel in related)
                    {
                        if (expanded.Add(rel)) // Only add if not already present
                            toProcess.Enqueue(rel.ToLower()); // Recursively expand
                    }
                }
            }
            return expanded;
        }
    }
} 