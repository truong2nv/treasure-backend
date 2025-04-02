using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnePiece.Core.Interfaces;
using OnePiece.Infrastructure.Data;

namespace OnePiece.Infrastructure;

public static class Dependencies
{
    public static void ConfigureLocalDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OpDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

}
