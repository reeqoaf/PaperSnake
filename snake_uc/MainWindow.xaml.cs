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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace snake_uc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputTextBox.Text = File.ReadAllText("Data/nickname.txt");
            List<String> config = new List<string>();
            config.Add("color=0");
            if (!File.Exists("Data/config.txt"))
            {
                File.WriteAllLines("Data/config.txt", config);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("Data/nickname.txt", InputTextBox.Text);
            List<string> users = new List<string>();
            if (!File.Exists("Data/users.txt"))
            {
                users.Add(InputTextBox.Text + " 0 0");
                File.WriteAllLines("Data/users.txt", users);
                return;
            }
            users = File.ReadLines("Data/users.txt").ToList();
            foreach (var user in users)
            {
                var words1 = user.Split(" ");
                var words2 = (InputTextBox.Text + " 0 0").Split(" ");
                if (words1.Count() == words2.Count() && words1[0] == words2[0]) return;
            }
            users.Add(InputTextBox.Text + " 0 0");
            File.WriteAllLines("Data/users.txt", users);
        }

        private async void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            Leaderboard board = new Leaderboard();
            board.Show();
            await Task.Delay(200);
            Close();
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            Choose_level choose = new Choose_level();
            choose.Show();
            await Task.Delay(200);
            Close();
        }

        private async void Customize_Click(object sender, RoutedEventArgs e)
        {
            Customize cust = new Customize();
            cust.Show();
            await Task.Delay(200);
            Close();
        }
    }
}
