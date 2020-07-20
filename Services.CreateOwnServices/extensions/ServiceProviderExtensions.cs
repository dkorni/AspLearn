using Microsoft.Extensions.DependencyInjection;
using Services.CreateOwnServices.services;

namespace Services.CreateOwnServices.extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
            services.AddTransient<TimeService>();
        }
    }
}
