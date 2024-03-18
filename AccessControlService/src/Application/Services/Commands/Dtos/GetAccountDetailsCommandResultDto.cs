using Common.Application;

namespace Application.Services.Commands.Dtos;

public class GetAccountDetailsCommandResultDto : IApplicationCommandResult
{
    public GetAccountDetailsCommandResultDto(Guid accountId, string username, string email, bool active, DateTime createdOn)
    {
        AccountId = accountId;
        Username = username;
        Email = email;
        Active = active;
        CreatedOn = createdOn;
    }

    public Guid AccountId { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedOn { get; private set; }
}