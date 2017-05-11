using Vending.Domain;

namespace Vending.Features.Vending
{
    public interface ICoinRecognizer
    {
        Coin Recognize(Token token);
    }
}