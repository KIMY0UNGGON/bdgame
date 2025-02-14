using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardgame
{
    class Player //가로 세로. 사각형.
    {
        public static int Height_1 = 64;
        public static int Width_1 = 75;

        public Rectangle  nowpos;

        //public Rectangle nowpos;
        public Player(Rectangle[][] pos,int i, int a)
        {
            nowpos = pos[i][a];

        }

        public static Rectangle make1(int x, int y)
        {
            return new Rectangle(x, y, Player.Width_1, Player.Height_1);
        }
        public static Rectangle make2(int x, int y)
        {
            return new Rectangle(x, y, Player.Height_1, Player.Width_1);
        }
    }
}
