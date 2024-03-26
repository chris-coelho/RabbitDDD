using Common.Messaging;

namespace Domain.DomainEvents;

public class AccountUsernameChangedEvent : IntegrationMessage
{
    public AccountUsernameChangedEvent(Guid accountId, string username)
    {
        AccountId = accountId;
        Username = username;
    }

    public Guid AccountId { get; set; }
    public string Username { get; set; }
}