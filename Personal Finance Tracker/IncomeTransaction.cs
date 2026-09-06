namespace Personal_Finance_Tracker
{
    internal class IncomeTransaction : Transaction
    {
        // Constructor to ensure all properties are provided when creating an income transaction
        public IncomeTransaction(decimal amount, string category, DateOnly date, string description)
            : base(amount, category, date, description)
        {
        }

        // Parameterless constructor for serialization/deserialization
        public IncomeTransaction() : base()
        {
        }

        // Overriden method to indicate that this transaction is an income transaction
        public override bool IsIncome()
        {
            return true;
        }
    }
}
