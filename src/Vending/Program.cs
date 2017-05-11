using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrutor;
using Serilog;

namespace Vending
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.Scan(x => x.FromAssemblyOf<VendingSession>().AddClasses().AsMatchingInterface());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logging = serviceProvider.GetRequiredService<ILoggerFactory>();

            var config = new LoggerConfiguration();
            config.MinimumLevel.Debug();
            config.WriteTo.LiterateConsole();
            logging.AddSerilog(config.CreateLogger());

            var vendingSession = serviceProvider.GetRequiredService<IVendingSession>();

            vendingSession.TryAcceptToken(new Token(0.6m, 15));
            vendingSession.TryAcceptToken(new Token(0.6m, 5));
            vendingSession.TryAcceptToken(new Token(0.1m, 5));


            Console.ReadLine();
        }
    }
}