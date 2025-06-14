using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IChatUsersRepository : IRepositoryBase<ChatUsers>
    {
        void CreateChatUsers(ChatUsers chatUsers);

        Task<ChatUsers> GetChatByUserIds(string FirstUserId, string secondUserId, bool trackchange);
        Task<ChatUsers> GetChatByChatId(int ChatId, bool trackchange);
        IQueryable<ChatUsers> FindByCondition(Expression<Func<ChatUsers, bool>> expression, bool trackChanges);
        Task<IEnumerable<ChatUsers>> GetAllChatsWithLastMessageForUser(string userId, bool trackChanges);
    }
}
