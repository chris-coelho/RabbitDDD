using Common.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Infra.Messaging;

public class RabbitMqConfiguration : IMessagingConfig
{
    private readonly ILogger<RabbitMqConfiguration> _logger;

    private static IConnection? _connection;
    private static IModel? _channel;
    private static string _uri;

    public RabbitMqConfiguration(ILogger<RabbitMqConfiguration> logger)
    {
        _logger = logger;
    }

    public void Connect(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new ArgumentException("URI is required to create a Rabbit MQ Management object");

        _uri = uri;
        
        if (_connection != null && _connection.IsOpen)
            return;
        
        var factory = new ConnectionFactory { Uri = new Uri(_uri) };

        try
        {
            _logger.LogInformation($"Connecting to Rabbit MQ broker on {_uri}...");
            _connection = factory.CreateConnection();
            _logger.LogInformation($"Rabbit MQ broker {_uri} has been successfully connected.");
        }
        catch (BrokerUnreachableException e)
        {
            throw new Exception($"Unable to connect to {_uri}. Details: {e.Message}");
        }
        catch (RabbitMQClientException e)
        {
            throw new Exception($"Unable to connect to {_uri}. Details: {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to connect to {_uri}. Details: {e.Message}");
        }
    }

    public void RegisterQueue(string queueName)
    {
        _logger.LogInformation($"Registering queue {queueName}...");

        _channel ??= GetChannel();

        _channel.QueueDeclare(
            queueName, 
            durable: false, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null);

        _logger.LogInformation($"Registering queue {queueName}...OK");
    }

    public EventingBasicConsumer GetConsumer(string queueName)
    {
        _channel ??= GetChannel();
        
        var consumer = new EventingBasicConsumer(_channel);

        _channel.BasicConsume(queueName, false, consumer);
        
        return consumer;
    }
    
    internal static IModel GetChannel()
    {
        if (_channel != null && _channel.IsOpen)
            return _channel;

        if (_connection is null)
            throw new Exception("Rabbit MQ Connection is required to create a channel. Call Connect() method before it.");
        
        try
        {
            _channel = _connection.CreateModel();
        }
        catch (RabbitMQClientException e)
        {
            throw new Exception($"It was not possible create a channel to connection {_connection.Endpoint.HostName}. Details: {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"It was not possible create a channel to connection {_connection.Endpoint.HostName}. Details: {e.Message}");
        }

        return _channel;
    }
}