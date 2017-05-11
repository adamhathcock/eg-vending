using System;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Vending.Tests
{
    public class VendingSessionTests
    {
        private readonly MockRepository repository = new MockRepository(MockBehavior.Strict);
        private readonly Mock<ICoinRecognizer> coinRecognizer;
        private readonly Mock<ILogger<CoinRecognizer>> logger;

        public VendingSessionTests()
        {
            coinRecognizer = repository.Create<ICoinRecognizer>();
            logger = repository.Create<ILogger<CoinRecognizer>>(MockBehavior.Loose);
        }

        [Fact]
        public void ExpectSessionMemory_TryAcceptToken()
        {
            var token50 = new Token(0,0);
            var token20 = new Token(0, 0);
            var tokenUnknown = new Token(0, 0);

            coinRecognizer.Setup(x => x.Recognize(token50)).Returns(Coin.Fifty);
            coinRecognizer.Setup(x => x.Recognize(token20)).Returns(Coin.Twenty);
            coinRecognizer.Setup(x => x.Recognize(tokenUnknown)).Returns((Coin)null);

            var vendingSession = new VendingSession(coinRecognizer.Object, logger.Object);
            Assert.True(vendingSession.TryAcceptToken(token50));
            Assert.True(vendingSession.TryAcceptToken(token20));
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.Equal(0.7m, vendingSession.GetCurrentCoinValue());
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.Equal(0.7m, vendingSession.GetCurrentCoinValue());
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.Equal(0.7m, vendingSession.GetCurrentCoinValue());
        }

        [Fact]
        public void ExpectSessionMemory_TryAcceptToken_Purchase_Chips()
        {
            var token50 = new Token(0, 0);
            var token20 = new Token(0, 0);
            var tokenUnknown = new Token(0, 0);

            coinRecognizer.Setup(x => x.Recognize(token50)).Returns(Coin.Fifty);
            coinRecognizer.Setup(x => x.Recognize(token20)).Returns(Coin.Twenty);
            coinRecognizer.Setup(x => x.Recognize(tokenUnknown)).Returns((Coin)null);

            var vendingSession = new VendingSession(coinRecognizer.Object, logger.Object);
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.Equal(0.0m, vendingSession.GetCurrentCoinValue());
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.Equal(0.0m, vendingSession.GetCurrentCoinValue());
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.True(vendingSession.TryAcceptToken(token50));
            Assert.True(vendingSession.TryAcceptToken(token20));
            Assert.Equal(0.7m, vendingSession.GetCurrentCoinValue());
            Assert.Equal(0.7m, vendingSession.GetRemainingValue());

            Assert.True(vendingSession.TryPurchase(Product.Chips));
            Assert.Equal(0.2m, vendingSession.GetRemainingValue());
        }

        [Fact]
        public void ExpectSessionMemory_TryAcceptToken_Candy_And_Cola()
        {
            var token50 = new Token(0, 0);
            var token20 = new Token(0, 0);
            var tokenUnknown = new Token(0, 0);

            coinRecognizer.Setup(x => x.Recognize(token50)).Returns(Coin.Fifty);
            coinRecognizer.Setup(x => x.Recognize(token20)).Returns(Coin.Twenty);
            coinRecognizer.Setup(x => x.Recognize(tokenUnknown)).Returns((Coin)null);

            var vendingSession = new VendingSession(coinRecognizer.Object, logger.Object);
            Assert.False(vendingSession.TryAcceptToken(tokenUnknown));
            Assert.True(vendingSession.TryAcceptToken(token50));
            Assert.True(vendingSession.TryAcceptToken(token20));
            Assert.Equal(0.7m, vendingSession.GetCurrentCoinValue());


            Assert.Throws<ArgumentException>(() => vendingSession.TryAcceptToken(token50));

            Assert.True(vendingSession.TryPurchase(Product.Candy));
            Assert.Equal(0.05m, vendingSession.GetRemainingValue());

            Assert.False(vendingSession.TryPurchase(Product.Cola));
            Assert.Equal(0.05m, vendingSession.GetRemainingValue());
        }
    }
}