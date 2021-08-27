using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Client.Services.Interfaces;

namespace PlannerApp.Client.Services
{
    public static class DependecyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection service)
        {
            return service.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                          .AddScoped<IPlansService, HttpPlansService>();
        }
    }
}