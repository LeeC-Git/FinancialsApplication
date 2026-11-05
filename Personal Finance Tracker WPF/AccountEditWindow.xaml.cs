using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace Personal_Finance_Tracker_WPF
{
    /// <summary>
    /// Interaction logic for AccountEditWindow.xaml
    /// </summary>
    internal partial class AccountEditWindow : Window
    {
        public ObservableCollection<Account> WorkingAccounts { get; }

        public RelayCommand<string> AddCommand { get; }
        public RelayCommand<Account> RemoveCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AccountEditWindow(IEnumerable<Account> currentAccounts)
        {

            WorkingAccounts = new ObservableCollection<Account>(currentAccounts);

            AddCommand = new RelayCommand<string>(
                name =>
                {
                    WorkingAccounts.Add(new Account(name!.Trim(), new List<Transaction>()));
                    NewNameBox.Clear();
                    NewNameBox.Focus();
                },
                name => !string.IsNullOrWhiteSpace(name));

            RemoveCommand = new RelayCommand<Account>(account => WorkingAccounts.Remove(account!), account => account != null);

            OkCommand = new RelayCommand(() => DialogResult = true);
            CancelCommand = new RelayCommand(() => DialogResult = false);

            InitializeComponent();
            DataContext = this;
            NewNameBox.Focus();
        }
    }
}
