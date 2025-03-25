using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IChatMessagesRepository
    {
        void CreateMessage(ChatMessage chatMessage);

        void DeleteMessage(ChatMessage chatMessage);

        void UpdateMessage(ChatMessage chatMessage);


    }
}
