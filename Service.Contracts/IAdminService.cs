using Shared.RequestFeatures;
using Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO.Admin;

namespace Service.Contracts
{
    public interface IAdminService
    {
        Task<PagedList<UserDto>> GetUsersAsync(UsersParameters parameters, bool trackChanges);
        Task<UserDto> GetUserByIdAsync(string userId, bool trackChanges);
        Task BanUserAsync(string userId, UserBanDto banDto, bool trackChanges);
        Task UnbanUserAsync(string userId, bool trackChanges);
        Task DeleteUserAsync(string userId);
        Task<AdminDashboardStatsDto> GetDashboardStatisticsAsync();
    }
}
