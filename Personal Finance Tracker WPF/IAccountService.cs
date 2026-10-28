namespace Personal_Finance_Tracker_WPF
{
    internal interface IAccountService
    {
        List<Account> GetAccounts();
        void SaveAccounts(IEnumerable<Account> accounts);

    }
}
