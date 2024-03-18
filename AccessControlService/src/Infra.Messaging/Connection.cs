using RabbitMQ.Client;

namespace Infra.Messaging;

public class Connection
{
    public static IModel Connect(string hostname)
    {
        var factory = new ConnectionFactory { HostName = hostname };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        if (channel is null)
            throw new Exception($"Does not possible connect to RabbitMQ on host: {hostname}");
        
        return channel;
    }
}