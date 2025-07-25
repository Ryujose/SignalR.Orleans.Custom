using Microsoft.AspNetCore.SignalR;

namespace SignalR.Orleans;

internal static class HubUtility
{
    internal static string GetHubName<THub>() where THub : Hub => typeof(THub).Name;
}