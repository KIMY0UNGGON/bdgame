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

       public void test(Rectangle[] cities, int count, int margin, int num)
        {
            cities = new Rectangle[count];
            for (int i = 0; i < count; i++)
            {
                if (i == 9)
                {
                    if (num == 0)
                        cities[i] = new Rectangle(x - 75, y, 150, 150);
                    else if (num == 1)
                        cities[i] = new Rectangle(x, y - 75, 150, 150);
                    else if (num == 2)
                        cities[i] = new Rectangle(x, y, 150, 150);
                    else if (num == 3)
                        cities[i] = new Rectangle(x, y, 150, 150);

                }
                else if (num == 0) //num == 구역.
                {
                    cities[i] = City.cityWidth(x, y);
                    x += margin;
                    // Width = true;
                }
                else if (num == 1) //citys2
                {
                    cities[i] = City.cityHeight(x, y);

                    y += margin;
                    //Width = false;
                }
                else if (num == 2)
                {
                    cities[i] = City.cityWidth(x, y); //가로
                    x += margin;
                }
                else if (num == 3)
                {
                    cities[i] = City.cityHeight(x, y); //세로

                    y += margin;
                }

                cityRect[i] = new List<Rectangle>();
            }
        }
    }
}
