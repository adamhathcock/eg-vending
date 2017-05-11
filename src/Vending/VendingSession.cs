using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Vending
{
    public class VendingSession : IVendingSession
    {
        private readonly Dictionary<Token, Coin> currentTokens = new Dictionary<Token, Coin>();

        private readonly List<Product> purchasedItems = new List<Product>();

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
            logger.LogWarning($"unmatched token {token}");
            return false;
        }

        public decimal GetCurrentCoinValue()
        {
            return Math.Round(currentTokens.Values.Aggregate(0.0m, (x, c) => x + c.Value), 2);
        }
        
        public decimal GetRemainingValue()
        {
            return Math.Round(GetCurrentCoinValue() - purchasedItems.Aggregate(0.0m, (x, c) => x + c.Price), 2);
        }

        public bool TryPurchase(Product product)
        {
            if (GetRemainingValue() - product.Price > 0)
            {
                logger.LogDebug($"Purchasing {product}");
                purchasedItems.Add(product);
                return true;
            }
            logger.LogWarning($"Could not purchase {product}");
            return false;
        }
    }
}