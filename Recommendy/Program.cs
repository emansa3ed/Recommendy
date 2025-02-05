
using Contracts;
using Recommendy.Extensions;
using Repository;
using Service.Contracts;
using Service;


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


            builder.Services.AddControllers()
           .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
              builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);



            var app = builder.Build();

            ///////// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}
