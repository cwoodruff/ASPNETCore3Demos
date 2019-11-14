using System;

namespace GrpcVendorMachineClient.Api.Controllers
{
    class OrderProducts
    {
        private static string AskForInsertCoins()
        {
            // To insert coins the customer should enter the value of the coin via standard input.
            return Console.ReadLine().Trim() + Environment.NewLine;
        }
    }
}
