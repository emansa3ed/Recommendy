using Entities.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.DTO.Chat;
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

        Task<ChatUsers> CreateChat(string FirstUserId, string seccondUserId);

        Task<IEnumerable<ChatUsers>> GetAllChatsForUser(string userId);

        Task<IEnumerable<ChatDto>> GetAllChatDtosForUser(string userId);
    }
}
