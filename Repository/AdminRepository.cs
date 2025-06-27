using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdminRepository : RepositoryBase<User>, IAdminRepository
    {
        private readonly UserManager<User> _userManager;

        public AdminRepository(RepositoryContext repositoryContext, UserManager<User> userManager)
            : base(repositoryContext)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<User>> GetUsersAsync(UsersParameters parameters, bool trackChanges)
        {
            var users = FindAll(trackChanges)
                .Include(u => u.University)
                    .ThenInclude(u => u.Country)
                .Include(u => u.Company)
                .Include(u => u.Student)
                .Where(u=>u.Discriminator != "Admin")
                .AsNoTracking();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(parameters.Role))
                users = users.Where(u => u.Discriminator == parameters.Role);

            if (parameters.IsBanned.HasValue)
                users = users.Where(u => u.IsBanned == parameters.IsBanned.Value);

            if (parameters.IsVerified.HasValue)
            {
                users = users.Where(u =>
                    (u.University != null && u.University.IsVerified == parameters.IsVerified.Value) ||
                    (u.Company != null && u.Company.IsVerified == parameters.IsVerified.Value));
            }

            //  Search functionality
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var searchTerm = parameters.SearchTerm.Trim().ToLower();
                users = users.Where(u =>
                    u.UserName.ToLower().Contains(searchTerm) ||
                    u.Email.ToLower().Contains(searchTerm) ||
                    (u.FirstName != null && u.FirstName.ToLower().Contains(searchTerm)) ||
                    (u.LastName != null && u.LastName.ToLower().Contains(searchTerm)) ||
                    (u.University != null && u.University.UniversityUrl.ToLower().Contains(searchTerm)) ||
                    (u.Company != null && u.Company.CompanyUrl.ToLower().Contains(searchTerm))
                );
            }

            // Apply ordering
            users = parameters.OrderBy?.ToLower() switch
            {
                "createdat" => users.OrderByDescending(u => u.CreatedAt),
                "username" => users.OrderBy(u => u.UserName),
                "name" => users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName),
                _ => users.OrderByDescending(u => u.CreatedAt)
            };

            return await PagedList<User>.CreateAsync(
                users,
                parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<User> GetUserByIdAsync(string userId, bool trackChanges) =>
            await FindByCondition(u => u.Id.Equals(userId), trackChanges)
                .Include(u => u.University)
                    .ThenInclude(u => u.Country)
                .Include(u => u.Company)
                .Include(u => u.Student)
                .SingleOrDefaultAsync();

        public async Task UpdateUserBanStatusAsync(User user) =>
            Update(user);
    }
}
