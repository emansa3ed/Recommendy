using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shared.DTO.User
{
    public record UserDto
    {
        public string Id { get; init; }
        public string phoneNumber { get; set; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Discriminator { get; init; }
        public bool EmailConfirmed { get; init; }
        public string UrlPicture { get; init; }
        public string Bio { get; init; }
        public bool IsBanned { get; init; }
        public DateTime CreatedAt { get; init; }
        public bool IsVerified { get; init; }


        // Additional properties specific to each role
        public string? UniversityUrl { get; init; }
        public string? CompanyUrl { get; init; }
        public string? CountryName { get; init; }
    }

    public record UserBanDto
    {
        public bool IsBanned { get; init; }
        public string? Reason { get; init; }
    }

}
