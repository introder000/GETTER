using Microsoft.Extensions.DependencyInjection;
using GETTER.Application.Common;
using GETTER.Application.Warehouses;
using GETTER.Infrastructure.Database;
using GETTER.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GETTER.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();

        return services;
    }
}
