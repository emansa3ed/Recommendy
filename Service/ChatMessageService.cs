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

        public async Task SendMessage(int chatId, ChatMessageDto chatMessageDto)
        {

            var Chat = await _repositoryManager.ChatUsersRepository.GetChatByUserId(chatId, false);
            if (Chat == null)
                throw new ChatNotFoundException(chatId);

            if (chatMessageDto.SenderId != Chat.SecondUserId && chatMessageDto.SenderId!=Chat.FirstUserId)
                throw new BadRequestException("not allowed send messages in this chat ");

            var message = _mapper.Map < ChatMessage>(chatMessageDto);
            message.ChatId = chatId;    
            _repositoryManager.ChatMessagesRepository.CreateMessage(message);

            await _repositoryManager.SaveAsync();

        }


    }
}
