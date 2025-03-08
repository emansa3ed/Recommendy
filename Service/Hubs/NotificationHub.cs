using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

public class NotificationHub : Hub
{
	private static readonly Dictionary<string, HashSet<string>> _connections = new();
	private static readonly object _lock = new();

	private readonly UserManager<User> _userManager;

	public NotificationHub(UserManager<User> userManager)
	{
		_userManager = userManager;
	}

	public override async Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		var id = httpContext.Request.Cookies.First();
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(id.Value); 

		var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
		if (userId != null)
		{
			lock (_lock)
			{
				if (!_connections.ContainsKey(userId))
				{
					_connections[userId] = new HashSet<string>();
				}
				_connections[userId].Add(Context.ConnectionId);
			}

		}
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var httpContext = Context.GetHttpContext();
		var id = httpContext.Request.Cookies.First();
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(id.Value);

		var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
		if (userId != null)
		{
			lock (_lock)
			{
				if (_connections.ContainsKey(userId))
				{
					_connections[userId].Remove(Context.ConnectionId);
					if (_connections[userId].Count == 0)
					{
						_connections.Remove(userId, out var x);
					}
				}
			}
		}
		await base.OnDisconnectedAsync(exception);
	}

	public async Task SendNotification(Notification notification)
	{
		List<string> connectionIds;

		lock (_lock)
		{
			if (!_connections.TryGetValue(notification.ReceiverID, out var connections))
			{
				return;
			}
			connectionIds = connections.ToList();
		}

		foreach (var connectionId in connectionIds)
		{
			await Clients.Client(connectionId).SendAsync("ReceiveNotification", notification);
		}
	}
	public static Dictionary<string, HashSet<string>> GetUsersConnections()
	{
		return _connections;
	}
}
