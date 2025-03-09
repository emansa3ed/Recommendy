using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DTO.Student;
using Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public  interface IUserService
    {
        Task<User> GetDetailsByUserName(string username);
        Task<UserDto> GetDetailsbyId(string Id);
        Task ChangePasswordAsync(string studentId, ChangePasswordDto changePasswordDto);
        Task<string> UploadProfilePictureAsync(IFormFile file, string Id);


    }
}
