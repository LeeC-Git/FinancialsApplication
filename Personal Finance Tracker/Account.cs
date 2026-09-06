using System.Text.Json.Serialization;

namespace Personal_Finance_Tracker
{
    internal class Account
    {
        // Name Property
        public string Name { get; set;  }

        // Private field to hold the list of transactions associated with the account, initialized as an empty list
        public List<Transaction> Transactions { get; set; } = new();

        // Paramaterless constructor to allow for serialization and deserialization of the Account class
        public Account() { }

        // Constructor ensuring name is provided when creating an account
        public Account(string name)
        {
            Name = name;
        }

        // Methods to add transactions, retrieve transactions, and calculate the account balance
        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        // Method to retrieve the list of transactions for the account
        public List<Transaction> GetTransactions()
        {
            return Transactions;
        }

        // Method to calculate the current balance by summing all transaction amounts
        public decimal GetBalance()
        {
            decimal total = 0;

            foreach (var t in Transactions)
            {
                if (t.IsIncome())
                {
                    total += t.Amount;
                }
                else
                {
                    total -= t.Amount;
                }
            }
            return total;
        }
    }
}
