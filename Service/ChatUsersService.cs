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
    }
}
