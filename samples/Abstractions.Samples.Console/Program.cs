using Atya.Messaging.Abstractions;

namespace Atya.Messaging.Abstractions.Samples.ConsoleApp;

/// <summary>
/// Runs the sample console application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Demonstrates creating a transport-neutral message envelope.
    /// </summary>
    public static void Main()
    {
        var envelope = MessageEnvelope.Create(
            "customer.created",
            new MessagePublishOptions(
                messageId: "message-1",
                correlationId: "correlation-1",
                headers: new Dictionary<string, string>
                {
                    ["tenant"] = "acme",
                }));

        Console.WriteLine($"{envelope.MessageId}: {envelope.Message}");
        Console.WriteLine($"Correlation: {envelope.CorrelationId}");
    }
}
