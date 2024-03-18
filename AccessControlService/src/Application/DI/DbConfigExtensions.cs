using Domain.Repositories;

namespace Application.DI;

public static class DbConfigExtensions
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        const string dbConnection = "AccessControlDbConnection";
        var serviceProvider = services.BuildServiceProvider();

        var sessionFactory = PostgresSessionFactory
            .Factory(serviceProvider.GetService<ILogger<PostgresSessionFactory>>())
            .CreateSessionFactory(configuration.GetConnectionString(dbConnection));

        services.AddSingleton(sessionFactory);
        services.AddScoped<IUnitOfWorkDomain>(_ => new UnitOfWorkImpl(sessionFactory.OpenSession()));

        return services;
    }
}