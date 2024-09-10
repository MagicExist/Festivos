using Festivos.API.Models;
using Festivos.Domain.Repository;
using Festivos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Festivos.Persistence.Extensions
{
    public static class PersistenceServiceConfigurator
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration) 
        {
            //Add Db EntityFramework DI
            services.AddDbContext<FestivosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            //Add Repository DI
            services.AddScoped<IFestivoRepository, FestivoRepository>();
            return services;
        }
    }
}
