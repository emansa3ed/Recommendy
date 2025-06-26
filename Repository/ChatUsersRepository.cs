using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatUsersRepository : RepositoryBase<ChatUsers>, IChatUsersRepository
    {
        public ChatUsersRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateChatUsers(ChatUsers chatUsers) => Create(chatUsers);

        public IQueryable<ChatUsers> FindByCondition(Expression<Func<ChatUsers, bool>> expression, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<ChatUsers>().Where(expression).AsNoTracking()
                         : RepositoryContext.Set<ChatUsers>().Where(expression);

        public async Task<IEnumerable<ChatUsers>> GetAllChatsWithLastMessageForUser(string userId, bool trackChanges)
        {
            var res = await FindByCondition(c => c.FirstUserId == userId || c.SecondUserId == userId, trackChanges)
                .Include(c => c.Messages.OrderByDescending(m => m.CreatedAt).Take(1))
                .ToListAsync();
            foreach (var chat in res)
			{
                chat.Messages.Decrypt();
			}

			return res;
        }

        public async Task<ChatUsers> GetChatByUserIds(string FirstUserId, string secondUserId, bool trackchange)
        {
            return await FindByCondition(i => (i.FirstUserId == FirstUserId && i.SecondUserId == secondUserId) || 
                                            (i.FirstUserId == secondUserId && i.SecondUserId == FirstUserId), trackchange)
                .FirstOrDefaultAsync();
        }

        public async Task<ChatUsers> GetChatByChatId(int ChatId, bool trackchange)
        {
            return await FindByCondition(i => i.Id == ChatId, trackchange).FirstOrDefaultAsync();
        }
    }
}
