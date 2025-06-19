using Contracts;
using Recommendy.Extensions;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.Extensions.Logging;
using Stripe;
using Entities.Models;
using Service.Hubs;
using Microsoft.OpenApi.Models;

namespace Recommendy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("StripeSecretKey");


			builder.Services.ConfigureRepositoryManager(); ///manager//////////////
            builder.Services.ConfigureOrganizationProfileService();
            builder.Services.ConfigureServiceManager();   /// service manager
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureCors();
            builder.Services.AddScoped<IEmailsService, EmailsService>();
            builder.Services.AddScoped<IResumeParserService, ResumeParserService>();
			builder.Services.AddScoped< IUserCodeService , UserCodeService > ();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
			builder.Services.AddHttpClient();
			builder.Services.AddSignalR();


			builder.Services.AddControllers()
           .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
           .ConfigureApiBehaviorOptions(
                options =>
                    options.SuppressModelStateInvalidFilter = true);


            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureAuthorization();  //new for verification task
            builder.Services.ConfigureJWT(builder.Configuration);
			builder.Services.AddSingleton<MyMemoryCache>();

            builder.Services.ConfigureRateLimiter();

			var app = builder.Build();
            app.UseCors("CorsPolicy");
            app.ConfigureExceptionHandler();

            ///////// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

			app.UseStaticFiles();
            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();
    

            app.MapControllers();
			app.MapHub<NotificationHub>("/notificationHub"); 
			app.MapHub<FeedbackHub>("/feedbackHub");
			app.MapHub<MessageHub>("/messageHub");


			app.Run();
        }
    }
}
