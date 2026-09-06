using System.Text.Json;

namespace Personal_Finance_Tracker

{
    internal class JsonAccountPersistence : IAccountPersistence
    {
        // File path for storing account data in JSON format
        private const string FilePath = "accounts.json";

        // JsonSerializerOptions instance to configure serialization and deserialization behavior
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            Converters = { new TransactionConverter() }
        };

        // Logger instance for logging operations
        private readonly ILogger logger;

        // Constructor to inject the logger
        public JsonAccountPersistence(ILogger logger)
        {
            this.logger = logger;
        }

        // Method to save a list of accounts to a JSON file
        public void Save(List<Account> accounts)
        {
            try
            {
                string json = JsonSerializer.Serialize(accounts, Options);
                File.WriteAllText(FilePath, json);
                logger.Log($"Accounts saved successfully to {FilePath}.");
            }
            catch (Exception ex)
            {
                logger.Log($"Error saving accounts: {ex.Message}");
                throw;
            }
        }

        // Method to load a list of accounts from a JSON file
        public List<Account> Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    logger.Log($"No file found at {FilePath}. Returning an empty account list.");
                    return new List<Account>();
                }

                string json = File.ReadAllText(FilePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    logger.Log($"No file found at {FilePath}. Returning an empty account list.");
                    return new List<Account>();
                }

                var loaded = JsonSerializer.Deserialize<List<Account>>(json, Options);

                if (loaded == null)
                {
                    logger.Log($"Failed to deserialize accounts from {FilePath}. Returning an empty account list.");
                    return new List<Account>();
                }

                logger.Log($"Accounts loaded successfully from {FilePath}.");
                return loaded;
            }
            catch (Exception ex)
            {
                logger.Log($"Error loading accounts: {ex.Message}");
                throw;
            }
        }
    }
}
