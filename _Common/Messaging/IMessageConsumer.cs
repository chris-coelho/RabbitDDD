using RabbitMQ.Client.Events;

namespace Common.Messaging;

public interface IMessageConsumer
{
    void Handle(object? sender, BasicDeliverEventArgs args);
}