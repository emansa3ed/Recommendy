using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Chat;
using Shared.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ChatMessageService : IChatMessageService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ChatMessageService(IRepositoryManager repositoryManager  ,IMapper mapper) 
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        
        }

        public async Task SendMessage(string UserId,string SecondUserId, int ChatId, string Message)
        {

            var Chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(UserId, SecondUserId, false);
            if (Chat == null)
                throw new ChatNotFoundException(ChatId);
            if (Chat.Id != ChatId)
                throw new InvalidChatIdBadRequestException(ChatId);

			var message =new ChatMessage() { Message = Message ,ChatId =ChatId,CreatedAt = DateTime.Now,SenderId=UserId};
            _repositoryManager.ChatMessagesRepository.CreateMessage(message);

            await _repositoryManager.SaveAsync();

        }


    }
}
