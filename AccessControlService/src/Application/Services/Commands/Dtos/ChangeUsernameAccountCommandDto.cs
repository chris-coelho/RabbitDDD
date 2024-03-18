using Common.Application;

namespace Application.Services.Commands.Dtos;

public class ChangeUsernameAccountCommandDto : IApplicationCommand
{
    public ChangeUsernameAccountCommandDto(Guid accountId, string username)
    {
        AccountId = accountId;
        Username = username;
    }

    public Guid AccountId { get; private set; }
    public string Username { get; private set; }
}