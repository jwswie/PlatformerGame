using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        private float SpeedX, SpeedY, Friction = 0.88f, Speed = 2f;
        bool GameEnd = false;
        #region move player
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
        #endregion

        public GameWindow()
        {
            InitializeComponent();
            GameScreen.Height = 1000;
            GameScreen.Width = 1000;
            CanvasViewer.Height = 500;
            CanvasViewer.Width = 500;
            CanvasViewer.Focus();
            UpdateCamera();
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            GameTimer.Tick += GameTick;
            GameTimer.Start(); // запускаем таймер
        }

        private void GameTick(object sender, EventArgs e) // При каждом тике (кадре)
        {
            // Размеры карты
            double canvasWidth = GameScreen.ActualWidth;
            double canvasHeight = GameScreen.ActualHeight;

            // Размер + позиция игрока
            double playerWidth = Player.Width;
            double playerHeight = Player.Height;
            double playerX = Canvas.GetLeft(Player);
            double playerY = Canvas.GetTop(Player);

            // Передвигаемся вверх/вниз/влево/вправо на 2 (значение Speed)
            if (UpKeyPressed)
            {
                SpeedY += Speed;
                Player.Source = new BitmapImage(new Uri("Resources/GoFront.png", UriKind.Relative));
            }
            else if (DownKeyPressed)
            {
                SpeedY -= Speed;
                Player.Source = new BitmapImage(new Uri("Resources/GoBack.png", UriKind.Relative));
            }
            else if (LeftKeyPressed)
            {
                SpeedX += Speed;
                Player.Source = new BitmapImage(new Uri("Resources/GoRight.png", UriKind.Relative));
            }
            else if (RightKeyPressed)
            {
                SpeedX -= Speed;
                Player.Source = new BitmapImage(new Uri("Resources/GoLeft.png", UriKind.Relative));
            }
            else
            {
                Player.Source = new BitmapImage(new Uri("Resources/StandBack.png", UriKind.Relative));
            }



            SpeedX *= Friction; // Уменьшаем скорость с учетом трения
            SpeedY *= Friction;

            // Обновляем позицию игрока
            playerX += SpeedX;
            playerY -= SpeedY;

            // Устанавливаем новые координаты игрока
            Canvas.SetLeft(Player, playerX);
            Collide("x");
            Canvas.SetTop(Player, Canvas.GetTop(Player) - SpeedY);
            Collide("y");
            UpdateCamera();
        }

        #region Camera
        private void UpdateCamera()
        {
            // calculate offset of scrollViewer, relative to actual position of the player
            double offsetX = Canvas.GetLeft(Player) / 1.3;
            double offsetY = Canvas.GetTop(Player) / 1.3;
            // move the "camera"
            CanvasViewer.ScrollToHorizontalOffset(offsetX);
            CanvasViewer.ScrollToVerticalOffset(offsetY);
        }
        #endregion


        #region Collision

        private void Collide(string dir)
        {
            foreach (var child in GameScreen.Children.OfType<Image>())
            {
                if ((string)child.Tag == "Collide") // Объекты типа Collide(Колизия) 
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height); //Ищем игрока в канвасе
                    Rect ToCollide = new Rect(Canvas.GetLeft(child), Canvas.GetTop(child), child.Width, child.Height); //Ищем объект в канвасе

                    string Name = child.Name;

                    if (PlayerHB.IntersectsWith(ToCollide)) //Условия контакта с объектом
                    {
                        switch (dir)
                        {
                            case "x":
                                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - SpeedX);
                                SpeedX = 0;
                                break;
                            case "y":
                                Canvas.SetTop(Player, Canvas.GetTop(Player) + SpeedY);
                                SpeedY = 0;
                                break;
                        }
                    }

                    if (PlayerHB.IntersectsWith(ToCollide) && Name == "Finish") // Условия контакта с объектом
                    {

                        MessageBox.Show("Finished level");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Close();
                        GameTimer.Stop();
                        break;
                    }
                }
            }
        }
        #endregion
    }
}