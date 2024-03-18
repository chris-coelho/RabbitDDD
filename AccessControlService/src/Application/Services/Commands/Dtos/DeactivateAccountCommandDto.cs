using Common.Application;

namespace Application.Services.Commands.Dtos;

public class DeactivateAccountCommandDto : IApplicationCommand
{
    public DeactivateAccountCommandDto(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; private set; }
}