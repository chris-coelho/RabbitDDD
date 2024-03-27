namespace Common.Messaging;

public interface IMessagingConfig
{
    void Connect(string uri);
    void RegisterQueue(string queueName);
    void RegisterConsumer<T>(T handler, string queueName) where T : IMessageConsumer;
}