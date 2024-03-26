using Common.Messaging;

namespace Application.Messaging.Events;

public class AccountUsernameChangedEvent : IntegrationMessage
{
    public Guid AccountId { get; set; }
    public string Username { get; set; }
}