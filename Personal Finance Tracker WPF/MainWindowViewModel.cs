using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Personal_Finance_Tracker_WPF
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;

        public decimal SelectedAccountBalance
        {
            get
            {
                return SelectedAccount?.GetBalance() ?? 0;
            }
        }

        public ObservableCollection<Account> Accounts { get; set; }

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            { 
                _selectedAccount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedAccountBalance));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            Accounts = new ObservableCollection<Account>(_accountService.GetAccounts());

        }
    }
}
