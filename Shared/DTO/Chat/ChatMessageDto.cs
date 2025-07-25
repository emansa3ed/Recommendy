﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Chat
{
    public record ChatMessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public SenderDto Sender { get; set; }
    }
}