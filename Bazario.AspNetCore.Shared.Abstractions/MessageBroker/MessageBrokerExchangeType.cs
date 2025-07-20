namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    /// <summary>
    /// Represents the type of exchange used in a message broker.
    /// </summary>
    public enum MessageBrokerExchangeType
    {
        Direct,
        Fanout
    }
}
