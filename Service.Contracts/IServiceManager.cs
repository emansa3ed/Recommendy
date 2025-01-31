using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICountryService CountryService { get; }
        IAuthenticationService AuthenticationService { get; }
        IUserService UserService { get; }


    }
}
