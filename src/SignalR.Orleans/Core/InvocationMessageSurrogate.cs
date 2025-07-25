using Microsoft.AspNetCore.SignalR.Protocol;

namespace SignalR.Orleans.Core;

[GenerateSerializer]
[Immutable]
public readonly struct InvocationMessageSurrogate
{
    [Id(0)] public readonly string? InvocationId;

    [Id(1)] public readonly string Target;

    [Id(2)] public readonly object?[] Arguments;

    [Id(3)] public readonly string[]? StreamIds;

    [Id(4)] public readonly IDictionary<string, string>? Headers;

    public InvocationMessageSurrogate(string? invocationId, string target, object?[] arguments, string[]? streamIds,
        IDictionary<string, string>? headers)
    {
        InvocationId = invocationId;
        Target = target;
        Arguments = arguments;
        StreamIds = streamIds;
        Headers = headers;
    }
}

[RegisterConverter]
public sealed class InvocationMessageSurrogateConverter : IConverter<InvocationMessage, InvocationMessageSurrogate>
{
    public InvocationMessage ConvertFromSurrogate(in InvocationMessageSurrogate surrogate) =>
        new(
            surrogate.InvocationId,
            surrogate.Target,
            surrogate.Arguments,
            surrogate.StreamIds) { Headers = surrogate.Headers };

    public InvocationMessageSurrogate ConvertToSurrogate(in InvocationMessage value) =>
        new(
            value.InvocationId,
            value.Target,
            value.Arguments,
            value.StreamIds,
            value.Headers);
}