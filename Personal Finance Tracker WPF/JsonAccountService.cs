using System.IO;
using System.Text.Json;

namespace Personal_Finance_Tracker_WPF
{
    internal class JsonAccountService :IAccountService
    {
        private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "accounts.json");

        public List<Account> GetAccounts()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return new List<Account>();
                }
                    
                var json = File.ReadAllText(FilePath);

                return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
            }
            catch (JsonException)
            {
                return new List<Account>();
            }
        }

        public void SaveAccounts(IEnumerable<Account> accounts)
        {
            var json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(FilePath, json);
        }
    }

}
