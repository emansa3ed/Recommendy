using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;
using Shared.DTO.opportunity;
using System.Text.Json;


namespace Service
{
	public class WebScrapingService : IWebScrapingService
	{

		MyMemoryCache _memoryCache;
		public WebScrapingService(MyMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}
		public async Task<List<WuzzufOpportunityDto>> GetWuzzufOpportunities(int PageNumber)
		{
			if (!_memoryCache.Cache.TryGetValue("GetWuzzufOpportunities" + PageNumber, out List<WuzzufOpportunityDto> opportunities))
			{
				opportunities = new();
				string url = $"https://wuzzuf.net/a/Internships-in-Egypt?page%5Bnumber%5D={PageNumber}";
				HttpClient httpClient = new HttpClient();
				var document = new HtmlDocument();

				var html = await httpClient.GetStringAsync(url);
				document.LoadHtml(html);

				var jobCards = document.DocumentNode.SelectNodes("//a[@class='css-19qsr80']");

				if (jobCards != null)
				{
					foreach (var jobCard in jobCards)
					{
						string jobTitle = jobCard.SelectSingleNode(".//h3[contains(@class,'css-3wv8cg')]/a")?.InnerText?.Trim();
						string jobLink = jobCard.GetAttributeValue("href", "").Trim();
						string companyName = jobCard.SelectSingleNode(".//a[contains(@class,'css-r5lqqx')]")?.InnerText?.Trim();
						string location = jobCard.SelectSingleNode(".//span[contains(@class,'css-ghensa')]")?.InnerText?.Trim();
						string postedAgo = jobCard.SelectSingleNode(".//span[contains(@class,'css-ahonbi')]")?.InnerText?.Trim();
						string imageUrl = jobCard.SelectSingleNode(".//img")?.GetAttributeValue("src", "").Trim();

						opportunities.Add(new WuzzufOpportunityDto
						{
							JobTitle = jobTitle,
							CompanyName = companyName,
							Location = location,
							PostedAgo = postedAgo,
							JobLink = string.IsNullOrEmpty(jobLink) ? null : $"https://wuzzuf.net{jobLink}",
							ImageUrl = imageUrl
						});
					}
				}

				var cacheEntryOptions = new MemoryCacheEntryOptions()
				.SetSize(JsonSerializer.SerializeToUtf8Bytes(opportunities).Length)
				.SetSlidingExpiration(TimeSpan.FromSeconds(240))
				.SetAbsoluteExpiration(TimeSpan.FromSeconds(320));

				_memoryCache.Cache.Set("GetWuzzufOpportunities" + PageNumber, opportunities, cacheEntryOptions);
			}
			return opportunities;
		}
	}
}
