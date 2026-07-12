namespace Atya.Messaging.Abstractions.UnitTests;

public sealed class MessageEnvelopeTests
{
    [Fact]
    public void Constructor_With_Metadata_Copies_Values()
    {
        var headers = new Dictionary<string, string>
        {
            ["tenant"] = "acme",
        };

        var envelope = new MessageEnvelope<string>(
            "message-1",
            "payload",
            "correlation-1",
            headers);

        envelope.MessageId.Should().Be("message-1");
        envelope.Message.Should().Be("payload");
        envelope.CorrelationId.Should().Be("correlation-1");
        envelope.Headers.Should().ContainSingle().Which.Should().Be(
            new KeyValuePair<string, string>("tenant", "acme"));

        headers["tenant"] = "other";
        envelope.Headers["tenant"].Should().Be("acme");
    }

    [Fact]
    public void Constructor_With_Whitespace_Correlation_Normalizes_To_Null()
    {
        var envelope = new MessageEnvelope<string>("message-1", "payload", " ");

        envelope.CorrelationId.Should().BeNull();
        envelope.Headers.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_With_Empty_MessageId_Throws()
    {
        var act = () => new MessageEnvelope<string>(" ", "payload");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_With_Null_Message_Throws()
    {
        var act = () => new MessageEnvelope<string>("message-1", null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_Without_MessageId_Generates_MessageId()
    {
        var envelope = MessageEnvelope.Create("payload");

        envelope.MessageId.Should().NotBeNullOrWhiteSpace();
        envelope.Message.Should().Be("payload");
    }

    [Fact]
    public void Create_With_Options_Uses_Options_Metadata()
    {
        var envelope = MessageEnvelope.Create(
            "payload",
            new MessagePublishOptions(
                "message-1",
                "correlation-1",
                new Dictionary<string, string> { ["trace"] = "abc" }));

        envelope.MessageId.Should().Be("message-1");
        envelope.CorrelationId.Should().Be("correlation-1");
        envelope.Headers["trace"].Should().Be("abc");
    }
}
