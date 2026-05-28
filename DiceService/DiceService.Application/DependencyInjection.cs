using DiceService.Application.Interfaces;
using DiceService.Application.Requests;
using DiceService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiceService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddServices();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDiceRollService, DiceRollService>();

        return services;
    }
}
