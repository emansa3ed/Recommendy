namespace Shared.DTO
{
    public class OrganizationProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? UrlPicture { get; set; }
        public string? Url { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string Type { get; set; } // "Company" or "University"
    }
} 