using System.Collections.Generic;

namespace Vending
{
    public interface IVendingSession
    {
        bool TryAcceptToken(Token token);
        decimal GetRemainingValue();
        bool TryPurchase(Product product);
        IEnumerable<Coin> MakeChange();
    }
}