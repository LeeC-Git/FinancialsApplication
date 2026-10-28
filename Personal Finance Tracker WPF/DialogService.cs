using System.Windows;

namespace Personal_Finance_Tracker_WPF
{
    internal class DialogService : IDialogService
    {

        public List<Account>? EditAccounts(IEnumerable<Account> current)
        {
            var popup = new AccountEditWindow(current) { Owner = Application.Current.MainWindow };
            return popup.ShowDialog() == true ? popup.WorkingAccounts.ToList() : null;
        }

        public List<Transaction>? EditTransactions(IEnumerable<Transaction> current)
        {
            var popup = new TransactionEditWindow(current) { Owner = Application.Current.MainWindow };
            return popup.ShowDialog() == true ? popup.WorkingTransactions.ToList() : null;
        }

    }
}
