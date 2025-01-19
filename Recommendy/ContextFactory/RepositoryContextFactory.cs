using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Recommendy.ContextFactory
{
    
        public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
        {
            public RepositoryContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

                

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(configuration.GetConnectionString("Connection"),
              b => b.MigrationsAssembly("Recommendy"));

            return new RepositoryContext(builder.Options);
            }
        }
    
}
