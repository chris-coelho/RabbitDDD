using Common.Util;

namespace Common.Messaging;

public abstract class IntegrationMessage : IIntegrationMessage
{
    protected IntegrationMessage()
    {
        MessageType = GetType().Name;
        OccurredOn = DateTime.Now.GetBrazilianTime();
    }

    public DateTime OccurredOn { get; }
    public string MessageType { get; }
    public Guid CorrelationId { get; set; }
}