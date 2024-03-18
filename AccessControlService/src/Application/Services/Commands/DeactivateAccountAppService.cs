using Application.Services.Commands.Dtos;
using Common.Application;
using Common.Application.NotificationPattern;
using Domain.Repositories;

namespace Application.Services.Commands;

public class DeactivateAccountAppService : IApplicationCommandServiceAsync<DeactivateAccountCommandDto>
{
    private readonly INotificationContext _notification;
    private readonly IAccountRepository _accountRepository;

    public DeactivateAccountAppService(
        INotificationContext notification, 
        IAccountRepository accountRepository)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public async Task ProcessAsync(DeactivateAccountCommandDto command, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, cancellationToken);
        if (account is null)
        {
            _notification.AddAsAppService("Account does not exists");
            return;
        }

        account.Deactivate();
        if (account.Invalid)
        {
            _notification.AddAsDomainValidation("Deactivate failure", account.ValidationResult);
            return;
        }

        await _accountRepository.SaveOrUpdateAsync(account, cancellationToken);
    }
}