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
using Entities.Exceptions;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailsService _emailsService;
        public UserService( IRepositoryManager repository,  UserManager<User> userManage , IMapper mapper , IEmailsService emailsService) { 

            _repository = repository;
            _userManager = userManage;
            _mapper = mapper;
            _emailsService = emailsService;
           
        }

        public  async Task<User> GetDetailsByUserName(string username)
        {
             var user =  await _userManager.FindByNameAsync(username);

            return user;
        }

        public async Task<UserDto> GetDetailsbyId(string Id)
        {
            var user = await  _repository.User.GetById(Id);
          var result = _mapper.Map< UserDto>(user);


            return result;
        }
        public async Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Password change failed.");
            }
            var subject = "Your Password Has Been Changed";
            var body = $@"
<p>Dear {user.UserName},</p>
<p>We want to inform you that your password has been successfully changed.</p>
<p>If you made this change, no further action is required.</p>
<p>If you did not request this change, please contact our support team immediately.</p>
<p>Best regards,<br>Your Support Team</p>";

            var email = await _emailsService.Sendemail(user.Email, body, subject);

        }
    }
}
