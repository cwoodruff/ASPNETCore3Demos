using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcVendorMachineService.Domain.Interfaces
{
    public interface IOrderProducts
    {
        Task<PlaceOrderReply> PlaceOrder(OrderRequest request, ServerCallContext context);
    }
}