using Application.Services.Commands.Dtos;
using Common.Application;
using Common.Application.NotificationPattern;
using Domain.Repositories;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
namespace Application.Services.Commands;

public class GetAccountDetailsAppService : IApplicationCommandServiceWithResultAsync<GetAccountDetailsCommandDto, 
    GetAccountDetailsCommandResultDto>
{
    private readonly INotificationContext _notification;
    private readonly IAccountRepository _accountRepository;

    public GetAccountDetailsAppService(INotificationContext notification, IAccountRepository accountRepository)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public async Task<GetAccountDetailsCommandResultDto?> ProcessAsync(GetAccountDetailsCommandDto command, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, cancellationToken);
        if (account is null)
        {
            _notification.AddAsAppService("Account does not exists");
            return null;
        }

        return new GetAccountDetailsCommandResultDto
        (
            account.Id,
            account.Username,
            account.Email,
            account.Active,
            account.CreatedOn
        );
    }
}