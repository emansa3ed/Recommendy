
using Contracts;
using Recommendy.Extensions;
using Repository;


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
            builder.Services.AddScoped<IFileRepository, FileRepository>();

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
