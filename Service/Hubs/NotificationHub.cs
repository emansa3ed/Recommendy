using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DTO.Notification;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

public class NotificationHub : Hub
{
	private readonly INotificationService _notificationService;
	public NotificationHub(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	public async Task SendNotification(NotificationCreationDto notification)
	{
		await _notificationService.CreateNotificationAsync(notification);
		string jsonData = JsonSerializer.Serialize(notification);
		await Clients.User(notification.ReceiverID).SendAsync("ReceiveNotification", jsonData);
	}

}
