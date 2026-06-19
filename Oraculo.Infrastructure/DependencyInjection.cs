using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Infrastructure.Persistence;
using Oraculo.Infrastructure.Persistence.Repositories;

namespace Oraculo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {        
        string connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "Persistence", "OraculoDB")}";
        services.AddDbContext<OraculoContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IPredictionsRepository, PredictionsRepository>();
        services.AddScoped<IMatchesRepository, MatchesRepository>();

        return services;
    }
}