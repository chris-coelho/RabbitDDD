using System.Text;
using Application.Messaging.Events;
using Common.Application.NotificationPattern;
using Domain.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Application.Messaging.EventHandlers;

public class AccountUsernameChangedEventHandler
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AccountUsernameChangedEventHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }

    public void Handle(object? sender, BasicDeliverEventArgs args)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AccountUsernameChangedEventHandler>>();
        var notifications = scope.ServiceProvider.GetRequiredService<INotificationContext>();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

        logger.LogInformation($"Processing received message: {args.RoutingKey}...");

        var model = ((EventingBasicConsumer) sender!).Model;
        
        var message = GetMessage(args);

        var account = accountRepository.GetByIdAsync(message.AccountId).Result;
        if (account is null)
        {
            notifications.AddAsAppService($"Account {message.AccountId} does not exists");
            return;
        }
        
        account.ChangeUsername(message.Username);
        if (account.Invalid)
        {
            notifications.AddAsDomainValidation($"Update username has been failed", account.ValidationResult);
            return;
        }

        accountRepository.SaveOrUpdateAsync(account);
        
        model.BasicAck(args.DeliveryTag, false);
        
        logger.LogInformation($"Message: {args.RoutingKey} processed with successful. Account ID: {message.AccountId} was updated.");
    }

    private AccountUsernameChangedEvent GetMessage(BasicDeliverEventArgs args)
    {
        var body = args.Body.ToArray();
        return JsonConvert.DeserializeObject<AccountUsernameChangedEvent>(Encoding.UTF8.GetString(body));
    }
}