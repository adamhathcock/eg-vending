namespace Vending
{
    public interface IVendingSession
    {
        bool TryAcceptToken(Token token);
        decimal GetCurrentCoinValue();
    }
}