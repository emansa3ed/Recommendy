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
	public static class RepositoryScholarshipExtensions
	{
		public static IQueryable<Scholarship> Paging(this IQueryable<Scholarship> Scholarship, int PageNumber, int PageSize)
		{
			return Scholarship
			.Skip((PageNumber - 1) * PageSize)
			.Take(PageSize);
		}
		public static IQueryable<Scholarship> Filter(this IQueryable<Scholarship> Scholarship, Funded? funded, Degree? degree) => (funded != null && degree != null)
			? Scholarship
			.Where(e => (e.Funded.Equals(funded) && e.Degree.Equals(degree)))
			: Scholarship;
		public static IQueryable<Scholarship> Search(this IQueryable<Scholarship> Scholarship, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return Scholarship;
			var lowerCaseTerm = searchTerm.Trim().ToLower();
			return Scholarship.Where(e => (e.Name.ToLower().Contains(lowerCaseTerm)));
		}
		public static IQueryable<Scholarship> Recommendation(this IQueryable<Scholarship> Scholarship, string Skills)
		{

			if (string.IsNullOrWhiteSpace(Skills))
				return Scholarship;

			var apiKey = Environment.GetEnvironmentVariable("GeminiKey");


			var geminiAgent = new GeminiChatAgent(
					name: "gemini",
					model: "gemini-1.5-flash",
					apiKey: apiKey,
					systemMessage: "just answer without any additional text.")
				.RegisterMessageConnector()
				.RegisterPrintMessage();
			var reply = geminiAgent.SendAsync($"Given the following skills: {Skills}," +
				$" identify the top 10 general titles that are commonly found in the name or description of relevant scholarships or internships." +
				$" Avoid combining skills with titles (e.g., avoid 'Python Developer'). Only return general," +
				$" role-based titles such as 'Developer', 'Analyst', or 'Researcher'." +
				$" Format the result exactly as: Title1, Title2, Title3, Title4, Title5, Title6, Title7, Title8, Title9, Title10." +
				$" Do not include any additional text or explanations.").Result;
			var jobTitles = reply.GetContent();
			if (string.IsNullOrWhiteSpace(jobTitles))
				return Scholarship;
			var terms = jobTitles
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => term.ToLower())
				.ToList();

			return Scholarship.Where(Scholarship =>
				terms.Any(term => Scholarship.Name.ToLower().Contains(term) || Scholarship.Description.ToLower().Contains(term)));
		}
	}
}
