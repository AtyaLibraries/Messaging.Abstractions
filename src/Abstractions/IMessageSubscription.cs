namespace Atya.Messaging.Abstractions;

/// <summary>
/// Represents an active consumer subscription.
/// </summary>
public interface IMessageSubscription
{
    /// <summary>
    /// Unsubscribes the consumer.
    /// </summary>
    /// <param name="cancellationToken">A token that cancels the unsubscribe operation.</param>
    /// <returns>A task that completes when the subscription has been removed.</returns>
    public ValueTask UnsubscribeAsync(CancellationToken cancellationToken = default);
}
