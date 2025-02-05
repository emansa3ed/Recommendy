using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record EmailRequestDto
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string? Reason { get; set; }
    }
}
