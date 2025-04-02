using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnePiece.Application.Interfaces;
using OnePiece.Application.Services;

namespace OnePiece.Application;

public static class Dependencies
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITreasureHuntService, TreasureHuntService>();
    }
}
