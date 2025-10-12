namespace RabbitHole.Interfaces;

public interface IRabbitConsumer
{
    Task StartConsumingAsync(string queueName, Func<string, Task> onMessageReceived, CancellationToken token = default);
}
