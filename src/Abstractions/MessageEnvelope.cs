using System.Collections.ObjectModel;
using Atya.Foundation.Guards;

namespace Atya.Messaging.Abstractions;

/// <summary>
/// Carries a message payload and transport-neutral metadata.
/// </summary>
/// <typeparam name="TMessage">The message payload type.</typeparam>
public sealed class MessageEnvelope<TMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageEnvelope{TMessage}"/> class.
    /// </summary>
    /// <param name="messageId">The stable message identifier.</param>
    /// <param name="message">The message payload.</param>
    /// <param name="correlationId">The optional correlation identifier propagated with the message.</param>
    /// <param name="headers">The optional message headers.</param>
    public MessageEnvelope(
        string messageId,
        TMessage message,
        string? correlationId = null,
        IReadOnlyDictionary<string, string>? headers = null)
    {
        MessageId = Guard.AgainstNullOrWhiteSpace(messageId);
        Message = message is null ? throw new ArgumentNullException(nameof(message)) : message;
        CorrelationId = string.IsNullOrWhiteSpace(correlationId) ? null : correlationId;
        Headers = CopyHeaders(headers);
    }

    /// <summary>
    /// Gets the stable message identifier.
    /// </summary>
    public string MessageId { get; }

    /// <summary>
    /// Gets the message payload.
    /// </summary>
    public TMessage Message { get; }

    /// <summary>
    /// Gets the optional correlation identifier propagated with the message.
    /// </summary>
    public string? CorrelationId { get; }

    /// <summary>
    /// Gets the message headers.
    /// </summary>
    public IReadOnlyDictionary<string, string> Headers { get; }

    private static ReadOnlyDictionary<string, string> CopyHeaders(
        IReadOnlyDictionary<string, string>? headers)
    {
        if (headers is null || headers.Count == 0)
        {
            return ReadOnlyDictionary<string, string>.Empty;
        }

        var copy = new Dictionary<string, string>(headers.Count, StringComparer.Ordinal);
        foreach (var (key, value) in headers)
        {
            copy.Add(Guard.AgainstNullOrWhiteSpace(key), Guard.AgainstNull(value));
        }

        return new ReadOnlyDictionary<string, string>(copy);
    }
}
