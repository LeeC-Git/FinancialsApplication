using System.Text.Json.Serialization;
using System.Text.Json;

namespace Personal_Finance_Tracker
{
    // This class is a custom JSON converter for the Transaction class and its derived classes (IncomeTransaction and ExpenseTransaction).
    // It handles the serialization and deserialization of Transaction objects to and from JSON format.
    internal class TransactionConverter : JsonConverter<Transaction>
    {
        // Override the Read method to deserialize a Transaction object from JSON
        public override Transaction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            string type = root.GetProperty("Type").GetString();

            Transaction transaction = type switch
            {
                "Income" => new IncomeTransaction(),
                "Expense" => new ExpenseTransaction(),
                _ => throw new JsonException($"Unknown transaction type: {type}")
            };

            transaction.Amount = root.GetProperty("Amount").GetDecimal();
            transaction.Category = root.GetProperty("Category").GetString();
            transaction.Description = root.GetProperty("Description").GetString();
            transaction.DateString = root.GetProperty("DateString").GetString();

            return transaction;
        }

        // Override the Write method to serialize a Transaction object to JSON
        public override void Write(Utf8JsonWriter writer, Transaction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Type", value.IsIncome() ? "Income" : "Expense");
            writer.WriteNumber("Amount", value.Amount);
            writer.WriteString("Category", value.Category);
            writer.WriteString("Description", value.Description);
            writer.WriteString("DateString", value.DateString);

            writer.WriteEndObject();
        }
    }
}
