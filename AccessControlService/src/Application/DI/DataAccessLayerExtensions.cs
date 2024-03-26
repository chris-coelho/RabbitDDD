using Domain.Repositories;
using Infra.DataAccess.Repositories;

namespace Application.DI;

public static class DataAccessLayerExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}