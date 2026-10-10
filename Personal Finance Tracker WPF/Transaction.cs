namespace Personal_Finance_Tracker_WPF
{
    internal class Transaction
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string DisplayText => $"{Description}: {Amount:C2}";

        public Transaction(decimal amount, string description)
        {
            Amount = amount;
            Description = description;
        }
    }
}
