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

namespace snake_uc
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public GameOver(int score)
        {
            InitializeComponent();
            text_score.Text += score.ToString();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            Choose_level level = new Choose_level();
            level.Show();
            await Task.Delay(200);
            Close();
        }
        private async void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            await Task.Delay(200);
            Close();
        }
    }
}
