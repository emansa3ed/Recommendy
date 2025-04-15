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
		private readonly IServiceManager _serviceManager;
		public MessageHub(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		public async Task SendMessage(int ChatId,  string UserId, string Message)
		{
			var CurrentUser = await _serviceManager.UserService.GetDetailsByUserName(Context.User.Identity.Name);

			var Senderid = CurrentUser.Id;

			await _serviceManager.ChatMessageService.SendMessage(Senderid, UserId, ChatId, Message);

			string jsonData = JsonSerializer.Serialize(new {ChatId,Senderid,Message});

			await Clients.User(UserId).SendAsync("ReceiveMessage", jsonData);

		}

	}
}
