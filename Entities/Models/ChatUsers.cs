using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ChatUsers
    {
        [Key]
        public int Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public User User { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
