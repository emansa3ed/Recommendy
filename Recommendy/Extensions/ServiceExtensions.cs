using Contracts;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;

namespace Recommendy.Extensions
{
    public  static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
         services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
      IConfiguration configuration) =>
     services.AddDbContext<RepositoryContext>(opts => 
     opts.UseSqlServer(configuration.GetConnectionString("connection")));
    }
}
