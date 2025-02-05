using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public  class UserCodeRepository : RepositoryBase<UserCode> , IUserCodeRepository
    {

        public UserCodeRepository( RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }


        public async Task AddAsync(UserCode userToken)
        {
            Create(userToken);
        }

        public async Task<UserCode> GetAsync(string  userId, string token)
        {
            return      FindByCondition(i=>i.UserId== userId && i.Token ==token ,false).AsNoTracking().FirstOrDefault();
                
                
               ;
        }

        public async Task DeleteAsync(UserCode userToken)
        {
            Delete(userToken);
        }

    }
}
