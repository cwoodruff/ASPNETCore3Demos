using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcVendorMachineService;
using Microsoft.Extensions.Logging;

namespace GrpcVendorMachineClient
{
    class Program
    {
        private static ILoggerFactory _loggerFactory = CreateLoggerFactory();
        static async Task Main(string[] args)
        {
            Orderer.OrdererClient client = SetupOrderProductsClient();
            
            // The customer should be able to insert coins:
            Console.WriteLine("Please insert coins:");
            var reply = await client.PlaceOrderAsync(new OrderRequest { Message = Console.ReadLine()});
            
            Console.WriteLine(reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static Orderer.OrdererClient SetupOrderProductsClient()
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { LoggerFactory = _loggerFactory });
            var client = new Orderer.OrdererClient(channel);
            return client;
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
             return LoggerFactory.Create(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Debug);
            });
        }
    }
}