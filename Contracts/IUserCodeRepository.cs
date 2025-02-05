using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public  interface IUserCodeRepository
    {


        Task AddAsync(UserCode userToken);
        Task<UserCode> GetAsync(string  userId, string token);
        Task DeleteAsync(UserCode userToken);
    }
}
