using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        public UserService( IRepositoryManager repository,  UserManager<User> userManage) { 

            _repository = repository;
            _userManager = userManage;
           
        }

        public  async Task<User> GetDetails(string username)
        {
             var user =  await _userManager.FindByNameAsync(username);

            return user;
        }
    }
}
