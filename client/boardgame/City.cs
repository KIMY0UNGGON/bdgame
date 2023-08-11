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
        public static Size fours = new Size(150, 150);

        public static Size s1 = new Size(75, 150);
        public static Size s2 = new Size(150, 75);
        public const int City_Four = 150;
        public const int City_H = 75;
  


        public static Rectangle cityWidth(int x, int y) //가로
        {
            return new Rectangle(x, y, City_H, City_Four);
        }


        public static Rectangle cityHeight(int x, int y) //세로
        {
            return new Rectangle(x, y, City_Four,City_H );
        }

        public static Rectangle citybuildA(int x, int y)
        {
            return new Rectangle(x, y, 25, 25);
        }
        public static Rectangle citybuildB(int x,int y)
        {
            return new Rectangle(x, y, 25, 25);
        }
        public static Rectangle four(int x, int y)
        {
            return new Rectangle(x, y, City_Four, City_Four);
        }

       
    }
}
