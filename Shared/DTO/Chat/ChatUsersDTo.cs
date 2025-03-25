using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Chat
{
    public record ChatUsersDTo
    {
        [Required]
        public string secondUserId {  get; set; }


    }
}
