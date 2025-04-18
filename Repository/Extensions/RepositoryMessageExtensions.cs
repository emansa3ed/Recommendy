using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryMessageExtensions
    {
        public static IQueryable<ChatMessage> Paging(this IQueryable<ChatMessage> ChatMessage, int PageNumber, int PageSize)
        {
            return ChatMessage
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }
       
    }
}
