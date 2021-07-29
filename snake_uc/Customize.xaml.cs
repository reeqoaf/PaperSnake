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
    /// Interaction logic for Customize.xaml
    /// </summary>
    public partial class Customize : Window
    {
        public Customize()
        {
            InitializeComponent();
            ChangeChoosed(GetColor());
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            ChangeChoosed(0);
            EditConfig(0);
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            ChangeChoosed(1);
            EditConfig(1);
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            ChangeChoosed(2);
            EditConfig(2);
        }

        private void EditConfig(int color)
        {
            List<String> config = new List<string>();
            config.Add("color=" + color.ToString());
            File.WriteAllLines("Data/config.txt", config);
        }
        private int GetColor()
        {
            return int.Parse(File.ReadAllText("Data/config.txt").Split("=").Last());
        }
        private void ChangeChoosed(int color)
        {
            switch(GetColor())
            {
                case 0:
                    purple_choosed.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    green_choosed.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    yellow_choosed.Visibility = Visibility.Collapsed;
                    break;
            }
            switch(color)
            {
                case 0:
                    purple_choosed.Visibility = Visibility.Visible;
                    break;
                case 1:
                    green_choosed.Visibility = Visibility.Visible;
                    break;
                case 2:
                    yellow_choosed.Visibility = Visibility.Visible;
                    break;
            }
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
