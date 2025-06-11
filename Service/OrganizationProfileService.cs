using Contracts;
using System.Threading.Tasks;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DTO.Organization;

namespace Service
{
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IRepositoryManager _repository;

        public OrganizationProfileService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<OrganizationProfileDto?> GetOrganizationProfileAsync(string id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);
            if (company != null)
            {
                return new OrganizationProfileDto
                {
                    Id = id,
                    Name = company.User.UserName,
                    Bio = company.User.Bio,
                    UrlPicture = company.User.UrlPicture,
                    Url = company.CompanyUrl,
                    PhoneNumber = company.User.PhoneNumber,
                    IsVerified = company.IsVerified,
                    Type = "Company"
                };
            }

            var university = await _repository.university.GetUniversityAsync(id, trackChanges: false);
            if (university != null)
            {
                return new OrganizationProfileDto
                {
                    Id = id,
                    Name = university.User.UserName,
                    Bio = university.User.Bio,
                    UrlPicture = university.User.UrlPicture,
                    Url = university.UniversityUrl,
                    PhoneNumber = university.User.PhoneNumber,
                    IsVerified = university.IsVerified,
                    Type = "University"
                };
            }

            throw new NotFoundOrganizationException(id);
        }
    }
} 