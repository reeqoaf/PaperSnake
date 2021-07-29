using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace snake_uc.GameEntitites
{
    class GameEntity
    {
        public UIElement element { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ElementSize{ get; set; }
        public void SetCords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
