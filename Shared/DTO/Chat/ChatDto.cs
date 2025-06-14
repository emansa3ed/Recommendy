using System;
using System.Collections.Generic;

namespace Shared.DTO.Chat
{
    public record ChatDto
    {
        public int Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ChatMessageDto LastMessage { get; set; }
        public SenderDto OtherUser { get; set; }
    }
} 