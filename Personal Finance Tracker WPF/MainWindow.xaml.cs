using System.Windows;
using System.Windows.Controls;

namespace Personal_Finance_Tracker_WPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DummyAccountService());
        }
    }
}