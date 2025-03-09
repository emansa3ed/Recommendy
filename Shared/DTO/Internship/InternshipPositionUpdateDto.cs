using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Internship
{
    public record InternshipPositionUpdateDto
    {
        public string Requirements { get; set; }
        public int NumOfOpenings { get; set; }

    }
}
