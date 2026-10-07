using System.Windows;
using System.Windows.Controls;

namespace Personal_Finance_Tracker_WPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AccountsListLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountsListLeft.SelectedItem is ListBoxItem selectedAccount)
            {
                string accountName = selectedAccount.Content.ToString();

                AccountNameText.Text = $"Account: {accountName}";

                TransactionsList.Items.Clear();

                if (accountTransactions.ContainsKey(accountName))
                {
                    foreach (var transaction in accountTransactions[accountName])
                    {
                        TransactionsList.Items.Add(transaction);
                    }
                }

                if (accountBalances.ContainsKey(accountName))
                {
                    AccountBalanceText.Text = $"Balance: {accountBalances[accountName]:C2}";
                }
            }
        }

        private Dictionary<string, List<string>> accountTransactions = new Dictionary<string, List<string>>
        {
            {
                "Checking", new List<string>
                {
                    "Tesco - £25.00",
                    "Amazon - £14.99",
                    "Salary + £2,000.00"
                }
            },

            {   "Savings", new List<string>
                {
                    "Interest + £5.00",
                    "Transfer from Checking + £100.00"
                }
            },

            {
                "Credit Card", new List<string>
                {
                    "Fuel - £45.00",
                    "Restaurant - £30.00"
                }
            },

            {
                "Investment", new List<string>
                {
                    "Bought ETF - £200.00",
                    "Dividend + £15.00"
                }
            }
        };

        private Dictionary<string, decimal> accountBalances = new Dictionary<string, decimal>
        {
            { "Checking", 1250.75m },
            { "Savings", 5000.00m },
            { "Credit Card", -350.25m },
            { "Investment", 12000.50m }
        };

    }
}