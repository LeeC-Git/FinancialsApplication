namespace Personal_Finance_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // You can switch between different logger implementations by commenting/uncommenting the appropriate line below.
            ILogger logger = new ConsoleLogger();
            // ILogger logger = new SilentLogger();

            // Creates an instance of the JsonAccountPersistence class, which implements the IAccountPersistence interface, to handle saving and loading account data in JSON format.
            IAccountPersistence persistence = new JsonAccountPersistence(logger);

            // Creates an instance of the FinanceApplication class, passing in the logger and persistence instances.
            var app = new FinanceApplication(logger, persistence);

            app.Run();
        }
    }
}
