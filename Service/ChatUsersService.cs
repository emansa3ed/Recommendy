using Contracts;
using Entities.Exceptions;
using Entities.Models;
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
        public ChatUsersService( IRepositoryManager repositoryManager)
        { 
            _repositoryManager = repositoryManager;
        
        }


        public async Task<ChatUsers> GetChatByUserIds(string FirstUserId, string seccondUserId)
        {
            var user1 =  await _repositoryManager.User.GetById(FirstUserId);

            if (user1 == null)
                throw new UserNotFoundException(FirstUserId);



            var user2 =  await _repositoryManager.User.GetById(seccondUserId);

            if (user2 == null) 
                throw new UserNotFoundException(seccondUserId);

            var chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(FirstUserId, seccondUserId, false);

            if (chat == null)
            {
                ChatUsers chatUsers= new ChatUsers();

                chatUsers.FirstUserId = FirstUserId;
                chatUsers.SecondUserId = seccondUserId;
                _repositoryManager.ChatUsersRepository.CreateChatUsers(chatUsers);
               await  _repositoryManager.SaveAsync();
            }
            chat = await _repositoryManager.ChatUsersRepository.GetChatByUserIds(FirstUserId, seccondUserId, false);

            return chat;


        }
    }
}
