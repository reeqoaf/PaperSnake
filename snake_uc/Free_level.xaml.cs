using snake_uc.GameEntitites;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace snake_uc
{
    /// <summary>
    /// Interaction logic for Free_level.xaml
    /// </summary>
    public partial class Free_level : Window
    {
        private int elementSize = 60;
        private int NumberofColumns;
        private int NumberofRows;
        private int gameHeight;
        private int gameWidth;
        private bool gamePaused;
        private int score;
        private Direction currentDirection;
        List<SnakeElement> snakeElements;
        Fruit fruit;
        private SnakeElement tailBackup;

        private Random rand;
        DispatcherTimer gameLoopTimer;

        Image whole_black;
        string color;
        public Free_level()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            InitializeGame();
            base.OnContentRendered(e);
        }

        private void ParseColor()
        {
            switch (File.ReadAllLines("Data/config.txt").ToList().First().Split("=").Last())
            {
                case "0":
                    color = "purple";
                    break;
                case "1":
                    color = "green";
                    break;
                case "2":
                    color = "yellow";
                    break;
                default:
                    color = "purple";
                    break;
            }
        }

        private void InitializeGame()
        {
            rand = new Random(DateTime.Now.Millisecond / DateTime.Now.Second);
            whole_black = new Image();
            whole_black.Source = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Pictures/background_whole_black.png"));
            whole_black.Opacity = 0.7;
            ParseColor();
            score = 0;
            InitializeTimer();
            DrawGameWorld();
            InitaliaizeSnake();
            DrawSnake();
        }

        private void DrawSnake()
        {
            foreach (var snakeElement in snakeElements)
            {
                if (!GameWorld.Children.Contains(snakeElement.element))
                {
                    GameWorld.Children.Add(snakeElement.element);
                }

                Canvas.SetLeft(snakeElement.element, snakeElement.X);
                Canvas.SetTop(snakeElement.element, snakeElement.Y);
            }
        }

        private void InitaliaizeSnake()
        {
            snakeElements = new List<SnakeElement>();
            currentDirection = Direction.Down;

            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().SetCords((NumberofColumns / 2) * elementSize, (NumberofRows) / 2 * elementSize);
            snakeElements.Last().ToHead();
            snakeElements.Last().Rotate(currentDirection.ToString());

            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().SetCords((NumberofColumns / 2) * elementSize, (NumberofRows) / 2 * elementSize - elementSize);
            snakeElements.Last().ToBody();
            snakeElements.Last().Rotate(currentDirection.ToString());

            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().SetCords((NumberofColumns / 2) * elementSize, (NumberofRows) / 2 * elementSize - 2 * elementSize);
            snakeElements.Last().ToBody();
            snakeElements.Last().Rotate(currentDirection.ToString());

            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().SetCords((NumberofColumns / 2) * elementSize, (NumberofRows) / 2 * elementSize - 3 * elementSize);
            snakeElements.Last().ToBody();
            snakeElements.Last().Rotate(currentDirection.ToString());

            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().SetCords((NumberofColumns / 2) * elementSize, (NumberofRows) / 2 * elementSize - 4 * elementSize);
            snakeElements.Last().ToTail();
            snakeElements.Last().Rotate(currentDirection.ToString());
        }

        private void DrawGameWorld()
        {
            gameHeight = (int)GameWorld.ActualHeight;
            gameWidth = (int)GameWorld.ActualWidth;
            NumberofColumns = gameWidth / elementSize + 1;
            NumberofRows = gameHeight / elementSize + 1;

            for (int i = 0; i < NumberofRows; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Blue;
                line.X1 = 0;
                line.Y1 = i * elementSize;
                line.X2 = gameWidth;
                line.Y2 = i * elementSize;
                GameWorld.Children.Add(line);
            }
            for (int i = 0; i < NumberofColumns; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Blue;
                line.X1 = i * elementSize;
                line.Y1 = 0;
                line.X2 = i * elementSize;
                line.Y2 = gameHeight;
                GameWorld.Children.Add(line);
            }
            //DrawWalls();
        }
        private void DrawWalls()
        {
            for(int i = 0; i < gameWidth; i += elementSize)
            {
                Rectangle shape = new Rectangle();
                shape.Width = elementSize;
                shape.Height = elementSize;
                shape.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Pictures/cobble.png"))
                };
                GameWorld.Children.Add(shape);
                Canvas.SetLeft(shape, i);
                Canvas.SetTop(shape, 0);
            }
            for (int i = 0; i < gameWidth; i += elementSize)
            {
                Rectangle shape = new Rectangle();
                shape.Width = elementSize;
                shape.Height = elementSize;
                shape.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Pictures/cobble.png"))
                };
                GameWorld.Children.Add(shape);
                Canvas.SetLeft(shape, i);
                Canvas.SetTop(shape, gameHeight - elementSize);
            }
            for (int i = 0; i < gameHeight; i += elementSize)
            {
                Rectangle shape = new Rectangle();
                shape.Width = elementSize;
                shape.Height = elementSize;
                shape.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Pictures/cobble.png"))
                };
                GameWorld.Children.Add(shape);
                Canvas.SetLeft(shape, 0);
                Canvas.SetTop(shape, i);
            }
            for (int i = 0; i < gameHeight; i += elementSize)
            {
                Rectangle shape = new Rectangle();
                shape.Width = elementSize;
                shape.Height = elementSize;
                shape.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Pictures/cobble.png"))
                };
                GameWorld.Children.Add(shape);
                Canvas.SetLeft(shape, gameWidth - elementSize);
                Canvas.SetTop(shape, i);
            }
        }

        public void InitializeTimer()
        {
            gameLoopTimer = new DispatcherTimer();
            gamePaused = false;
            gameLoopTimer.Interval = TimeSpan.FromSeconds(0.2);
            gameLoopTimer.Tick += MainGameLoop;
            gameLoopTimer.Start();
        }
        private void MainGameLoop(object sender, EventArgs e)
        {
            try
            {
                MoveSnake();
                CheckCollision();
                DrawSnake();
                CreateFruit();
                DrawFruit();
            }
            catch (Exception error)
            {
                File.WriteAllText($"logs/log{DateTime.Now.ToString().Replace(":", "-").Replace(".", "-").Replace(" ", "-")}.txt", error.ToString());
            }

        }

        private void DrawFruit()
        {
            if (fruit == null) return;
            if (!GameWorld.Children.Contains(fruit.element))
            {
                GameWorld.Children.Add(fruit.element);
            }

            Canvas.SetLeft(fruit.element, fruit.X);
            Canvas.SetTop(fruit.element, fruit.Y);

        }

        private async void CreateFruit()
        {
            if (fruit != null) return;
            int randFruit = rand.Next(0, 100);
            if (randFruit < 70)
            {
                fruit = new Apple(elementSize)
                {
                    X = rand.Next(0, NumberofColumns - 1) * elementSize,
                    Y = rand.Next(0, NumberofRows - 1) * elementSize
                };
            }
            else if (randFruit < 90)
            {
                fruit = new Banana(elementSize)
                {
                    X = rand.Next(0, NumberofColumns - 1) * elementSize,
                    Y = rand.Next(0, NumberofRows - 1) * elementSize
                };
            }
            else if (randFruit < 100)
            {
                fruit = new Grape(elementSize)
                {
                    X = rand.Next(0, NumberofColumns - 1) * elementSize,
                    Y = rand.Next(0, NumberofRows - 1) * elementSize
                };
            }
            if (snakeElements.Find(element => element.X == fruit.X || element.Y == fruit.Y) != null)
            {
                fruit = null;
                await Task.Delay(1);
                CreateFruit();
            }
        }

        private void CheckCollision()
        {
            //CheckColllisonWithWolrldBounds();
            CheckCollisionWithSelf();
            CheckCollisionWithWorldItems();
        }

        private void CheckCollisionWithWorldItems()
        {
            if (fruit == null) return;
            SnakeElement head = snakeElements[0];
            if (head.X == fruit.X && head.Y == fruit.Y)
            {
                GameWorld.Children.Remove(fruit.element);
                GrowSnake();
                score += fruit.score;
                fruit = null;
            }
        }

        private void GrowSnake()
        {
            snakeElements.Last().ToBody();
            snakeElements.Add(new SnakeElement(elementSize, color));
            snakeElements.Last().ToTail();
            snakeElements.Last().shape.RenderTransform = snakeElements[snakeElements.Count - 2].shape.RenderTransform;
            snakeElements.Last().SetCords(tailBackup.X, tailBackup.Y);
        }

        private void CheckCollisionWithSelf()
        {
            SnakeElement snakeHead = snakeElements.Find(item => item.IsHead == true);
            if (snakeHead != null)
            {
                foreach (var snakeElement in snakeElements)
                {
                    if (!snakeElement.IsHead)
                    {
                        if (snakeElement.X == snakeHead.X && snakeElement.Y == snakeHead.Y)
                        {
                            GameOver();
                            break;
                        }
                    }
                }
            }
        }

        private async void GameOver()
        {
            gameLoopTimer.Stop();
            String nickname = File.ReadAllText("Data/nickname.txt");
            List<String> users = new List<string>();
            users = File.ReadLines("Data/users.txt").ToList();
            for (int i = 0; i < users.Count; i++)
            {
                var words1 = users[i].Split(" ");
                var words2 = (nickname + " " + score.ToString() + " 0").Split(" ");
                if (words1.Count() == words2.Count() && words1[0] == words2[0])
                {
                    if (score > int.Parse(words1[words1.Count() - 1])) users[i] = nickname + " " + score.ToString() + " " + words1.Last();
                    break;
                }
            }
            File.WriteAllLines("Data/users.txt", users);
            GameOver over = new GameOver(score);
            over.Show();
            await Task.Delay(200);
            Close();
        }


        private void CheckColllisonWithWolrldBounds()
        {
            SnakeElement snakeHead = snakeElements.Find(item => item.IsHead == true);
            if (snakeHead.X > gameWidth - elementSize || snakeHead.X < 0 || snakeHead.Y < 0 || snakeHead.Y > gameHeight - elementSize)
            {
                MessageBox.Show("Game Over");
            }
        }

        private void MoveSnake()
        {
            SnakeElement head = snakeElements.First();
            SnakeElement tail = snakeElements.Last();
            tailBackup = new SnakeElement(elementSize, color)
            {
                X = tail.X,
                Y = tail.Y
            };
            tail.SetCords(head.X, head.Y);
            tail.ToHead();
            head.ToBody();
            switch (currentDirection)
            {
                case Direction.Right:
                    if (tail.X > gameWidth - 2 * elementSize) tail.X = 0;
                    else tail.X += elementSize;
                    break;
                case Direction.Left:
                    if (tail.X < elementSize) tail.X = gameWidth - elementSize;
                    else tail.X -= elementSize;
                    break;
                case Direction.Up:
                    if (tail.Y < elementSize) tail.Y = gameHeight - elementSize;
                    else tail.Y -= elementSize;
                    break;
                case Direction.Down:
                    if (tail.Y > gameHeight - 2 * elementSize) tail.Y = 0;
                    else tail.Y += elementSize;
                    break;
                default:
                    break;
            }
            tail.Rotate(currentDirection.ToString());
            snakeElements.RemoveAt(snakeElements.Count - 1);
            snakeElements.Insert(0, tail);
            snakeElements.Last().ToTail();
            snakeElements.Last().shape.RenderTransform = snakeElements[snakeElements.Count - 2].shape.RenderTransform;
        }

        private void KeyWasReleased(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                    break;
                case Key.A:
                case Key.Left:
                    if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                    break;
                case Key.S:
                case Key.Down:
                    if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                    break;
                case Key.D:
                case Key.Right:
                    if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                    break;
                case Key.Escape:
                    PauseOrContinueGame();
                    break;
            }
        }

        private void PauseOrContinueGame()
        {
            if (!gamePaused)
            {
                gamePaused = true;
                gameLoopTimer.Stop();
                GameWorld.Children.Add(whole_black);
                Canvas.SetZIndex(PauseMenu, 100);
                text_score.Text = "Score: " + score.ToString();
                PauseMenu.Visibility = Visibility.Visible;
            }
            else
            {
                gamePaused = false;
                gameLoopTimer.Start();
                PauseMenu.Visibility = Visibility.Collapsed;
                GameWorld.Children.Remove(whole_black);
            }
        }

        enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        private async void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            await Task.Delay(200);
            Close();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            PauseOrContinueGame();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
