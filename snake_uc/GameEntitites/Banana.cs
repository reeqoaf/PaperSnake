using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace snake_uc.GameEntitites
{
    class Banana : Fruit
    {
        public Banana(int size)
        {
            Rectangle shape = new Rectangle();
            shape.Width = size;
            shape.Height = size;
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/banana.png"))
            };
            element = shape;
            score = 50;
        }
        public override bool Equals(object obj)
        {
            Banana banana = obj as Banana;
            if (banana != null)
            {
                return X == banana.X && Y == banana.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
