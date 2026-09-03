using System.Text.Json.Serialization;

namespace Personal_Finance_Tracker
{
    internal abstract class Transaction
    {
        // Properties
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        // The Date property is marked with the [JsonIgnore] attribute to prevent it from being serialized directly.
        // Instead, the DateString property is used for serialization and deserialization
        // converting the DateOnly object to a string in the format "dd/MM/yyyy" and parsing it back to a DateOnly object when deserializing.
        // this approach is necessary because the System.Text.Json library does not natively support the DateOnly type.
        [JsonIgnore]
        public DateOnly Date { get; set; }

        // This property is used for serialization and deserialization of the Date property in the Transaction class.
        // It converts the DateOnly object to a string in the format "dd/MM/yyyy" for serialization
        // and parses a string in the same format back to a DateOnly object for deserialization.
        public string DateString
        {
            get => Date.ToString("dd/MM/yyyy");
            set => Date = DateOnly.Parse(value);
        }

        // Parameterless constructor for serialization/deserialization
        public Transaction() { } 

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
