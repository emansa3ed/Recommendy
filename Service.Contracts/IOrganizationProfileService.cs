using Shared.DTO.Organization;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IOrganizationProfileService
    {
        Task<OrganizationProfileDto?> GetOrganizationProfileAsync(string id);
    }
} 