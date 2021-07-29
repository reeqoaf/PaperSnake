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

namespace snake_uc
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        List<List<string>> Leaders;
        public Leaderboard()
        {
            //Height = (int)Board.Height;
            //Width = (int)Board.Width;
            Leaders = new List<List<string>>();
            ParseUsers();
            SortByOriginal();
            InitializeComponent();
            drawBoard();

        }
        private void drawBoard()
        {
            if(Board_Stack != null) Board_Stack.Children.Clear();
            Thickness margin;
            Grid leader = new Grid()
            {
                Height = 80
            };
            margin.Left = 0;
            margin.Right = 1500;
            leader.Children.Add(new TextBlock()
            {
                Margin = margin,
                Width = 100,
                FontFamily = new FontFamily("Arial Rounded MT"),
                Foreground = Brushes.Black,
                FontSize = 60,
                FontStretch = FontStretches.Normal,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Text = "#",
                TextAlignment = TextAlignment.Center
            });
            margin.Left = 105;
            margin.Right = 611;
            leader.Children.Add(new TextBlock()
            {
                Margin = margin,
                Width = 884,
                FontFamily = new FontFamily("Arial Rounded MT"),
                Foreground = Brushes.Black,
                FontSize = 60,
                FontStretch = FontStretches.Normal,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Text = "Name",
                TextAlignment = TextAlignment.Center
            });
            margin.Left = 994;
            margin.Right = 330;
            leader.Children.Add(new TextBlock()
            {
                Margin = margin,
                Width = 300,
                FontFamily = new FontFamily("Arial Rounded MT"),
                Foreground = Brushes.Black,
                FontSize = 60,
                FontStretch = FontStretches.Normal,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Text = "Original",
                TextAlignment = TextAlignment.Center
            });
            margin.Left = 1297;
            margin.Right = 0;
            leader.Children.Add(new TextBlock()
            {
                Margin = margin,
                Width = 300,
                FontFamily = new FontFamily("Arial Rounded MT"),
                Foreground = Brushes.Black,
                FontSize = 60,
                FontStretch = FontStretches.Normal,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Text = "Extreme",
                TextAlignment = TextAlignment.Center
            });
            Board_Stack.Children.Add(leader);
            for(int i = 0; i < 10; i++)
            {
                leader = new Grid()
                {
                    Height = 80
                };
                margin.Left = 0;
                margin.Right = 1500;
                leader.Children.Add(new TextBlock()
                {
                    Margin = margin,
                    Width = 100,
                    FontFamily = new FontFamily("Arial Rounded MT"),
                    Foreground = Brushes.Black,
                    FontSize = 60,
                    FontStretch = FontStretches.Normal,
                    FontStyle = FontStyles.Normal,
                    FontWeight = FontWeights.Normal,
                    Text = (i + 1).ToString(),
                    TextAlignment = TextAlignment.Center
                });
                margin.Left = 105;
                margin.Right = 611;
                leader.Children.Add(new TextBlock()
                {
                    Margin = margin,
                    Width = 884,
                    FontFamily = new FontFamily("Arial Rounded MT"),
                    Foreground = Brushes.Black,
                    FontSize = 60,
                    FontStretch = FontStretches.Normal,
                    FontStyle = FontStyles.Normal,
                    FontWeight = FontWeights.Normal,
                    Text = i >= Leaders.Count ? "-" : Leaders[i][0],
                    TextAlignment = TextAlignment.Center
                });
                margin.Left = 994;
                margin.Right = 330;
                leader.Children.Add(new TextBlock()
                {
                    Margin = margin,
                    Width = 300,
                    FontFamily = new FontFamily("Arial Rounded MT"),
                    Foreground = Brushes.Black,
                    FontSize = 60,
                    FontStretch = FontStretches.Normal,
                    FontStyle = FontStyles.Normal,
                    FontWeight = FontWeights.Normal,
                    Text = i >= Leaders.Count ? "-" : Leaders[i][2],
                    TextAlignment = TextAlignment.Center
                }); 
                margin.Left = 1297;
                margin.Right = 0;
                leader.Children.Add(new TextBlock()
                {
                    Margin = margin,
                    Width = 300,
                    FontFamily = new FontFamily("Arial Rounded MT"),
                    Foreground = Brushes.Black,
                    FontSize = 60,
                    FontStretch = FontStretches.Normal,
                    FontStyle = FontStyles.Normal,
                    FontWeight = FontWeights.Normal,
                    Text = i >= Leaders.Count ? "-" : Leaders[i][1],
                    TextAlignment = TextAlignment.Center
                });
                Board_Stack.Children.Add(leader);
            }
        }
        private void ParseUsers()
        {
            List<string> users = new List<string>();
            users = File.ReadLines("Data/users.txt").ToList();
            foreach (var user in users)
            {
                Leaders.Add(ParseUser(user));
            }

        }
        private List<string> ParseUser(string user)
        {
            List<string> leader = new List<string>();
            var temp = user.Split(" ").ToList();
            String Score1 = temp.Last();
            temp.RemoveAt(temp.Count - 1);
            String Score2 = temp.Last();
            temp.RemoveAt(temp.Count - 1);
            return new List<string>() { String.Join(" ", temp.ToArray()), Score1, Score2};
        }

        private void SortByOriginal()
        {
            for(int i = 0; i < Leaders.Count; i++)
            {
                for (int j = 0; j < Leaders.Count - 1; j++)
                {
                    if(int.Parse(Leaders[j][2]) < int.Parse(Leaders[j + 1][2]))
                    {
                        var temp = Leaders[j];
                        Leaders[j] = Leaders[j + 1];
                        Leaders[j + 1] = temp;
                    }
                }
            }
        }
        private void SortByExtreme()
        {
            for (int i = 0; i < Leaders.Count; i++)
            {
                for (int j = 0; j < Leaders.Count - 1; j++)
                {
                    if (int.Parse(Leaders[j][1]) < int.Parse(Leaders[j + 1][1]))
                    {
                        var temp = Leaders[j];
                        Leaders[j] = Leaders[j + 1];
                        Leaders[j + 1] = temp;
                    }
                }
            }
        }

        private void OriginalSort_Click(object sender, RoutedEventArgs e)
        {
            SortByOriginal();
            drawBoard();
        }

        private void ExtremeSort_Click(object sender, RoutedEventArgs e)
        {
            SortByExtreme();
            drawBoard();
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            await Task.Delay(200);
            Close();
        }
    }
}
