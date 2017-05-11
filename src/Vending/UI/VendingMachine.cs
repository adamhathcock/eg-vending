using System;
using Microsoft.Extensions.DependencyInjection;
using Vending.Domain;
using Vending.Features.Vending;

namespace Vending.UI
{
    public class VendingMachine
    {
        private readonly IServiceProvider serviceProvider;

        private IVendingSession vendingSession;

        public VendingMachine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            Start();
        }

        public void Start()
        {
            Console.WriteLine("Starting Vending Machine...");
            vendingSession = serviceProvider.GetRequiredService<IVendingSession>();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine($"Current Money: {vendingSession.GetRemainingValue()}");
                Console.WriteLine("Press [i] to insert coins.");
                Console.WriteLine("Press [p] to view/purchase a product.");
                Console.WriteLine("Press [c] to cancel.");
                Console.WriteLine("---> Secret! - Press [o] to coin dimensions.");

                var key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.C:
                        Refund();
                        return;
                    case ConsoleKey.I:
                        InsertCoins();
                        break;
                    case ConsoleKey.O:
                        Coins();
                        break;
                    case ConsoleKey.P:
                        if (ViewProducts())
                        {
                            Refund();
                            return;
                        }
                        break;
                }
            }
        }

        public void Coins()
        {
            foreach (var coin in Coin.Coins)
            {
                Console.WriteLine(coin);
            }
        }

        public void Refund()
        {
            foreach (var coin in vendingSession.MakeChange())
            {
                Console.WriteLine($"---> Refunding {coin}");
            }
            Console.WriteLine("THANK YOU");
        }


        public void InsertCoins()
        {
            while (true)
            {
                Console.WriteLine("INSERT COINS");
                Console.WriteLine();
                Console.WriteLine($"Current Money: {vendingSession.GetRemainingValue()}");
                Console.WriteLine();
                Console.WriteLine("Enter a value for the Coin Weight...");
                var weight = EnterDecimal();
                if (weight == null)
                {
                    return;
                }
                Console.WriteLine("Enter a value for the Coin Diameter...");
                var diameter = EnterDecimal();
                if (diameter == null)
                {
                    return;
                }
                if (vendingSession.TryAcceptToken(new Token(weight.Value, diameter.Value)))
                {
                    continue;
                }

                Console.WriteLine("Coin was not accepted.  Try again.");
                Console.WriteLine();
            }
        }

        public decimal? EnterDecimal()
        {
            while (true)
            {
                Console.WriteLine("Enter a number (e.g. 0.25) or just 'c' to cancel.");
                var line = Console.ReadLine();
                if (line.Equals("c"))
                {
                    return null;
                }
                decimal val;
                if (decimal.TryParse(line, out val))
                {
                    return val;
                }
                Console.WriteLine($"Invalid value: '{line}'");
            }
        }

        public bool ViewProducts()
        {
            while (true)
            {
                Console.WriteLine($"Current Money: {vendingSession.GetRemainingValue()}");
                for (var i = 0; i < Product.Products.Count; i++)
                {
                    Console.WriteLine($"Press [{i}] --> {Product.Products[i]}");
                }
                Console.WriteLine("Press [c] to cancel.");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                if (key == ConsoleKey.C)
                {
                    return false;
                }
                var k = (int)key;
                k -= 48; //convert to 0 index;
                if (k < Product.Products.Count)
                {
                    if (vendingSession.TryPurchase(Product.Products[k]))
                    {
                        Console.WriteLine($"Purchased {Product.Products[k]}");
                        Console.WriteLine();
                        return true;
                    }
                    Console.WriteLine($"Not enough money entered.");
                }
                else
                {
                    Console.WriteLine("Bad selection, try again.");
                }
                Console.WriteLine();
            }
        }
        
    }
}