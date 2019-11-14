using System.Collections.Generic;

namespace GrpcVendorMachineService.Domain.Interfaces
{
    public interface ICoinsManagement
    {
        (IEnumerable<decimal> coins, IEnumerable<string> infos) ExtractAnswerCoinsAndInfos(string answer);
        (bool areValid, string errorMsg) ValidateInsertedCoins(IEnumerable<decimal> insertedCoins);
    }
}