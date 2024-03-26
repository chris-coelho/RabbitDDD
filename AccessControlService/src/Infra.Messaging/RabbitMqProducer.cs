using System.Text;
using Common.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infra.Messaging;

public class RabbitMqProducer : IMessagingSender
{
    private readonly IModel _channel = RabbitMqConfiguration.GetChannel();

    public void PublishAll(string queueName, ICollection<IIntegrationMessage> messages)
    {
        foreach (var message in messages)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
        
            _channel.BasicPublish(
                exchange: string.Empty, 
                routingKey: queueName, 
                basicProperties: null, 
                body: body);
        }
    }
}