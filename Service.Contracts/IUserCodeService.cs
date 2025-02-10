using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
     public interface IUserCodeService
    {

        Task<string> GenerateUserCodeForConfirmtationAsync(string Userid);
        Task<string> GenerateUserCodeForResetPasswordAsync(string UserId);

    }
}
