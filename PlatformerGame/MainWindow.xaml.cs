using System.Windows;

namespace PlatformerGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayerStatistic _playerStatistic;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(PlayerStatistic playerStatistic)
        {
            InitializeComponent();

            _playerStatistic = playerStatistic;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_playerStatistic == null)
            {
                _playerStatistic = new PlayerStatistic(0, 0);
            }

            MessageBox.Show($"The last score: {_playerStatistic.Score}\nCollected coins in the last level: " +
                $"{_playerStatistic.CollectedCoins}", "Statistic", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
