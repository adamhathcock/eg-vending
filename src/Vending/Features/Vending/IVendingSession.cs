using System.Collections.Generic;
using Vending.Domain;

namespace Vending.Features.Vending
{
    public interface IVendingSession
    {
        bool TryAcceptToken(Token token);
        decimal GetRemainingValue();
        bool TryPurchase(Product product);
        IEnumerable<Coin> MakeChange();
    }
}