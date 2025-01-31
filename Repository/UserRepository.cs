using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {

        private readonly UserManager<User> _userManager;
        public UserRepository( RepositoryContext repositoryContext, UserManager<User> userManager) 
            : base (repositoryContext) { 
            _userManager = userManager;
        
        } 

        public async Task<string> GetType( string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);


            return roles.First();

        }
        
    }
}
