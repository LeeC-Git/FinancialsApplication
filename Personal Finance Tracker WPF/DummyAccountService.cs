using System.Collections.Generic;

namespace Personal_Finance_Tracker_WPF
{
    internal class DummyAccountService  : IAccountService
    {
        public List<Account> GetAccounts()
        {
            return new List<Account>()
            {
                new Account("Savings", new List<Transaction>
                {
                    new Transaction(1000, "Initial Deposit"),
                    new Transaction(-200, "Shopping")
                }),
                new Account("Checking", new List<Transaction>
                {
                    new Transaction(500, "Paycheck"),
                    new Transaction(-50, "Gas Bills")
                })
            };
        }
    }
}
