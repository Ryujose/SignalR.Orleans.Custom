using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

namespace SignalR.Orleans.Tests;

public class OrleansFixture : IDisposable
{
    public OrleansFixture()
    {
        Silo = new HostBuilder()
            .UseOrleans(siloBuilder =>
            {
                siloBuilder.UseLocalhostClustering();
                siloBuilder.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback);
                siloBuilder.UseSignalR();
            })
            .Build();

        Silo.StartAsync().GetAwaiter().GetResult();

        ClientHost = new HostBuilder()
            .UseOrleansClient(clientBuilder =>
            {
                clientBuilder.UseLocalhostClustering();
                clientBuilder.UseSignalR(config: null); // fixes compiler confusion
            })
            .Build();

        ClientHost.StartAsync().GetAwaiter().GetResult();
        Client = ClientHost.Services.GetRequiredService<IClusterClient>();
    }

    public IHost Silo { get; }
    public IClusterClient Client { get; }
    public IHost ClientHost { get; }

    public void Dispose()
    {
        ClientHost.StopAsync().GetAwaiter().GetResult();
        Silo.StopAsync().GetAwaiter().GetResult();
        ClientHost.Dispose();
        Silo.Dispose();
    }
}