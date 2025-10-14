using System.Diagnostics;
using RabbitHole.Interfaces;
using RabbitHole.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Microsoft.Extensions.Logging;
using RabbitHole.Exceptions;

namespace RabbitHole.Services;

public class RabbitConnection : IRabbitConnection, IAsyncDisposable
{
    private readonly RabbitOptions _options;
    private IConnection? _connection;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly ILogger<RabbitConnection> _logger;

    public RabbitConnection(RabbitOptions options, ILogger<RabbitConnection> logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException();
        ValidateOptions();
    }

    private void ValidateOptions()
    {
        if (string.IsNullOrWhiteSpace(_options.HostName))
            throw new ArgumentException("HostName não pode ser vazio", nameof(_options));

        if (string.IsNullOrWhiteSpace(_options.UserName))
            throw new ArgumentException("UserName não pode ser vazio", nameof(_options));
    }


    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }

        _lock.Dispose();
    }

    public async ValueTask<IConnection> GetConnectionAsync()
    {
        if (_connection is { IsOpen: true })
            return _connection;

        await _lock.WaitAsync();
        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost
            };

            var stopWatch = Stopwatch.StartNew();
            _connection = await factory.CreateConnectionAsync();
            stopWatch.Stop();

        }
        catch (BrokerUnreachableException ex)
        {
            _logger.LogError(ex, "Falha ao conectar com RabbitMQ em {HostName}:{Port}",
            _options.HostName, _options.Port);
            throw new RabbitMQConnectionException($"Não foi possível conectar ao RabbitMQ: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao retornar a conexão");
            throw;
        }
        finally
        {
            _lock.Release();
        }

        return _connection!;
    }
}
