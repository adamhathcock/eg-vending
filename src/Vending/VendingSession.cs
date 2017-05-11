using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Vending
{
    public class VendingSession : IVendingSession
    {
        private readonly Dictionary<Token, Coin> currentTokens = new Dictionary<Token, Coin>();

        private readonly ICoinRecognizer coinRecognizer;
        private readonly ILogger logger;

        public VendingSession(ICoinRecognizer coinRecognizer, ILogger<CoinRecognizer> logger)
        {
            this.coinRecognizer = coinRecognizer;
            this.logger = logger;
        }

        public bool TryAcceptToken(Token token)
        {
            var coin = coinRecognizer.Recognize(token);
            if (coin != null)
            {
                logger.LogDebug($"matched token {token} to coin {coin}");
                currentTokens.Add(token, coin);
                logger.LogDebug($"Current Value {GetCurrentCoinValue()}");
                return true;
            }
            logger.LogDebug($"unmatched token {token}");
            return false;
        }

        public double GetCurrentCoinValue()
        {
            return currentTokens.Values.Aggregate<Coin, double>(0.0, (x, c) => x + c.Value);
        }
    }
}