namespace Personal_Finance_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ILogger logger = new ConsoleLogger();

            // SilentLogger does nothing
            // ILogger logger = new SilentLogger();

            var app = new FinanceApplication(logger);
            app.Run();
        }
    }
}
