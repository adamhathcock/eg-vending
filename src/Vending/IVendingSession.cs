namespace Vending
{
    public interface IVendingSession
    {
        bool TryAcceptToken(Token token);
        double GetCurrentCoinValue();
    }
}