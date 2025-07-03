using Shared.DTO.opportunity;

namespace Service.Contracts
{
    public interface IWebScrapingService
    {
		public Task<List<WuzzufOpportunityDto>> GetWuzzufOpportunities(int PageNumber);
	}
}
