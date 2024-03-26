namespace Common.Messaging;

public interface IMessagingSender
{
    void PublishAll(string queueName, ICollection<IIntegrationMessage> messages);
}