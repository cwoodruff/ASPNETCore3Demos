using System.Collections.Generic;

namespace GrpcVendorMachineService.Domain.Models
{

    public class VendorCoins
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public decimal Value { get; set; }
        public int AvailableUnits { get; set; }

    }

    public class CoinsManagement
    {
        public static IEnumerable<decimal> AcceptedCoins = new decimal[]
        {
                0.01m,
                0.05m,
                0.10m,
                0.25m,
                0.50m,
                1.00m
        };
    }

}