using Microsoft.Extensions.DependencyInjection;
using Oraculo.Application.Interfaces;
using Oraculo.Application.UseCases;

namespace Oraculo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IGetPredictionIdUseCase, GetPredictionUseCase>();
        services.AddScoped<IGetMatchesIdUseCase, GetMatchesUseCase>();
        services.AddScoped<IUpdatePredictionUseCase, UpdatePredictionUseCase>();
        return services;
    }
}