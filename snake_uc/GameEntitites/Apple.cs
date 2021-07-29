using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace snake_uc.GameEntitites
{
    class Apple : Fruit
    {
        public Apple(int size)
        {
            Rectangle shape = new Rectangle();
            shape.Width = size;
            shape.Height = size;
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/apple.png"))
            };
            element = shape;
            score = 25;
        }
        public override bool Equals(object obj)
        {
            Apple apple = obj as Apple;
            if(apple != null)
            {
                return X == apple.X && Y == apple.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
