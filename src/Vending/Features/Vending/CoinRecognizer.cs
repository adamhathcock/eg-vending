using Vending.Domain;

namespace Vending.Features.Vending
{
    public class CoinRecognizer : ICoinRecognizer
    {

        public Coin Recognize(Token token)
        {
            foreach (var coin in Coin.Coins)
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