using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime.Messaging;

namespace Silo
{
    class Program
    {
        private static int siloPort = 11111;
        private static int gatewayPort = 30000;

        static async Task<int> Main(string[] args)
        {
            try
            {
                var host = await StartSilo(0);
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");

                Console.ReadLine();
                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo(int num)
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                    .UseLocalhostClustering(primarySiloEndpoint:IPEndPoint.Parse("127.0.0.1:11111"))
                    .Configure<EndpointOptions>(options =>
                    {
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.GatewayPort = gatewayPort + num;
                    options.SiloPort = siloPort + num;
                    })
                    .Configure<ConnectionOptions>(op => op.ProtocolVersion = NetworkProtocolVersion.Version2)
                    .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();

            await host.StartAsync();
            return host;
        }
    }
}
