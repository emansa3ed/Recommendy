namespace Shared.DTO.opportunity
{
    public record WuzzufOpportunityDto
	{
		public string JobTitle { get; set; }
		public string CompanyName { get; set; }
		public string Location { get; set; }
		public string PostedAgo { get; set; }
		public string JobLink { get; set; }
		public string ImageUrl { get; set; }
	}
}
