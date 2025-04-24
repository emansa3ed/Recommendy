using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.DTO.User;
using Shared.DTO.Student;
using Stripe;

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

        public  async Task<UserDto> GetDetailsByUserName(string username)
        {
            var user =  await _userManager.FindByNameAsync(username);
            if (user == null)
				throw new UserNotFoundException(username);
			return _mapper.Map<UserDto>(user); 
        }

        public async Task<UserDto> GetDetailsbyId(string Id)
        {
            var user = await  _repository.User.GetById(Id);
			if (user == null)
				throw new UserNotFoundException(Id);
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
<p>Best regards,<br> Recommendy Team </p>";

            var email = await _emailsService.Sendemail(user.Email, body, subject);

        }
        public async Task<string> UploadProfilePictureAsync(IFormFile file, string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
                throw new UserNotFoundException(Id);
            var imageUrl = await _repository.File.UploadImage("Uploads", file);
          
            user.UrlPicture = imageUrl;
            await _userManager.UpdateAsync(user);
             await _repository.SaveAsync();
            return imageUrl;
        }

		public async Task AddPremiumUserRoleAsync(string username, string SubscriptionID)
		{
			var user =  await _userManager.FindByNameAsync(username);
			if (user == null)
				throw new UserNotFoundException(username);
            user.SubscriptionId = SubscriptionID;
			var result = await _userManager.AddToRoleAsync(user, "PremiumUser");
			if (!result.Succeeded)
			{
				throw new BadRequestException("Role assignment failed.");
			}
            await _repository.SaveAsync();
		}

		public async Task<bool> IsInRoleAsync(string username, string roleName)
		{
			var user = await _userManager.FindByNameAsync(username);

			if (user == null)
				throw new UserNotFoundException(username);

			return await _userManager.IsInRoleAsync(user, "PremiumUser");
		}

		public async Task<UserDto> CancelSubscriptionInPremium(string username)
		{
			var user = await _userManager.FindByNameAsync(username);

			if (user == null)
				throw new UserNotFoundException(username);

			using (var transaction = await _repository.BeginTransactionAsync()) 
			{
				try
				{
                    if (user.SubscriptionId == null)
						throw new BadRequestException("User does not have an active subscription.");

					var result = await _userManager.RemoveFromRoleAsync(user, "PremiumUser");

					if (!result.Succeeded)
						throw new BadRequestException("Role removal failed.");

					var service = new SubscriptionService();
					await service.CancelAsync(user.SubscriptionId);

					user.SubscriptionId = null;

					await _repository.SaveAsync();

					await transaction.CommitAsync();
                    return _mapper.Map<UserDto>(user);
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();

					throw new Exception("Error occurred while canceling the subscription: " + ex.Message);
				}
			}

		}

		public async Task<UserDto> GetDetailsByUserEmail(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				throw new UserNotFoundException(email);
			return _mapper.Map<UserDto>(user);
		}
	}
}
