using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class UsersParameters : RequestParameters
    {
        public string? Role { get; init; }
        public string? SearchTerm { get; init; }
        public bool? IsBanned { get; init; }
        public bool? IsVerified { get; init; }
        public string? OrderBy { get; init; } 
    }
}
