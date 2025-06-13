using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DTO.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Hubs
{
	public class MessageHub : Hub
	{
		public MessageHub()
		{
		}

		public async Task SendMessage(int ChatId, string Senderid, string ReceiverId, string Message)
		{

			string jsonData = JsonSerializer.Serialize(new {ChatId,Senderid,Message});

			await Clients.User(ReceiverId).SendAsync("ReceiveMessage", jsonData);

		}

	}
}
