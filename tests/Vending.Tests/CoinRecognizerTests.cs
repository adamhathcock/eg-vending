﻿using System;
using Vending.Domain;
using Vending.Features.Vending;
using Xunit;

namespace Vending.Tests
{
    public class CoinRecognizerTests
    {
        [Theory]
        [InlineData(0.6, 15, true)]
        [InlineData(0.6, 5, true)]
        [InlineData(0.1, 5, true)]
        [InlineData(0.1, 3, false)]
        public void SingleTokenTests(decimal weight, decimal diameter, bool success)
        {
            var recognizer = new CoinRecognizer();
            Assert.Equal(success, recognizer.Recognize(new Token(weight, diameter)) != null);
        }

        
        [Fact]
        public void RecognizeAllCoins()
        {
            var recognizer = new CoinRecognizer();
            foreach (var coin in Coin.Coins)
            {
                Assert.Same(coin, recognizer.Recognize(new Token(coin.MinWeight, coin.MinDiameter)));
                Assert.Same(coin, recognizer.Recognize(new Token(coin.MaxWeight, coin.MaxDiameter)));
            }
        }

    }
}
