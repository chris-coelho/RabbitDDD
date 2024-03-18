using Common.Application;

namespace Application.Services.Commands.Dtos;

public class GetAccountDetailsCommandDto : IApplicationCommand
{
    public GetAccountDetailsCommandDto(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; private set; }
}