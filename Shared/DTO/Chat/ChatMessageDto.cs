using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Chat
{
    public record ChatMessageDto
    {
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string Message
        {
            get; set;

        }
    }
}