namespace Atya.Messaging.Abstractions;

/// <summary>
/// Registers message consumers with a transport.
/// </summary>
/// <typeparam name="TMessage">The message payload type.</typeparam>
public interface IMessageConsumerRegistry<TMessage>
{
    /// <summary>
    /// Subscribes a consumer to messages of the configured type.
    /// </summary>
    /// <param name="consumer">The consumer to subscribe.</param>
    /// <returns>A subscription that can remove the consumer.</returns>
    public IMessageSubscription Subscribe(IMessageConsumer<TMessage> consumer);
}
