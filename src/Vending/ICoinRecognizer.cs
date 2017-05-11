namespace Vending
{
    public interface ICoinRecognizer
    {
        Coin Recognize(Token token);
    }
}