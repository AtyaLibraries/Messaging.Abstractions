namespace Atya.Messaging.Abstractions;

/// <summary>
/// Handles messages delivered through an Atya messaging transport.
/// </summary>
/// <typeparam name="TMessage">The message payload type.</typeparam>
public interface IMessageConsumer<TMessage>
{
    /// <summary>
    /// Handles a delivered message envelope.
    /// </summary>
    /// <param name="envelope">The delivered message envelope.</param>
    /// <param name="cancellationToken">A token that cancels the handling operation.</param>
    /// <returns>A task that completes when the message has been handled.</returns>
    public ValueTask ConsumeAsync(
        MessageEnvelope<TMessage> envelope,
        CancellationToken cancellationToken = default);
}
