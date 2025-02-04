using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using AutoMapper;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService( IRepositoryManager repository,  UserManager<User> userManage , IMapper mapper) { 

            _repository = repository;
            _userManager = userManage;
            _mapper = mapper;
           
        }

        public  async Task<User> GetDetailsByUserName(string username)
        {
             var user =  await _userManager.FindByNameAsync(username);

            return user;
        }

        public async Task<UserDto> GetDetailsbyId(string Id)
        {
            var user = await  _userManager.FindByIdAsync(Id);
           var result = _mapper.Map< UserDto>(user);
            return result;
        }
    }
}
