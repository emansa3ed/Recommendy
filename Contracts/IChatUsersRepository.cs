using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IChatUsersRepository
    {
        void CreateChatUsers(ChatUsers chatUsers);

        Task<ChatUsers> GetChatByUserIds(string FirstUserId, string secondUserId, bool trackchange);
        Task<ChatUsers> GetChatByUserId(int ChatId, bool trackchange);



    }
}
