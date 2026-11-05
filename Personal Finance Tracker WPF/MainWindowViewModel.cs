using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Personal_Finance_Tracker_WPF
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogs;

        public decimal SelectedAccountBalance
        {
            get
            {
                return SelectedAccount?.GetBalance() ?? 0;
            }
        }

        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; } = new();

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            { 
                _selectedAccount = value;
                OnPropertyChanged();
                RefreshTransactions();
                EditTransactionsCommand.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand EditAccountsCommand { get; }
        public RelayCommand EditTransactionsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel(IAccountService accountService, IDialogService dialogs)
        {
            _accountService = accountService;
            _dialogs = dialogs;
            Accounts = new ObservableCollection<Account>(_accountService.GetAccounts());

            EditAccountsCommand = new RelayCommand(EditAccounts);
            EditTransactionsCommand = new RelayCommand(EditTransactions, () => SelectedAccount != null);

        }

        private void EditAccounts()
        {
            var result = _dialogs.EditAccounts(Accounts);

            if (result == null)
            {
                return;
            }

            foreach (var a in Accounts.Where(a => !result.Contains(a)).ToList())
            {
                Accounts.Remove(a);
            }
                
            foreach (var a in result.Where(a => !Accounts.Contains(a)).ToList()) 
            {
                Accounts.Add(a);
            }
        }

        private void EditTransactions()
        {
            var result = _dialogs.EditTransactions(SelectedAccount!.Transactions);

            if (result == null)
            {
                return;
            }

            SelectedAccount.Transactions.Clear();
            SelectedAccount.Transactions.AddRange(result);
            RefreshTransactions();
        }

        private void RefreshTransactions()
        {
            Transactions.Clear();

            if (SelectedAccount != null)
            {
                foreach (var t in SelectedAccount.Transactions)
                {
                    Transactions.Add(t);
                }
                    
            }

            OnPropertyChanged(nameof(SelectedAccountBalance));
        }

        public void Save()
        {
            _accountService.SaveAccounts(Accounts);
        }
    }
}
