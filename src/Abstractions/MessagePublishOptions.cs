using System.Collections.ObjectModel;
using Atya.Foundation.Guards;

namespace Atya.Messaging.Abstractions;

/// <summary>
/// Provides optional metadata for a published message.
/// </summary>
public sealed class MessagePublishOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagePublishOptions"/> class.
    /// </summary>
    /// <param name="messageId">The optional caller-supplied message identifier.</param>
    /// <param name="correlationId">The optional correlation identifier to propagate.</param>
    /// <param name="headers">The optional message headers.</param>
    public MessagePublishOptions(
        string? messageId = null,
        string? correlationId = null,
        IReadOnlyDictionary<string, string>? headers = null)
    {
        MessageId = string.IsNullOrWhiteSpace(messageId) ? null : messageId;
        CorrelationId = string.IsNullOrWhiteSpace(correlationId) ? null : correlationId;
        Headers = CopyHeaders(headers);
    }

    /// <summary>
    /// Gets an empty options instance.
    /// </summary>
    public static MessagePublishOptions Empty { get; } = new();

    /// <summary>
    /// Gets the optional caller-supplied message identifier.
    /// </summary>
    public string? MessageId { get; }

    /// <summary>
    /// Gets the optional correlation identifier to propagate.
    /// </summary>
    public string? CorrelationId { get; }

    /// <summary>
    /// Gets the optional message headers.
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
