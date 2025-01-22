
using Recommendy.Extensions;


namespace Recommendy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.ConfigureRepositoryManager(); ///manager
            builder.Services.ConfigureServiceManager();   /// service manager
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddControllers()
           .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

     

            var app = builder.Build();

            ///////// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

        
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
