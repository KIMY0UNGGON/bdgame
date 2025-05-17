using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace boardgame
{
    class City
    {
        public const int FOURWH = 150;
        public const int HEIGHT = 75;
        public static Size fours = new Size(FOURWH, FOURWH);

        public static Size s1 = new Size(HEIGHT, FOURWH);
        public static Size s2 = new Size(FOURWH, HEIGHT);

  


        public static Rectangle cityWidth(int x, int y) //가로
        {
            return new Rectangle(x, y, HEIGHT, FOURWH);
        }


        public static Rectangle cityHeight(int x, int y) //세로
        {
            return new Rectangle(x, y, FOURWH,HEIGHT );
        }

        public static Rectangle citybuildA(int x, int y)
        {
            return new Rectangle(x, y, 25, 25);
        }
        public static Rectangle citybuildA(Rectangle r)
        {
            return new Rectangle(r.X, r.Y, 25, 25);
        }
        public static Rectangle citybuildB(int x,int y)
        {
            return new Rectangle(x, y, 25, 25);
        }
        public static Rectangle citybuildB(Rectangle r)
        {
            return new Rectangle(r.X,r.Y, 25, 25);
        }
        public static Rectangle four(int x, int y)
        {
            return new Rectangle(x, y, FOURWH, FOURWH);
        }
    }
}
