
using Contracts;
using Recommendy.Extensions;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.Extensions.Logging;


namespace Recommendy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.ConfigureRepositoryManager(); ///manager//////////////
            builder.Services.ConfigureServiceManager();   /// service manager
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureCors();
            builder.Services.AddScoped<IEmailsService, EmailsService>();
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
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);
			builder.Services.AddSingleton<MyMemoryCache>();



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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
			app.MapHub<NotificationHub>("/notificationHub"); 


			app.Run();
        }
    }
}
