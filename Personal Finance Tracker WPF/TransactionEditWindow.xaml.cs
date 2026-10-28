using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personal_Finance_Tracker_WPF
{
    /// <summary>
    /// Interaction logic for TransactionEditWindow.xaml
    /// </summary>
    internal partial class TransactionEditWindow : Window
    {

        public ObservableCollection<Transaction> WorkingTransactions { get; }

        public RelayCommand AddCommand { get; }
        public RelayCommand<Transaction> UpdateCommand { get; }
        public RelayCommand<Transaction> RemoveCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public TransactionEditWindow(IEnumerable<Transaction> currentTransactions)
        {

            WorkingTransactions = new ObservableCollection<Transaction>(currentTransactions.Select(t => new Transaction(t.Amount, t.Description)));

            AddCommand = new RelayCommand(() =>
            {
                var t = ReadInput();

                if (t == null)
                {
                    return;
                }

                WorkingTransactions.Add(t);
                TransactionsList.SelectedItem = t;
            });

            UpdateCommand = new RelayCommand<Transaction>(
            selected =>
            {
                var t = ReadInput();
                if (t == null)
                {
                    return;
                }

                // Replace the item so the list refreshes

                var index = WorkingTransactions.IndexOf(selected!);
                WorkingTransactions[index] = t;
                TransactionsList.SelectedIndex = index;
            },
            selected => selected != null);

            RemoveCommand = new RelayCommand<Transaction>(
            selected => WorkingTransactions.Remove(selected!),
            selected => selected != null);

            OkCommand = new RelayCommand(() => DialogResult = true);
            CancelCommand = new RelayCommand(() => DialogResult = false);

            InitializeComponent();
            DataContext = this;
            DescriptionBox.Focus();
        }

        private Transaction ReadInput()
        {
            if (string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                MessageBox.Show("Please enter a description.");

                return null;
            }

            if (!decimal.TryParse(AmountBox.Text, out var amount))
            {
                MessageBox.Show("Please enter a valid amount.");

                return null;
            }

            return new Transaction(amount, DescriptionBox.Text.Trim());
        }
    }
}
