using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Contracts;

namespace Service
{
    public class QuestionClassificationService : IQuestionClassificationService
    {
        private readonly HashSet<string> _careerKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Career and professional development
            "career", "job", "work", "employment", "profession", "professional", "career path", "career advice",
            "resume", "cv", "interview", "application", "hire", "hiring", "employment", "workplace",
            
            // Education and learning
            "study", "learn", "education", "academic", "university", "college", "school", "course", "degree",
            "major", "minor", "gpa", "grade", "academic", "scholarship", "financial aid", "tuition",
            
            // Skills and development
            "skill", "skill development", "training", "certification", "certificate", "qualification",
            "experience", "expertise", "knowledge", "learning", "development", "improve", "enhance",
            
            // Internships and opportunities
            "internship", "intern", "opportunity", "position", "role", "placement", "work experience",
            "practical", "hands-on", "real-world", "industry", "company", "organization",
            
            // Technology and specific fields
            "programming", "coding", "software", "development", "engineering", "data science", "ai", "ml",
            "machine learning", "artificial intelligence", "web development", "mobile", "database",
            "cloud", "devops", "cybersecurity", "network", "system", "application", "backend", "frontend",
            "full stack", "developer", "programmer", "coder", "engineer", "architect", "analyst",
            
            // Business and management
            "business", "management", "leadership", "project", "team", "communication", "marketing",
            "finance", "accounting", "economics", "strategy", "planning", "analysis",
            
            // General career guidance
            "advice", "guidance", "help", "suggest", "recommend", "what should", "how to", "tips",
            "best practice", "strategy", "plan", "goal", "objective", "future", "aspiration",
            
            // Common career-related words
            "become", "get", "find", "apply", "search", "look", "want", "need", "best", "good", "great",
            "opportunity", "chance", "way", "path", "road", "journey", "start", "begin", "enter"
        };

        private readonly HashSet<string> _irrelevantKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Personal/private topics
            "relationship", "dating", "marriage", "family", "personal", "private", "romance", "love",
            
            // Entertainment and leisure
            "movie", "film", "music", "game", "gaming", "entertainment", "hobby", "sport", "sports",
            "travel", "vacation", "holiday", "party", "celebration", "fun", "entertainment",
            
            // Health and medical (unless career-related)
            "health", "medical", "doctor", "hospital", "medicine", "treatment", "symptom", "disease",
            "illness", "pain", "fitness", "exercise", "diet", "nutrition", "weight", "mental health",
            
            // Politics and religion
            "politics", "political", "religion", "religious", "belief", "faith", "god", "church",
            "government", "election", "vote", "party", "ideology",
            
            // Shopping and consumer
            "buy", "purchase", "shop", "shopping", "product", "price", "cost", "money", "expensive",
            "cheap", "discount", "sale", "store", "market", "brand", "fashion", "clothing",
            
            // Weather and current events
            "weather", "temperature", "rain", "sun", "climate", "news", "current events", "breaking",
            "headline", "story", "event", "incident", "accident",
            
            // Random questions
            "joke", "funny", "humor", "comedy", "riddle", "puzzle", "trivia", "random", "weird",
            "strange", "unusual", "bizarre", "crazy", "silly", "stupid"
        };

        public QuestionType ClassifyQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return QuestionType.Irrelevant;

            var normalizedQuestion = question.ToLowerInvariant();
            
            bool hasCareerKeywords = _careerKeywords.Any(keyword => normalizedQuestion.Contains(keyword));
            bool hasIrrelevantKeywords = _irrelevantKeywords.Any(keyword => normalizedQuestion.Contains(keyword));

            if (hasCareerKeywords)
            {
                return QuestionType.Relevant;
            }

            if (IsLikelyCareerQuestion(normalizedQuestion))
            {
                return QuestionType.Relevant;
            }

            if (hasIrrelevantKeywords)
            {
                return QuestionType.Irrelevant;
            }

            // Default to irrelevant if we have no signals
            return QuestionType.Irrelevant;
        }

        private bool IsLikelyCareerQuestion(string question)
        {
            var careerPatterns = new[]
            {
                @"\b(become|get|find|apply|search|look|want|need)\b",
                @"\b(internship|job|career|work|position|role)\b",
                @"\b(developer|engineer|programmer|coder|analyst|architect)\b",
                @"\b(backend|frontend|full.?stack|software|web|mobile)\b",
                @"\b(best|good|great|top|recommended)\b",
                @"\b(what|how|where|when|which)\s+(internship|job|career|position)\b"
            };

            return careerPatterns.Any(pattern => Regex.IsMatch(question, pattern, RegexOptions.IgnoreCase));
        }
    }
} 