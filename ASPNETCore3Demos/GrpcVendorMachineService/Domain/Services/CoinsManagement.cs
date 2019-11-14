using GrpcVendorMachineService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcVendorMachineService.Domain.Services
{
    public class CoinsManagement : ICoinsManagement
    {
        private readonly ILogger<CoinsManagement> _logger;
        const char CHAR_SPACE = ' ';

        public CoinsManagement(ILogger<CoinsManagement> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Extract coins values and another params from answer param
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public (IEnumerable<decimal> coins, IEnumerable<string> infos) ExtractAnswerCoinsAndInfos(string answer)
        {
            var answerValues = answer.Split(CHAR_SPACE);
            var coins = new List<decimal>();
            var infos = new List<string>();

            foreach (var item in answerValues)
            {
                if (decimal.TryParse(item, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out decimal coin))
                    coins.Add(coin);
                else
                    infos.Add(item);
            }

            return (coins, infos);
        }

        /// <summary>
        /// Validate if all inserted coins are accepted
        /// </summary>
        /// <param name="insertedCoins">Coins values from user input</param>
        /// <returns>Boolean condition if valid or not</returns>
        public (bool, string) ValidateInsertedCoins(IEnumerable<decimal> insertedCoins)
        {
            // List invalid inserted coins values
            // The available coins are 0.01, 0.05, 0.10, 0.25, 0.50 and 1.00
            var rejectedCoins = insertedCoins.Where(coin => !AcceptedCoins.Contains(coin));

            bool valid = !rejectedCoins.Any();
            if (valid) return (valid, string.Empty);
            
            var errorMsg = $"Invalid coins were inserted: {string.Join(CHAR_SPACE, rejectedCoins)} {Environment.NewLine}"+
                           $"Only {string.Join(CHAR_SPACE, AcceptedCoins)} coins values are accepted.{Environment.NewLine}" +
                           "Cannot proceed with invalid values.";
            return (valid, errorMsg);
        }

        public IEnumerable<decimal> AcceptedCoins = new decimal[]
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

