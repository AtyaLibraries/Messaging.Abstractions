namespace Atya.Messaging.Abstractions;

/// <summary>
/// Creates message envelopes.
/// </summary>
public static class MessageEnvelope
{
    /// <summary>
    /// Creates an envelope from a message and publish options.
    /// </summary>
    /// <typeparam name="TMessage">The message payload type.</typeparam>
    /// <param name="message">The message payload.</param>
    /// <param name="options">Optional publish metadata.</param>
    /// <returns>A message envelope.</returns>
    public static MessageEnvelope<TMessage> Create<TMessage>(
        TMessage message,
        MessagePublishOptions? options = null)
    {
        var metadata = options ?? MessagePublishOptions.Empty;
        var messageId = string.IsNullOrWhiteSpace(metadata.MessageId)
            ? Guid.NewGuid().ToString("N")
            : metadata.MessageId;

        return new MessageEnvelope<TMessage>(
            messageId,
            message,
            metadata.CorrelationId,
            metadata.Headers);
    }
}
