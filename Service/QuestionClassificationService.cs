using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Contracts;

namespace Service///////////////////  دا المنقذ حرفيا
{
    public class QuestionClassificationService : IQuestionClassificationService
    {
        private readonly HashSet<string> _irrelevantKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            "relationship", "dating", "marriage", "family", "personal", "private", "romance", "love", "name",
            

            "movie", "film", "music", "game", "gaming", "entertainment", "hobby", "sport", "sports",
            "travel", "vacation", "holiday", "party", "celebration", "fun", "entertainment",
            

            "health", "medical", "doctor", "hospital", "medicine", "treatment", "symptom", "disease",
            "illness", "pain", "fitness", "exercise", "diet", "nutrition", "weight", "mental health",
            

            "politics", "political", "religion", "religious", "belief", "faith", "god", "church", "president",
            "government", "election", "vote", "party", "ideology",
            

            "buy", "purchase", "shop", "shopping", "product", "price", "cost", "money", "expensive",
            "cheap", "discount", "sale", "store", "market", "brand", "fashion", "clothing",
            

            "weather", "temperature", "rain", "sun", "climate", "news", "current events", "breaking",
            "headline", "story", "event", "incident", "accident", "color",
            

            "joke", "funny", "humor", "comedy", "riddle", "puzzle", "trivia", "random", "weird",
            "strange", "unusual", "bizarre", "crazy", "silly", "stupid"
        };

        public QuestionType ClassifyQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return QuestionType.Irrelevant;

            var normalizedQuestion = question.ToLowerInvariant();
            
            if (IsLikelyCareerQuestion(normalizedQuestion))
            {
                return QuestionType.Relevant;
            }
            

            return QuestionType.Irrelevant;
        }

        private bool IsLikelyCareerQuestion(string question)
        {
            // First, check for definitely irrelevant patterns, like definition questions.
            if (question.StartsWith("what is ") || question.StartsWith("what are ") || question.StartsWith("define "))
            {
                // Instead of only checking the last word, check if the question contains any career-related keywords anywhere.
                var keywords = new[] { "internship", "intership", "intrnship", "job", "career", "scholarship", "sholarship", "opportunity", "opportunitiy", "opportunityies", "opportunities" };
                foreach (var keyword in keywords)
                {
                    if (question.Contains(keyword))
                        return true;
                }
                return false;
            }

            // Next, check for strong career-related patterns.
            var careerPatterns = new[]
            {
                // Catches 'what am I missing to become ...' questions
                @"what am i missing to become",
                // Catches preparation questions: "how do I prepare for a master's"
                @"\b(prepare\s+for|how\s+to\s+prepare\s+for)\s+(a|an|the)?\s+(masters?|phd|bachelors?|degree|job|career|internship|intership|intrnship|internshp|iternship|internhsip|opportunity|opportunitiy|opportunityies|opportunities|oportunity|oppotunity|oppertunity|oppurtunity|opporunity|scholarship|sholarship|scholrship|schlarship|schollarship)\b",

                // Catches intent + career noun: "how to get a job", "find an internship for ai"
                @"\b(become|get|find|apply|search|look|want|need|recommend|suggest)\s+.*?\s+((?:intern|intrn|inter|itern|internh|internsh|interns|intrnsh|internshp).?ship|job|career|work|position|role|(?:scholar|sholar|scholr|schlar|schollar).?ship|opportunit(?:y|ies|iy|yies|y|ies|i|e|ies|ity|itiy|ities|iies|eis|eit|eitiy|eitie|eitiy|eitiy|eitiy))\b",
                
                // Catches quality + career noun: "best internship for me"
                @"\b(best|good|top|recommended|suitable|perfect|ideal)\s+((?:intern|intrn|inter|itern|internh|internsh|interns|intrnsh|internshp).?ship|job|career|(?:scholar|sholar|scholr|schlar|schollar).?ship|opportunit(?:y|ies|iy|yies|y|ies|i|e|ies|ity|itiy|ities|iies|eis|eit|eitiy|eitie|eitiy|eitiy|eitiy))\b",

                // Catches simple noun queries: "AI internships", "python developer jobs"
                @"\b((backend|frontend|ai|python|java|c#)\s+)?((?:intern|intrn|inter|itern|internh|internsh|interns|intrnsh|internshp).?ship|job|career|work|position|role|(?:scholar|sholar|scholr|schlar|schollar).?ship|opportunit(?:y|ies|iy|yies|y|ies|i|e|ies|ity|itiy|ities|iies|eis|eit|eitiy|eitie|eitiy|eitiy|eitiy))s?\b",

                // Catches skill-related questions: "how to improve my skills"
                @"\b(improve|enhance|develop|learn|build)\s+(my)?\s+(skills?|skils|skil|skill|sklils|sills|sklls|career|profile)\b",

                // Catches general "what/how to" questions about careers: "what skills do I need"
                @"\b(what|how|where|when|which)\s+.*?\s+(skill|skills|skils|skil|skill|sklils|sills|sklls|course|degree|masters?|phd|bachelors?)\b"
            };

            return careerPatterns.Any(pattern => Regex.IsMatch(question, pattern, RegexOptions.IgnoreCase));
        }
    }
} 