namespace RabbitHole.Interfaces;

public interface IRabbitPublisher
{
  Task PublishAsync(string queueName, object message);
}
