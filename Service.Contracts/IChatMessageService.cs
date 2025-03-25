using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using Shared.DTO.Chat;

namespace Service.Contracts
{
    public interface IChatMessageService
    {

        Task SendMessage(int ChatId, ChatMessageDto chatMessageDto);
    }
}
