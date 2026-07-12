namespace Atya.Messaging.Abstractions.UnitTests;

public sealed class MessagePublishOptionsTests
{
    [Fact]
    public void Empty_Returns_Empty_Metadata()
    {
        MessagePublishOptions.Empty.MessageId.Should().BeNull();
        MessagePublishOptions.Empty.CorrelationId.Should().BeNull();
        MessagePublishOptions.Empty.Headers.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_Copies_Headers()
    {
        var headers = new Dictionary<string, string>
        {
            ["key"] = "value",
        };

        var options = new MessagePublishOptions("message-1", "correlation-1", headers);

        headers["key"] = "changed";

        options.MessageId.Should().Be("message-1");
        options.CorrelationId.Should().Be("correlation-1");
        options.Headers["key"].Should().Be("value");
    }

    [Fact]
    public void Constructor_Normalizes_Whitespace_Metadata_To_Null()
    {
        var options = new MessagePublishOptions(" ", "\t");

        options.MessageId.Should().BeNull();
        options.CorrelationId.Should().BeNull();
    }

    [Fact]
    public void Constructor_With_Empty_Header_Key_Throws()
    {
        var act = () => new MessagePublishOptions(
            headers: new Dictionary<string, string> { [" "] = "value" });

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_With_Null_Header_Value_Throws()
    {
        var act = () => new MessagePublishOptions(
            headers: new Dictionary<string, string> { ["key"] = null! });

        act.Should().Throw<ArgumentNullException>();
    }
}
