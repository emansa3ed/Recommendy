using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatMessagesRepository : RepositoryBase<ChatMessage>, IChatMessagesRepository
    {
        public ChatMessagesRepository(RepositoryContext context) : base(context) { }


        public void CreateMessage(ChatMessage chatMessage) {
            chatMessage.Encrypt();
            Create(chatMessage);
		} 

        public void DeleteMessage(ChatMessage chatMessage) => Delete(chatMessage);
        public void UpdateMessage(ChatMessage chatMessage) => Update(chatMessage);


        public async Task<PagedList<ChatMessage>> GetChatMessages(int chatid, MessageParameters messageParameters, bool trackChanges)
        {
            var messages = await FindByCondition(i => i.ChatId == chatid, trackChanges)
                .Paging(messageParameters.PageNumber, messageParameters.PageSize)
                .OrderBy(e => e.CreatedAt)
                .ToListAsync();
            messages.Decrypt();
            var count = await FindByCondition(i => i.ChatId == chatid, trackChanges).CountAsync();
            return new PagedList<ChatMessage>(messages, count, messageParameters.PageNumber, messageParameters.PageSize);
        }



    }
}
