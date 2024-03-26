using Application.Messaging;
using Application.Services.Commands.Dtos;
using Common.Application;
using Common.Application.NotificationPattern;
using Common.Messaging;
using Domain.Repositories;

namespace Application.Services.Commands;

public class ChangeUsernameAccountAppService : IApplicationCommandServiceAsync<ChangeUsernameAccountCommandDto>
{
    private readonly INotificationContext _notification;
    private readonly IAccountRepository _accountRepository;
    private readonly IMessagingSender _messagingSender;

    public ChangeUsernameAccountAppService(
        INotificationContext notification, 
        IAccountRepository accountRepository, 
        IMessagingSender messagingSender)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _messagingSender = messagingSender ?? throw new ArgumentNullException(nameof(messagingSender));
    }

    public async Task ProcessAsync(ChangeUsernameAccountCommandDto command, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, cancellationToken);
        if (account is null)
        {
            _notification.AddAsAppService("Account does not exists");
            return;
        }

        account.ChangeUsername(command.Username);
        if (account.Invalid)
        {
            _notification.AddAsDomainValidation("Change username failure", account.ValidationResult);
            return;
        }

        await _accountRepository.SaveOrUpdateAsync(account, cancellationToken);

        _messagingSender.PublishAll(MessagingConstants.UsernameAccountChangedQueue, account.DomainEvents.ToList());
    }
}