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
    /// Interaction logic for Choose_level.xaml
    /// </summary>
    public partial class Choose_level : Window
    {
        public Choose_level()
        {
            InitializeComponent();
        }

        private async void Free_level_Click(object sender, RoutedEventArgs e)
        {
            Free_level free = new Free_level();
            free.Show();
            await Task.Delay(200);
            Close();
        }

        private async void Walls_level_Click(object sender, RoutedEventArgs e)
        {
            Walls_level walls = new Walls_level();
            walls.Show();
            await Task.Delay(200);
            Close();
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
