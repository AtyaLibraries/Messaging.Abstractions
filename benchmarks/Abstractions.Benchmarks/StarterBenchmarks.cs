using Atya.Messaging.Abstractions;
using BenchmarkDotNet.Attributes;

namespace Atya.Messaging.Abstractions.Benchmarks;

/// <summary>
/// Benchmarks for envelope creation.
/// </summary>
[MemoryDiagnoser]
public class StarterBenchmarks
{
    private readonly MessagePublishOptions options = new(
        "message-1",
        "correlation-1",
        new Dictionary<string, string>
        {
            ["tenant"] = "acme",
        });

    /// <summary>
    /// Creates an envelope with caller-provided metadata.
    /// </summary>
    /// <returns>The created envelope.</returns>
    [Benchmark]
    public MessageEnvelope<string> CreateEnvelope() => MessageEnvelope.Create("payload", options);
}
