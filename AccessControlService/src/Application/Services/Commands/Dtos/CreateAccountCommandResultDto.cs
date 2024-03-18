using Common.Application;

namespace Application.Services.Commands.Dtos;

public class CreateAccountCommandResultDto : IApplicationCommandResult
{
    public CreateAccountCommandResultDto(Guid accountId, DateTime createdOn)
    {
        AccountId = accountId;
        CreatedOn = createdOn;
    }

    public Guid AccountId { get; private set; }
    public DateTime CreatedOn { get; private set; }
}