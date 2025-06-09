using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.University
{
    public record UniversityVerificationDto
    {
        public bool IsVerified { get; init; }
        public string? VerificationNotes { get; init; }
    }
}
