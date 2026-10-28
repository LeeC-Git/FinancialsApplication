using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Personal_Finance_Tracker_WPF
{

    public partial class MainWindow : Window
    {

        private readonly DispatcherTimer _saveTimer;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new JsonAccountService(), new DialogService());

            _saveTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            _saveTimer.Tick += (_, _) => SaveData();
            _saveTimer.Start();

            Application.Current.Exit += (_, _) => SaveData();
        }

        private void SaveData()
        {
            (DataContext as MainWindowViewModel)?.Save();
        }
    }
}