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
    class SnakeElement : GameEntity
    {
        public bool IsHead { get; set; }
        public bool IsTail { get; set; }

        private string color;
        public Rectangle shape;
        
        public SnakeElement(int size, string color)
        {
            shape = new Rectangle();
            shape.Width = size;
            shape.Height = size;
            shape.Fill = Brushes.Black;
            shape.Stroke = Brushes.LightBlue;
            shape.StrokeStartLineCap = PenLineCap.Triangle;
            element = shape;
            this.color = color;
            ElementSize = size;
        }
        public void ToHead()
        {
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/head_" + color + ".png"))
            };
            element = shape;
            IsHead = true;
            IsTail = false;
        }
        public void ToTail()
        {
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/tail_" + color + ".png"))
            };
            element = shape;
            IsHead = false;
            IsTail = true;
        }
        public void ToBody()
        {
            shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/snake_uc;component/Images/body_" + color + ".png"))
            };
            element = shape;
            IsHead = false;
            IsTail = false;
        }
        public void Rotate(string value)
        {
            switch(value)
            {
                case "Up":
                    shape.RenderTransform = new RotateTransform(0, ElementSize / 2, ElementSize / 2);
                    break;
                case "Right":
                    shape.RenderTransform = new RotateTransform(90, ElementSize / 2, ElementSize / 2);
                    break;
                case "Down":
                    shape.RenderTransform = new RotateTransform(180, ElementSize / 2, ElementSize / 2);
                    break;
                case "Left":
                    shape.RenderTransform = new RotateTransform(270, ElementSize / 2, ElementSize / 2);
                    break;
                default:
                    break;
            }
            
        }
    }
}
