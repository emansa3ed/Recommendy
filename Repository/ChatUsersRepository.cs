using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatUsersRepository : RepositoryBase<ChatUsers> , IChatUsersRepository
    {
       public ChatUsersRepository( RepositoryContext repositoryContext ) : base( repositoryContext) { }


       public  void CreateChatUsers(ChatUsers chatUsers) => Create(chatUsers);

    }
}
