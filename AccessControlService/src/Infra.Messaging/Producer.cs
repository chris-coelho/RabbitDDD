using System.Text;
using RabbitMQ.Client;

namespace Infra.Messaging;

public class Producer
{
    public static void Publish(IModel channel, string queueName, string message)
    {
        channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
    }
}