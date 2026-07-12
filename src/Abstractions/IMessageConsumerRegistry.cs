namespace Atya.Messaging.Abstractions;

/// <summary>
/// Registers message consumers with a transport.
/// </summary>
/// <typeparam name="TMessage">The message payload type.</typeparam>
public interface IMessageConsumerRegistry<TMessage>
{
    /// <summary>
    /// Asynchronously subscribes a consumer to messages of the configured type.
    /// </summary>
    /// <param name="consumer">The consumer to subscribe.</param>
    /// <param name="cancellationToken">A token that cancels the subscribe operation.</param>
    /// <returns>A task that produces a subscription that can remove the consumer.</returns>
    public ValueTask<IMessageSubscription> SubscribeAsync(
        IMessageConsumer<TMessage> consumer,
        CancellationToken cancellationToken = default);
}
