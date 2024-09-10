using Festivos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Festivos.Application.Extentions
{
    public static class ApplicationServiceConfigurator
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            //Add FestivoService DI
            service.AddScoped<FestivosService>();
            return service;
        }
    }
}
