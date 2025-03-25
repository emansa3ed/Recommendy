using Entities.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IChatUsersService
    {

        Task<ChatUsers> GetChatByUserIds(string FirstUserId ,string secondUserId);


    }
}
