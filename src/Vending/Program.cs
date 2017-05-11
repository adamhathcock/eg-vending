using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrutor;
using Serilog;
using Vending.Features.Vending;
using Vending.UI;

namespace Vending
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.Scan(x => x.FromAssemblyOf<VendingSession>().AddClasses().AsMatchingInterface());
            serviceCollection.AddTransient<VendingMachine>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logging = serviceProvider.GetRequiredService<ILoggerFactory>();

            var config = new LoggerConfiguration();
            config.MinimumLevel.Debug();
            config.WriteTo.LiterateConsole();
            logging.AddSerilog(config.CreateLogger());

            while (true)
            {
                var vendingMachine = serviceProvider.GetRequiredService<VendingMachine>();
                vendingMachine.Run();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}