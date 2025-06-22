using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO.Chat;

namespace Service
{
    public class ChatUsersService : IChatUsersService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ChatUsersService(IRepositoryManager repositoryManager)
        { 
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<ChatUsers>> GetAllChatsForUser(string userId)
        {
            var user = await _repositoryManager.User.GetById(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            return await _repositoryManager.ChatUsersRepository.GetAllChatsWithLastMessageForUser(userId, false);
        }

        public async Task<ChatUsers> GetChatByUserIds(string FirstUserId, string CurrentUserId)
        {
            var user1 = await _repositoryManager.User.GetById(FirstUserId);

            if (user1 == null)
                throw new UserNotFoundException(FirstUserId);

            if(FirstUserId.Equals(CurrentUserId))
                throw new BadRequestException("the two ids is same ");

            var chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(FirstUserId, CurrentUserId, false);

            return chat;
        }

        public async Task<ChatUsers> CreateChat(string FirstUserId, string CurrentUserId)
        {
            ChatUsers chatUsers = new ChatUsers();

            chatUsers.FirstUserId = FirstUserId;
            chatUsers.SecondUserId = CurrentUserId;
            _repositoryManager.ChatUsersRepository.CreateChatUsers(chatUsers);
            await _repositoryManager.SaveAsync();

            var chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(FirstUserId, CurrentUserId, false);

            return chat;
        }

        public async Task<IEnumerable<ChatDto>> GetAllChatDtosForUser(string userId)
        {
            var chats = await GetAllChatsForUser(userId);

            var lastMessages = chats.Select(c => c.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault()).Where(m => m != null).ToList();
            var senderIds = lastMessages.Select(m => m.SenderId).Distinct().ToList();

            var otherUserIds = chats.Select(chat => chat.FirstUserId == userId ? chat.SecondUserId : chat.FirstUserId).Distinct().ToList();

            var senders = new List<User>();
            foreach (var id in senderIds)
            {
                var user = await _repositoryManager.User.GetById(id);
                if (user != null)
                    senders.Add(user);
            }
            var senderDict = senders.ToDictionary(u => u.Id, u => new Shared.DTO.Chat.SenderDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Photo = u.UrlPicture
            });
            var otherUsers = new List<User>();
            foreach (var id in otherUserIds)
            {
                var user = await _repositoryManager.User.GetById(id);
                if (user != null)
                    otherUsers.Add(user);
            }
            var otherUserDict = otherUsers.ToDictionary(u => u.Id, u => new Shared.DTO.Chat.SenderDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Photo = u.UrlPicture
            });
            var chatDtos = chats.Select(chat =>
            {
                var lastMessage = chat.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault();
                var otherUserId = chat.FirstUserId == userId ? chat.SecondUserId : chat.FirstUserId;
                return new ChatDto
                {
                    Id = chat.Id,
                    FirstUserId = chat.FirstUserId,
                    SecondUserId = chat.SecondUserId,
                    CreatedAt = chat.CreatedAt,
                    LastMessage = lastMessage == null ? null : new ChatMessageDto
                    {
                        Id = lastMessage.Id,
                        ChatId = lastMessage.ChatId,
                        SenderId = lastMessage.SenderId,
                        Message = lastMessage.Message,
                        CreatedAt = lastMessage.CreatedAt,
                        Sender = lastMessage.SenderId != null && senderDict.ContainsKey(lastMessage.SenderId)
                            ? senderDict[lastMessage.SenderId]
                            : null
                    },
                    OtherUser = otherUserDict.ContainsKey(otherUserId) ? otherUserDict[otherUserId] : null
                };
            }).ToList();
            return chatDtos;
        }
    }
}
