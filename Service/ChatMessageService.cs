using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Chat;
using Shared.DTO.Internship;
using Shared.DTO.Report;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

  public async Task<PagedList<ChatMessageDto>> GetChatMessages(int chatid, string CurrentUserId, string SecondUserId, MessageParameters messageParameters)
        {

            var user1 = await  _repositoryManager.User.GetById(SecondUserId);

            if (user1 == null)
                throw new UserNotFoundException(SecondUserId);


            if (SecondUserId.Equals(SecondUserId))
                throw new BadRequestException("the two ids is same ");

            var chat =  await _repositoryManager.ChatUsersRepository.GetChatByUserIds(CurrentUserId, SecondUserId, false);

             if(chat.Id != chatid)
                throw new InvalidChatIdBadRequestException(chatid);

            var messages = await _repositoryManager.ChatMessagesRepository.GetChatMessages(chatid, messageParameters,  false);
            List<ChatMessageDto> chatMessages = _mapper.Map<List<ChatMessageDto>>(messages);

            return new PagedList<ChatMessageDto>(chatMessages, messages.MetaData.TotalCount, messageParameters.PageNumber, messageParameters.PageSize);

        }



    }
}
