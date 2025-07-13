using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options
{
    public sealed class MessageConsumerExchangeTypeSettings<TMessage>
        where TMessage : class
    {
        public MessageBrokerExchangeType ExchangeType { get; private set; } = MessageBrokerExchangeType.Direct;
        public string? ServiceIdentifier { get; set; }

        public MessageConsumerExchangeTypeSettings(
            MessageBrokerExchangeType exchangeType,
            string? serviceIdentifier = null)
        {
            ExchangeType = exchangeType;
            ServiceIdentifier = serviceIdentifier;
        }
    }
}
