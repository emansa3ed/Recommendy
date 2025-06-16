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
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.ExtendedProperties;
using Shared.DTO.Notification;

namespace Service
{
    public class ChatMessageService : IChatMessageService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

		private readonly INotificationService _notificationService;
        public ChatMessageService(IRepositoryManager repositoryManager, IMapper mapper, INotificationService notificationService) 
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task SendMessage(string senderId, string receiverId, int chatId, string message)
        {

            var Chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(senderId, receiverId, false);
            var receiver = await _repositoryManager.User.GetById(receiverId);
            if (Chat == null)
                throw new ChatNotFoundException(chatId);
            if (Chat.Id != chatId)
                throw new InvalidChatIdBadRequestException(chatId);

			var messageObj =new ChatMessage() { Message = message ,ChatId =chatId,CreatedAt = DateTime.Now,SenderId=senderId};
            _repositoryManager.ChatMessagesRepository.CreateMessage(messageObj);


            await _repositoryManager.SaveAsync();

            if (receiver.Discriminator == "Student")
            {
				await _notificationService.CreateNotificationAsync(new NotificationCreationDto
				{
					ActorID = senderId,
					ReceiverID = receiverId,
					Content = NotificationType.MessageSent,
				});
			}

        }

  public async Task<PagedList<ChatMessageDto>> GetChatMessages(int chatid, string CurrentUserId, string SecondUserId, MessageParameters messageParameters)
        {
            var user1 = await _repositoryManager.User.GetById(SecondUserId);
            if (user1 == null)
                throw new UserNotFoundException(SecondUserId);
            if (SecondUserId.Equals(CurrentUserId))
                throw new BadRequestException("the two ids is same ");
            var chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(CurrentUserId, SecondUserId, false);
            if (chat.Id != chatid)
                throw new InvalidChatIdBadRequestException(chatid);

            var messages = await _repositoryManager.ChatMessagesRepository.GetChatMessages(chatid, messageParameters, false);
            var senderIds = messages.Select(m => m.SenderId).Distinct().ToList();
            var senders = new List<User>();
            foreach (var id in senderIds)
            {
                var user = await _repositoryManager.User.GetById(id);
                if (user != null)
                    senders.Add(user);
            }
            var senderDict = senders.ToDictionary(u => u.Id, u => new SenderDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Photo = u.UrlPicture
            });

            var chatMessages = messages.Select(m => new ChatMessageDto
            {
                Id = m.Id,
                ChatId = m.ChatId,
                SenderId = m.SenderId,
                Message = m.Message,
                CreatedAt = m.CreatedAt,
                Sender = senderDict.ContainsKey(m.SenderId) ? senderDict[m.SenderId] : null
            }).ToList();

            return new PagedList<ChatMessageDto>(chatMessages, messages.MetaData.TotalCount, messageParameters.PageNumber, messageParameters.PageSize);
        }



    }
}
