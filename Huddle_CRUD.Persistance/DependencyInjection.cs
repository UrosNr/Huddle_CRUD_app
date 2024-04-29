using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Huddle_CRUD.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HuddleInMemoryDbContext>(options => options.UseInMemoryDatabase(databaseName: "HuddleInMemoryDb"));
            services.AddScoped<IHuddleInMemoryDbContext>(provider => provider.GetService<HuddleInMemoryDbContext>());

            //Example of using a RDBMS (not in use inside the application we are using the in memory database for everything) 
            services.AddDbContext<HuddleDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IHuddleDbContext>(provider => provider.GetService<HuddleDbContext>());

            return services;
        }
    }
}