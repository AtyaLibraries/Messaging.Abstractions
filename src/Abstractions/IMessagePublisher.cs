namespace Atya.Messaging.Abstractions;

/// <summary>
/// Publishes messages to a transport.
/// </summary>
/// <typeparam name="TMessage">The message payload type.</typeparam>
public interface IMessagePublisher<TMessage>
{
    /// <summary>
    /// Publishes a message.
    /// </summary>
    /// <param name="message">The message payload.</param>
    /// <param name="options">Optional metadata for the published message.</param>
    /// <param name="cancellationToken">A token that cancels the publish operation.</param>
    /// <returns>A task that completes when the message has been accepted by the transport.</returns>
    public ValueTask PublishAsync(
        TMessage message,
        MessagePublishOptions? options = null,
        CancellationToken cancellationToken = default);
}
