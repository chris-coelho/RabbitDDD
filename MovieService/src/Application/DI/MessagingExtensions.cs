using Application.Messaging;
using Application.Messaging.EventHandlers;
using Common.Messaging;
using Infra.Messaging;

namespace Application.DI;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessagingConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerUri = configuration.GetConnectionString("MessageBrokerConnection");
        if (string.IsNullOrWhiteSpace(messageBrokerUri))
            throw new Exception("Unable to get the connection string to connect on message broker");

        services.AddSingleton<IMessagingConfig, RabbitMqConfiguration>();
        services.AddScoped<IMessagingSender, RabbitMqProducer>();

        var serviceProvider = services.BuildServiceProvider();
        var broker = serviceProvider.GetService<IMessagingConfig>();
        if (broker is null)
            throw new Exception("Unable to get service IMessagingConfig from service provider");

        broker.Connect(messageBrokerUri);

        // Queue declarations
        broker.RegisterQueue(MessagingConstants.UsernameAccountChangedQueue);
        
        // Consumer Declarations
        var messagingScope = serviceProvider.GetService<IServiceScopeFactory>();
        var consumer = broker.GetConsumer(MessagingConstants.UsernameAccountChangedQueue);
        consumer.Received += new AccountUsernameChangedEventHandler(messagingScope).Handle;

        return services;
    }
}