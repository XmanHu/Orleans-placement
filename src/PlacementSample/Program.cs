using System;
using System.Net;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime.Messaging;

namespace PlacementSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ip = args.Length > 0 ? args[0] : "127.0.0.1";
            try
            {
                using (var client = await ConnectClient(ip))
                {
                    while (true)
                    {
                        Console.WriteLine("Please input a integer.");
                        int input = Int32.Parse(Console.ReadLine());

                        var grain = client.GetGrain<IGrainFirst>(input);
                        await grain.Work();

                        var second = client.GetGrain<IGrainSecond>(input);
                        var reply = await second.Echo("foo");
                        Console.WriteLine(reply);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<IClusterClient> ConnectClient(string ip)
        {
            var client = new ClientBuilder()
                .UseStaticClustering(new IPEndPoint(IPAddress.Parse(ip), 30000), new IPEndPoint(IPAddress.Parse(ip), 30001))
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "dev";
                })
                .Configure<ConnectionOptions>(op => op.ProtocolVersion = NetworkProtocolVersion.Version2)
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host\n");
            return client;
        }
    }
}
