using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infra.Messaging;

public class RabbitMqConsumer
{
    public static void Consume(IModel channel, string queueName)
    {
        channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
        };
    }
}