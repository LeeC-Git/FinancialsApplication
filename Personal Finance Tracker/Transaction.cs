namespace Personal_Finance_Tracker
{
    internal abstract class Transaction
    {
        // Properties
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }

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

        // Method to determine if the transaction is an income transaction, defaulting to false for the base class
        public abstract bool IsIncome();

    }
}
