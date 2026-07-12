namespace Atya.Messaging.Abstractions;

/// <summary>
/// Publishes messages when the payload type is known only at runtime.
/// </summary>
public interface IMessagePublisher
{
    /// <summary>
    /// Publishes a message using its runtime payload type.
    /// </summary>
    /// <param name="messageType">The runtime message payload type.</param>
    /// <param name="message">The message payload.</param>
    /// <param name="options">Optional metadata for the published message.</param>
    /// <param name="cancellationToken">A token that cancels the publish operation.</param>
    /// <returns>A task that completes when the message has been accepted by the transport.</returns>
    public ValueTask PublishAsync(
        Type messageType,
        object message,
        MessagePublishOptions? options = null,
        CancellationToken cancellationToken = default);
}
