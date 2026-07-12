# Atya.Messaging.Abstractions

Transport-agnostic messaging contracts for publishers, consumers, subscriptions, and envelopes in Atya services.

[![NuGet Version](https://img.shields.io/nuget/v/Atya.Messaging.Abstractions?style=for-the-badge&logo=nuget&logoColor=white&label=NuGet&color=512BD4)](https://www.nuget.org/packages/Atya.Messaging.Abstractions)
[![Downloads](https://img.shields.io/nuget/dt/Atya.Messaging.Abstractions?style=for-the-badge&logo=nuget&logoColor=white&label=Downloads&color=512BD4)](https://www.nuget.org/packages/Atya.Messaging.Abstractions)
![.NET 10.0](https://img.shields.io/badge/.NET_10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-512BD4?style=for-the-badge)](https://github.com/AtyaLibraries/Messaging.Abstractions/blob/development/LICENSE)

## Overview

`Atya.Messaging.Abstractions` defines the small contract layer shared by Atya messaging transports. It models message publishers, consumers, consumer registries, subscriptions, and immutable envelopes without binding applications to a broker, serializer, host, or dependency injection container.

The package intentionally does not implement a transport. `Atya.Messaging.InMemory` proves these contracts for tests and local development in the same release cycle.

## Features

- Publisher, consumer, and consumer registry interfaces with cancellation-aware `ValueTask` methods.
- Immutable message envelopes with message id, optional correlation id, and copied headers.
- Publish options for transport-neutral metadata.
- Subscription contracts for removing registered consumers.
- No broker-client, DI, serialization, or hosting dependency.

## Installation

```bash
dotnet add package Atya.Messaging.Abstractions
```

```powershell
Install-Package Atya.Messaging.Abstractions
```

```xml
<PackageReference Include="Atya.Messaging.Abstractions" Version="<latest-stable>" />
```

## Quick Start

```csharp
using Atya.Messaging.Abstractions;

var envelope = MessageEnvelope.Create(
    "customer.created",
    new MessagePublishOptions(
        messageId: "message-1",
        correlationId: "correlation-1",
        headers: new Dictionary<string, string>
        {
            ["tenant"] = "acme",
        }));

Console.WriteLine(envelope.MessageId);
Console.WriteLine(envelope.Message);
Console.WriteLine(envelope.CorrelationId);
```

## Feature Tour

Implement a publisher:

```csharp
using Atya.Messaging.Abstractions;

public sealed class CustomerEventPublisher(IMessagePublisher<string> publisher)
{
    public ValueTask PublishAsync(string message, CancellationToken cancellationToken = default)
    {
        return publisher.PublishAsync(
            message,
            new MessagePublishOptions(correlationId: "request-123"),
            cancellationToken);
    }
}
```

Implement a consumer:

```csharp
using Atya.Messaging.Abstractions;

public sealed class CustomerEventConsumer : IMessageConsumer<string>
{
    public ValueTask ConsumeAsync(
        MessageEnvelope<string> envelope,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine(envelope.Message);
        return ValueTask.CompletedTask;
    }
}
```

Consumer failure semantics are transport-neutral: a normal return acknowledges handling, while an exception indicates handling failed and the transport may negatively acknowledge or redeliver the message.

## Error Codes

This package does not define Result error codes. Invalid programmer input is rejected with standard .NET argument exceptions through `Atya.Foundation.Guards`.

## Why These Dependencies

- `Atya.Foundation.Abstractions` keeps this package aligned with the foundation contract layer.
- `Atya.Foundation.Guards` provides consistent argument validation for public entry points.

## Links

- Repository: https://github.com/AtyaLibraries/Messaging.Abstractions
- NuGet: https://www.nuget.org/packages/Atya.Messaging.Abstractions
- Samples: https://github.com/AtyaLibraries/Messaging.Abstractions/tree/development/samples
- License: https://github.com/AtyaLibraries/Messaging.Abstractions/blob/development/LICENSE
