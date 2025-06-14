using Azure;
using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;
using Shared.DTO.User;
using Stripe;
using Stripe.Checkout;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
	private readonly IServiceManager _service;

	public SubscriptionController(IServiceManager serviceManager)
	{
		_service = serviceManager;
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<ApiResponse<SubscriptionResponse>>> UserSubscription( [FromServices] IServiceProvider sp)
	{
		var user = await _service.UserService.GetDetailsByUserName(User.Identity.Name);
		
		if (await _service.UserService.IsInRoleAsync(user.UserName, "PremiumUser"))
			return BadRequest(new ApiResponse<SubscriptionResponse> { Success = false, Message = "User already has a subscription" });

		var referer = Request.Headers.Referer;

		var server = sp.GetRequiredService<IServer>();
		var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

		string? thisApiUrl = serverAddressesFeature?.Addresses.FirstOrDefault();

		if (thisApiUrl is not null)
		{
			var sessionId = await CreateSubscriptionSession(thisApiUrl, user);
			var pubKey = Environment.GetEnvironmentVariable("StripePublishableKey");

			var response = new SubscriptionResponse
			{
				SessionId = sessionId,
				PubKey = pubKey
			};

			return Ok(new ApiResponse<SubscriptionResponse> { Success =true,Data=response});
		}

		return BadRequest(new ApiResponse<SubscriptionResponse> { Success = false, Message="Faliled to get server url"});
	}

	[HttpGet("is-premium")]
	[Authorize]
	public async Task<ActionResult<ApiResponse<bool>>> IsPremiumUser()
	{
		var user = await _service.UserService.GetDetailsByUserName(User.Identity.Name);

		if (await _service.UserService.IsInRoleAsync(user.UserName, "PremiumUser"))
			return Ok(new ApiResponse<bool> { Success = true, Message = "User has a subscription",Data=true });

		return Ok(new ApiResponse<bool> { Success = true, Message = "User doesn't have a subscription", Data = false });
	}

	[NonAction]
	public async Task<string> CreateSubscriptionSession(string thisApiUrl,UserDto user)
	{

		var options = new SessionCreateOptions
		{
			SuccessUrl = $"{thisApiUrl}/Subscription/User/{user.Id}/Success?sessionId={{CHECKOUT_SESSION_ID}}",
			CancelUrl = $"{thisApiUrl}/Subscription/User/{user.Id}/Fail?sessionId={{CHECKOUT_SESSION_ID}}",
			PaymentMethodTypes = new List<string> { "card" },
			LineItems = new List<SessionLineItemOptions>
			{
				new()
				{
					Price =  Environment.GetEnvironmentVariable("PriceId"),
					Quantity = 1,
				}
			},
			Mode = "subscription",
			ClientReferenceId = user.Id,
			CustomerEmail = user.Email,
		};

		var service = new SessionService();
		var session = await service.CreateAsync(options);

		return session.Id;
	}

	[HttpGet("User/{UserId}/Success")]
	public async Task<ActionResult<ApiResponse<SubscriptionResponse>>> CheckoutSuccess([FromRoute] string UserId,[FromQuery] string sessionId)
	{
		var sessionService = new SessionService();
		var session = await sessionService.GetAsync(sessionId);

		if (session is null || session.ClientReferenceId != UserId)
			return BadRequest(new ApiResponse<SubscriptionResponse> { Success = false, Message = "Incorrect session sended" });

		var user = await _service.UserService.GetDetailsbyId(UserId);
		await _service.UserService.AddPremiumUserRoleAsync(user.UserName,session.SubscriptionId);



		var subject = "Your subscription is active now";
		var body = $@"
		<p>Dear {user.UserName},</p>
		<p>Thank you for subscribing to the Premium version!</p>
		<p>You now have full access to all premium features—enjoy the best we have to offer.</p>
		<p>If you have any questions or need help, feel free to reach out anytime.</p>
		<p>Best regards,<br> Recommendy Team </p>";

		_service.EmailsService.Sendemail(user.Email,body, subject);

		return Redirect($"http://localhost:3000/success?id={user.Id}");
	}

	[HttpGet("User/{UserId}/Fail")]
	public async Task<ActionResult<ApiResponse<SubscriptionResponse>>> CheckoutFail([FromRoute] string UserId, [FromQuery] string sessionId)
	{
		var sessionService = new SessionService();
		var session = sessionService.Get(sessionId);
		if (session is null || session.ClientReferenceId != UserId)
			return BadRequest(new ApiResponse<SubscriptionResponse> { Success = false, Message = "Incorrect session sended" });

		var user = await _service.UserService.GetDetailsbyId(UserId);

		var subject = "Subscription Failed";
		var body = $@"
		<p>Dear {user.UserName},</p>
		<p>We encountered an issue while processing your subscription to the Premium version.</p>
		<p>Please check your payment details and try again.</p>
		<p>If you believe this is a mistake or need assistance, feel free to contact our support team.</p>
		<p>We're here to help!</p>
		<p>Best regards,<br> Recommendy Team </p>";

		_service.EmailsService.Sendemail(user.Email, body, subject);

		return Redirect($"http://localhost:3000/fail?id={user.Id}");
	}

	[HttpDelete("Cancel")]
	[Authorize]

	public async Task<ActionResult> CancelUserSubscription()
	{
		var user = await _service.UserService.GetDetailsByUserName(User.Identity.Name);
		await _service.UserService.CancelSubscriptionInPremium(user.UserName);
		var subject = "Subscription Cancelled Successfully";
		var body = $@"
        <p>Dear {user.UserName},</p>
        <p>Your subscription to the Premium version has been successfully cancelled.</p>
        <p>If you have any questions or need further assistance, please feel free to contact our support team.</p>
        <p>We hope to have you back with us soon!</p>
        <p>Best regards,<br> Recommendy Team </p>";

		_service.EmailsService.Sendemail(user.Email, body, subject);
		return Ok();
	}


	[HttpPost("SubscriptionRenewed")]
	public async Task<IActionResult> SubscriptionRenewed()
	{
		var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

		var endpointSecret = "whsec_FjYwpOYo84H5YT90lmziqco8zUuCTSIM";

		Event stripeEvent;
		var stripeSignature = Request.Headers["Stripe-Signature"];
		stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret);

		var invoice = stripeEvent.Data.Object as Invoice;

		var customerEmail = invoice?.CustomerEmail;


		var user = await _service.UserService.GetDetailsByUserEmail(customerEmail);


		var subject = "Subscription Renewed Successfully";
		var body = $@"
		<p>Dear {user.UserName},</p>
		<p>Your subscription to the Premium version has been successfully renewed.</p>
		<p>If you have any questions or need further assistance, please feel free to contact our support team.</p>
		<p>Thank you for continuing to be a part of our service!</p>
		<p>Best regards,<br> Recommendy Team </p>";

		_service.EmailsService.Sendemail(user.Email, body, subject);


		return Ok();
	}

	[HttpPost("PaymentFailed")]
	public async Task<IActionResult> PaymentFailed()
	{
		var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

		var endpointSecret = "whsec_FycPWdYVgjUc5ocJ7TFuR5KRpxh7hhO8";

		Event stripeEvent;
		var stripeSignature = Request.Headers["Stripe-Signature"];
		stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret);

		var invoice = stripeEvent.Data.Object as Invoice;

		var customerEmail = invoice?.CustomerEmail;

		var user = await _service.UserService.GetDetailsByUserEmail(customerEmail);

		await _service.UserService.CancelSubscriptionInPremium(user.UserName);

		var subject = "Payment Failed for Your Subscription";
		var body = $@"
		<p>Dear {user.UserName},</p>
		<p>Unfortunately, your recent subscription payment has failed. This may be due to an expired or declined card.</p>
		<p>Please update your payment information to continue enjoying our Premium services.</p>
		<p>If you need any help, don't hesitate to reach out to our support team.</p>
		<p>Best regards,<br> Recommendy Team </p>";

		_service.EmailsService.Sendemail(user.Email, body, subject);

		return Ok();
	}



}
