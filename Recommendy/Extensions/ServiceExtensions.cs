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

namespace Recommendy.Extensions
{
    public  static class ServiceExtensions
    {



        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
       {

         options.AddPolicy("CorsPolicy", builder =>
         builder.WithOrigins("http://localhost:3000")
		.AllowAnyMethod()
        .AllowAnyHeader()
		.WithExposedHeaders("X-Pagination").AllowCredentials()); 
        });


        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
         services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
      IConfiguration configuration) =>
     services.AddDbContext<RepositoryContext>(opts => 
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
                    IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
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
							path.StartsWithSegments("/notificationHub"))
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
                               path.StartsWithSegments("/notificationHub"))
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
    }
}
