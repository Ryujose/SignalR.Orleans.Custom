﻿namespace SignalR.Orleans.Clients;

internal readonly record struct ClientKey
{
    public required string HubType { get; init; }
    public required string ConnectionId { get; init; }

    public string ToGrainPrimaryKey() => $"{HubType}:{ConnectionId}";

    public static ClientKey FromGrainPrimaryKey(string primaryKey)
    {
        var parts = primaryKey.Split(':', 2);
        return new ClientKey { HubType = parts[0], ConnectionId = parts[1] };
    }
}