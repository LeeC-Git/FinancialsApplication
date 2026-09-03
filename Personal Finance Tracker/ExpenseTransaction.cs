namespace Personal_Finance_Tracker
{
    internal class ExpenseTransaction : Transaction
    {
        // Constructor to ensure all properties are provided when creating an expense transaction
        public ExpenseTransaction(decimal amount, string category, DateOnly date, string description)
            : base(amount, category, date, description)
        {
        }

        // Parameterless constructor for serialization/deserialization
        public ExpenseTransaction() : base()
        {
        }

        // Overriden method to indicate that this transaction is not an income transaction
        public override bool IsIncome()
        {
            return false;
        }
    }
}
