using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminRepository
    {
        Task<PagedList<User>> GetUsersAsync(UsersParameters parameters, bool trackChanges);
        Task<User> GetUserByIdAsync(string userId, bool trackChanges);
        Task UpdateUserBanStatusAsync(User user);
    }
}
