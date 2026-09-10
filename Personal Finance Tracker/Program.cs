using Microsoft.Extensions.DependencyInjection;

namespace Personal_Finance_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
         
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
            serviceCollection.AddSingleton<IAccountPersistence, JsonAccountPersistence>();

            serviceCollection.AddTransient<FinanceApplication>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<FinanceApplication>();

            app.Run();
        }
    }
}
