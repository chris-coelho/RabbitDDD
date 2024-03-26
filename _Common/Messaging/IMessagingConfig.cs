using RabbitMQ.Client.Events;

namespace Common.Messaging;

public interface IMessagingConfig
{
    void Connect(string uri);
    void RegisterQueue(string queueName);

    EventingBasicConsumer GetConsumer(string queueName);

}