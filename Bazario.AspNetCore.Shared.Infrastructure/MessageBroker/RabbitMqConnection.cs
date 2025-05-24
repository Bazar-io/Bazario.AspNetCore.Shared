using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using RabbitMQ.Client;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker
{
    internal sealed class RabbitMqConnection(IConnectionFactory factory)
        : IRabbitMqConnection, 
        IDisposable
    {
        private IConnection? _connection;

        public IConnection Connection => _connection!;

        public async Task InitializeAsync()
        {
            _connection = await factory.CreateConnectionAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
