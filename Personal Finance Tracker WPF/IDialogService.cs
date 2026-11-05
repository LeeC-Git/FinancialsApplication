
namespace Personal_Finance_Tracker_WPF
{
    internal interface IDialogService
    {
        List<Account>? EditAccounts(IEnumerable<Account> current);
        List<Transaction>? EditTransactions(IEnumerable<Transaction> current);
    }
}