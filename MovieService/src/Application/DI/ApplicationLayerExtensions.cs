using Common.Application.NotificationPattern;

namespace Application.DI;

public static class ApplicationLayerExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationContext, NotificationContext>();

        #region Commands
        #endregion

        #region Queries
        #endregion

        return services;
    }
}