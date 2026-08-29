namespace Personal_Finance_Tracker
{
    internal abstract class Transaction
    {
        // Properties
        public decimal Amount { get; private set; }
        public string Category { get; private set; }
        public DateOnly Date { get; private set; }
        public string Description { get; private set; }

        // Constructor to ensure all properties are provided when creating a transaction
        public Transaction(decimal amount, string category, DateOnly date, string description)
        {
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }

        // Method to return the transaction details in a string
        public string GetSummary()
        {
            return $"£{Amount:F2} - {Category} - {Description} - {Date.ToString("dd/MM/yyyy")}";
        }

        // abstract method to determine if the transaction is an income or expense, to be implemented in derived classes
        public abstract bool IsIncome();

    }
}
