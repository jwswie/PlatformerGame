using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace PlatformerGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer(); // Таймер
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        private float SpeedX, SpeedY, Friction = 0.88f, Speed = 2;

        private void KeyBoardDown(object sender, KeyEventArgs e) // Клавиша нажата
        {
            switch (e.Key) 
            {
                case Key.W:
                    UpKeyPressed = true;
                    break;
                case Key.S: 
                    DownKeyPressed = true; 
                    break;
                case Key.D:
                    LeftKeyPressed = true;
                    break;
                case Key.A:
                    RightKeyPressed = true;
                    break;
            }
        }

        private void KeyBoardUp(object sender, KeyEventArgs e) // Клавиша отпущена
        {
            switch (e.Key)
            {
                case Key.W:
                    UpKeyPressed = false;
                    break;
                case Key.S:
                    DownKeyPressed = false;
                    break;
                case Key.D:
                    LeftKeyPressed = false;
                    break;
                case Key.A:
                    RightKeyPressed = false;
                    break;
            }
        }

        public GameWindow()
        {
            InitializeComponent();
            GameScreen.Focus();
            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start(); // запускаем таймер
        }
        private void GameTick(object sender, EventArgs e) // При каждом тике (кадре)
        {
            // Передвигаемся вверх/вниз/влево/вправо на 2 (значение Speed)

            if (UpKeyPressed) 
            {
                SpeedY += Speed;
                Player.Source = new BitmapImage(new Uri("C:\\Users\\pktb\\source\\repos\\PlatformerGame\\PlatformerGame\\GoFront.png", UriKind.Absolute));
            }
            else if (DownKeyPressed) 
            {
                SpeedY -= Speed;
                Player.Source = new BitmapImage(new Uri("C:\\Users\\pktb\\source\\repos\\PlatformerGame\\PlatformerGame\\GoBack.png", UriKind.Absolute));
            }
            else if(LeftKeyPressed) 
            {
                SpeedX += Speed;
                Player.Source = new BitmapImage(new Uri("C:\\Users\\pktb\\source\\repos\\PlatformerGame\\PlatformerGame\\GoRight.png", UriKind.Absolute));
            }
            else if(RightKeyPressed) 
            {
                SpeedX -= Speed;
                Player.Source = new BitmapImage(new Uri("C:\\Users\\pktb\\source\\repos\\PlatformerGame\\PlatformerGame\\GoLeft.png", UriKind.Absolute));
            }
            else 
            {
                Player.Source = new BitmapImage(new Uri("C:\\Users\\pktb\\source\\repos\\PlatformerGame\\PlatformerGame\\StandBack.png", UriKind.Absolute));
            }


            SpeedX *= Friction; // Уменьшаем скорость с учетом терния
            SpeedY *= Friction;

            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + SpeedX);
            Canvas.SetTop(Player, Canvas.GetTop(Player) - SpeedY);
        }
    }
}
