namespace Personal_Finance_Tracker_WPF
{
    internal class Account
    {
        public string Name { get; set; }

        public List<Transaction> Transactions { get; set; } = new();


        public Account(string name, List<Transaction> transactions)
        {
            Name = name;
            Transactions = transactions;
        }

        public decimal GetBalance()
        {
            decimal balance = 0;
            foreach (var transaction in Transactions)
            {
                balance += transaction.Amount;
            }

            return balance;
        }
    }
}
