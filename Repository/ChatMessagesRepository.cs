using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatMessagesRepository : RepositoryBase<ChatMessage>, IChatMessagesRepository
    {
        public ChatMessagesRepository (RepositoryContext context) : base (context) { }


       public void CreateMessage(ChatMessage chatMessage)=>Create(chatMessage);

      public void DeleteMessage(ChatMessage chatMessage) => Delete(chatMessage);
       public void UpdateMessage(ChatMessage chatMessage) => Update(chatMessage);



    }
}
