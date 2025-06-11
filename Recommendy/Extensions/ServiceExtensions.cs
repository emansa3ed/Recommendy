using Contracts;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Azure.Core;
using System.Threading.RateLimiting;
using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Recommendy.Extensions.Authorization;

namespace Recommendy.Extensions
{
    public  static class ServiceExtensions
    {



        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
       {

         options.AddPolicy("CorsPolicy", builder =>
		 builder.WithOrigins(new string[] { "http://localhost:3000", "http://127.0.0.1:5500" })
		.AllowAnyMethod()
        .AllowAnyHeader()
		.WithExposedHeaders("X-Pagination").AllowCredentials()); 
        });


        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureOrganizationProfileService(this IServiceCollection services) =>
         services.AddScoped<IOrganizationProfileService, OrganizationProfileService>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
         services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
      IConfiguration configuration) =>
     services.AddDbContextPool<RepositoryContext>(opts => 
     opts.UseSqlServer(configuration.GetConnectionString("connection")));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true; 
                
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretKey"))),
                        RoleClaimType = ClaimTypes.Role
                };

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
                        var c = context.Request;
						var accessToken = context.Request.Cookies["authToken"];

						var path = context.HttpContext.Request.Path;


						if (!string.IsNullOrEmpty(accessToken) &&
							( path.StartsWithSegments("/notificationHub") || path.StartsWithSegments("/feedbackHub") || path.StartsWithSegments("/messageHub")))
						{

							context.Token = accessToken;
                   
						}
						return Task.CompletedTask;
					},


					   OnTokenValidated = context =>
					   {
						       var path = context.HttpContext.Request.Path;

                               var accessToken = context.Request.Cookies["authToken"];

						   if (!string.IsNullOrEmpty(accessToken) &&
							   (path.StartsWithSegments("/notificationHub") || path.StartsWithSegments("/feedbackHub") || path.StartsWithSegments("/messageHub")))
						   {
                               var handler = new JwtSecurityTokenHandler();
                               var jwtToken = handler.ReadJwtToken(accessToken);
                               var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                               if (!string.IsNullOrEmpty(userId))
                               {
                                   if (context.Principal == null)
                                   {
                                       context.Principal = new ClaimsPrincipal();
                                   }

                                   context.Principal.AddIdentity(new ClaimsIdentity(new[]
                                   {
                                new Claim(ClaimTypes.NameIdentifier, userId)
                                }));
                               }
                           }
						   return Task.CompletedTask;
					   }


				};


			});
        }

		public static void ConfigureRateLimiter(this IServiceCollection services)
		{
			services.AddRateLimiter(options =>
			{
				options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
					RateLimitPartition.GetSlidingWindowLimiter(
						partitionKey: httpContext.User.Identity?.Name ??
						  httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
						  httpContext.Connection.RemoteIpAddress?.ToString() ??
						  "anonymous",
						factory: partition => new SlidingWindowRateLimiterOptions
						{
							AutoReplenishment = true,
							PermitLimit = 20,
							SegmentsPerWindow = 5,
							QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
							QueueLimit = 5,
							Window = TimeSpan.FromSeconds(10)
						}));

				options.OnRejected = async (context, cancellationToken) =>
				{
					context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

					var response = new ApiResponse<object>
					{
						Success = false,
						Message = "Rate limit exceeded. Please try again later.",
						Data = null
					};

					context.HttpContext.Response.ContentType = "application/json";
					await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken);


				};
			});
		}

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, VerifiedOrganizationHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("VerifiedOrganization", policy =>
                    policy.Requirements.Add(new VerifiedOrganizationRequirement()));
            });
        }
    }
}
