using AutoGen.Core;
using AutoGen.Gemini;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryInternshipExtensions
    {
        public static IQueryable<Internship> Filter(this IQueryable<Internship> internships, bool? paid)
        {
            if (paid.HasValue)
                return internships.Where(i => i.Paid == paid.Value);

            return internships; 
        }

        public static IQueryable<Internship> Paging(this IQueryable<Internship> internships, int PageNumber, int PageSize)
        {
            return internships
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }
		public static IQueryable<Internship> Search(this IQueryable<Internship> internships, string searchTerms)
		{
			if (string.IsNullOrWhiteSpace(searchTerms))
				return internships;

			var terms = searchTerms
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => term.ToLower())
				.ToList();

			return internships.Where(internship =>
				terms.Any(term => internship.Name.ToLower().Contains(term)));
		}


		public static IQueryable<Internship> Recommendation(this IQueryable<Internship> internships, string Skills)
		{

			if (string.IsNullOrWhiteSpace(Skills))
				return internships;

			var apiKey = "AIzaSyCA5lcOwz0bJZ7rGAxqiFZ_rimauFsRst4";


			var geminiAgent = new GeminiChatAgent(
					name: "gemini",
					model: "gemini-1.5-flash-001",
					apiKey: apiKey,
					systemMessage: "just answer without any additional text.")
				.RegisterMessageConnector()
				.RegisterPrintMessage();
			var reply =  geminiAgent.SendAsync($"Based on the skills: {Skills} list the top 5 matching job titles in this exact format: Job1, Job2, Job3, Job4, Job5. No additional text.").Result;

			var jobTitles = reply.GetContent();
			if (string.IsNullOrWhiteSpace(jobTitles))
				return internships;
			var terms = jobTitles
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => term.ToLower())
				.ToList();

			return internships.Where(internship =>
				terms.Any(term => internship.Name.ToLower().Contains(term) || internship.Description.ToLower().Contains(term)));
		}

	}

}
