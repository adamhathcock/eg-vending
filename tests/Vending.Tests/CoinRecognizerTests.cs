using System;
using Xunit;

namespace Vending.Tests
{
    public class CoinRecognizerTests
    {
        [Theory]
        [InlineData(0.6, 15, true)]
        [InlineData(0.6, 5, true)]
        [InlineData(0.1, 5, false)]
        public void SingleTokenTests(double weight, double diameter, bool success)
        {
            var recognizer = new CoinRecognizer();
            Assert.Equal(success, recognizer.Recognize(new Token(weight, diameter)) != null);
        }

        
        [Fact]
        public void RecognizeAllCoins()
        {
            var recognizer = new CoinRecognizer();
            foreach (var coin in CoinRecognizer.Coins)
            {
                Assert.Same(coin, recognizer.Recognize(new Token(coin.MinWeight, coin.MinDiameter)));
                Assert.Same(coin, recognizer.Recognize(new Token(coin.MaxWeight, coin.MaxDiameter)));
            }
        }

    }
}
