using System.Collections.Generic;

namespace Vending
{
    public class CoinRecognizer : ICoinRecognizer
    {
        public static readonly List<Coin> Coins = new List<Coin>()
        {
            Coin.Fifty,
            Coin.Twenty,
            Coin.Ten
        };

        public Coin Recognize(Token token)
        {
            foreach (var coin in Coins)
            {
                if ((token.Weight <= coin.MaxWeight)
                    && (token.Weight >= coin.MinWeight)
                    && (token.Diameter <= coin.MaxDiameter)
                    && (token.Diameter >= coin.MinDiameter))
                {
                    return coin;
                }
            }
            return null;
        }
    }
}