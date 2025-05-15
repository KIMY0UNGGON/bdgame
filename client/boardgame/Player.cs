using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace boardgame
{
    class Player //가로 세로. 사각형.
    {
        public static int Height_1 = 64;
        public static int Width_1 = 75;
        public int Number { get; set; } = -1;
        public (int block, int city) nowPos = (-1, -1);
        public Rectangle[][]  PosRectangle;
        public bool Visible = false;
        public Bitmap[] PlayerToken = new Bitmap[2];
        public void Activate(int x, int y)
        {
            Visible = true;
            nowPos = (x, y);
        }

        //public Rectangle nowpos;
        public Player(Rectangle[][] pos, Bitmap[] playerToken)
        {
            PosRectangle = pos;
            nowPos = (3, 9); //초기 좌표값 설정
            Visible = false;
            PlayerToken = playerToken;
        }
        public Rectangle NowRect()
        {
            return PosRectangle[nowPos.block][nowPos.city];
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
