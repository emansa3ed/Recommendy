using Shared.DTO.opportunity;

namespace Service.Contracts
{
    public interface IWebScrapingService
    {
		public Task<List<WebScrapingOpportunityDto>> GetWuzzufOpportunities(int PageNumber);
	}
}
