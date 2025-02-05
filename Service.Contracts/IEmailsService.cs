using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Contracts
{
    public interface IEmailsService
    {
        public Task<string> Sendemail(string email, string Message, string? reason);

    }
}
