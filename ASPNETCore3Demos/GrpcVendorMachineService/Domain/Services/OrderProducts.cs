using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcVendorMachineService.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GrpcVendorMachineService.Services
{
    public class OrderProducts : Orderer.OrdererBase, IOrderProducts
    {
        private readonly ILogger<OrderProducts> _logger;
        private readonly ICoinsManagement _coinsManagementService;

        public OrderProducts(ILogger<OrderProducts> logger, ICoinsManagement coinsManagementService)
        {
            _logger = logger;
            _coinsManagementService = coinsManagementService;
        }

        public override Task<PlaceOrderReply> PlaceOrder(OrderRequest request, ServerCallContext context)
        {
            try
            {
                var response = new PlaceOrderReply() { Message = $"Order has been placed {request.Message}" };
                var (coins, infos) = _coinsManagementService.ExtractAnswerCoinsAndInfos(request.Message);
                
                var (areValid, error) = _coinsManagementService.ValidateInsertedCoins(coins);
                // Validate input valuesand just message user if operation cannot be completed
                if (areValid) return Task.FromResult(response);
                else return Task.FromResult(new PlaceOrderReply { Message = error });
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while placing order: ", ex.Message);
                throw new RpcException(Status.DefaultCancelled ,ex.Message);
            }
        }
    }
}