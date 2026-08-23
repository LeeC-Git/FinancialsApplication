namespace Personal_Finance_Tracker
{
    internal class FinanceApplication
    {
        // Private static variable to hold the currently selected account, initialized to null, and a list to hold all created accounts
        private static Account currentAccount = null;
        private static List<Account> accounts = new();

        private readonly ILogger logger;

        public FinanceApplication(ILogger logger)
        {
            this.logger = logger;
        }

        public void Run() 
        {
            // bool variable to track whether the program is running or not, used to control the main menu loop
            bool running = true;

            logger.Log("Logger test");

            // Main menu loop which continues until the user chooses to exit the program
            while (running)
            {
                ShowMenu();
                string menuInput = Console.ReadLine();

                switch (menuInput)
                {
                    case "1":

                        Account newAccount = CreateAccount();
                        accounts.Add(newAccount);
                        currentAccount = newAccount;
                        break;


                    case "2":

                        ViewAccounts();
                        break;


                    case "3":

                        AddTransaction(currentAccount);
                        break;

                    case "4":

                        ViewTransactions(currentAccount);
                        break;

                    case "5":

                        ViewBalance(currentAccount);
                        break;

                    case "6":

                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select a valid menu option.");
                        break;
                }
            }

        }
        // Method which displays the programs main menu
        private void ShowMenu()
        {
            Console.WriteLine("---Personal Finance Tracker---");
            Console.WriteLine($"Current Account: '{(currentAccount != null ? currentAccount.Name : "None")}' ");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Select Account");
            Console.WriteLine("3. Add Transaction");
            Console.WriteLine("4. View Transactions");
            Console.WriteLine("5. View Balance");
            Console.WriteLine("6. Exit");
        }

        // Creates a new account by getting user input for the name and returns the created account object
        private Account CreateAccount()
        {
            string accountName;

            do
            {
                Console.Write("Enter account name: ");
                accountName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(accountName))
                {
                    Console.WriteLine("Account name cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(accountName));

            Account newAccount = new Account(accountName);
            Console.WriteLine($"Account '{accountName}' created successfully!");
            return newAccount;
        }

        // Method which prompts the user for transaction details, creates a new transaction object, and adds it to the account's transaction list
        private void AddTransaction(Account account)
        {
            Console.Write("Enter transaction amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter transaction category: ");
            string category = Console.ReadLine();

            Console.Write("Enter transaction description: ");
            string description = Console.ReadLine();

            Console.Write("Is this an income or an expense? (income/expense): ");
            string transactionType = Console.ReadLine().ToLower();

            // Prompt the user to choose between today's date or a custom date
            Console.Write("Use today's date? (y/n): ");
            string useToday = Console.ReadLine().ToLower();

            DateOnly date;

            if (useToday == "y")
            {
                date = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                date = PromptForDate();
            }

            Transaction newTransaction;

            if (transactionType == "income")
            {
                newTransaction = new IncomeTransaction(amount, category, date, description);
            }
            else if (transactionType == "expense")
            {
                newTransaction = new ExpenseTransaction(amount, category, date, description);
            }
            else
            {
                Console.WriteLine("Invalid transaction type. Transaction not added.");
                return;
            }

            account.AddTransaction(newTransaction);

            Console.WriteLine("Transaction added successfully!");
        }

        // Method which retrieves and displays all transactions for the given account, or a message if no transactions are found
        private void ViewTransactions(Account account)
        {
            List<Transaction> transactions = account.GetTransactions();

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                Console.WriteLine($"Transaction Summary for {account.Name}: ");
                Console.WriteLine("--------------------------------------------------");

                foreach (var transaction in transactions)
                {
                    string type = transaction.IsIncome() ? "Income" : "Expense";
                    Console.WriteLine($"{type} - {transaction.GetSummary()}");
                }
            }
        }

        // Method which retrieves and displays the current balance for the given account
        private void ViewBalance(Account account)
        {
            decimal balance = account.GetBalance();
            Console.WriteLine($"Current balance: £{balance:F2}");
        }

        // Method which prompts the user for a date in the format dd/MM/yyyy and validates the input, returning a DateOnly object
        private DateOnly PromptForDate()
        {
            bool isValidDate = false;
            DateOnly date = default;

            while (!isValidDate)
            {
                Console.Write("Enter transaction date (dd/MM/yyyy): ");
                string input = Console.ReadLine();

                if (DateOnly.TryParseExact(input, "dd/MM/yyyy", out date))
                {
                    isValidDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }

            return date;
        }

        // Method which displays a list of available accounts, prompts the user to select one, and updates the currently selected account if the selection is valid
        private void ViewAccounts()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts available. Please create an account first.");
                return;
            }

            Console.WriteLine("Available Accounts:");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name}");
            }

            Console.Write("Select an account number: ");
            string input = Console.ReadLine();

            // First validation: make sure input is a number
            if (!int.TryParse(input, out int index))
            {
                Console.WriteLine("Invalid Selection.");
                return;
            }

            // Second validation: make sure the number is within the range of available accounts
            if (index < 1 || index > accounts.Count)
            {
                Console.WriteLine("invalid selection.");
                return;
            }

            // Both validations passed -> update the currently selected account and inform the user
            currentAccount = accounts[index - 1];
            Console.WriteLine($"Account '{currentAccount.Name}' selected.");
        }

    }
}
