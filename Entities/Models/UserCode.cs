using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserCode
    {
        [Key]
        public int Id { get; set; } 
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate
        {
            get; set;
        }
    }

}
