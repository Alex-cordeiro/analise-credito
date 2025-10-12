using RabbitMQ.Client;

namespace RabbitHole.Interfaces;

public interface IRabbitConnection
{
    ValueTask<IConnection> GetConnectionAsync(); 
}
