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
    class Grape : Fruit
    {
        public Grape(int size)
        {
            Rectangle shape = new Rectangle();
            shape.Width = size;
            shape.Height = size;
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/grape.png"))
            };
            element = shape;
            score = 100;
        }
        public override bool Equals(object obj)
        {
            Grape pinapple = obj as Grape;
            if (pinapple != null)
            {
                return X == pinapple.X && Y == pinapple.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
