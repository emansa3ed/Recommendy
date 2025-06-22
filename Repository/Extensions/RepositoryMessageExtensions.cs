using Entities.Models;
using MSFC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryMessageExtensions
    {
        public static IQueryable<ChatMessage> Paging(this IQueryable<ChatMessage> ChatMessage, int PageNumber, int PageSize)
        {
            return ChatMessage
            .OrderByDescending(m=>m.Id)
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }
		public static IEnumerable<ChatMessage> Decrypt(this IEnumerable<ChatMessage> ChatMessage)
		{
            foreach (var m in ChatMessage)
			{
				var cipher = new MSFCipher().GetVernamCipher(Environment.GetEnvironmentVariable("SecretKey"));
				m.Message = Regex.Unescape(cipher.Decrypt(m.Message));
			}
			return ChatMessage;
		}
		public static ChatMessage Decrypt(this ChatMessage ChatMessage)
		{
			var cipher = new MSFCipher().GetVernamCipher(Environment.GetEnvironmentVariable("SecretKey"));
			ChatMessage.Message = cipher.Decrypt(ChatMessage.Message);
			return ChatMessage;
		}
		public static ChatMessage Encrypt(this ChatMessage ChatMessage)
		{
			var cipher = new MSFCipher().GetVernamCipher(Environment.GetEnvironmentVariable("SecretKey"));
			ChatMessage.Message = cipher.Encrypt(ChatMessage.Message);
			return ChatMessage;
		}
	}
}
