using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using Shared.DTO.Chat;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IChatMessageService
    {

        Task SendMessage(string UserId, string SecondUserId, int ChatId, string Message);
        Task<PagedList<ChatMessageDto>> GetChatMessages(int chatid, string FirstUserId, string SecondUserId, MessageParameters messageParameters);
    }
}
