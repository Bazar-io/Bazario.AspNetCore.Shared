using RabbitMQ.Client;

namespace Bazario.AspNetCore.Shared.Infrastructure.Abstractions
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }
}
