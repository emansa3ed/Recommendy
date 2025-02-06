using Entities.Models;
using Shared.DTO;
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


    }
}
