using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using Color = System.Drawing.Color;
//using Rectangle = System.Drawing.Rectangle;


namespace boardgame
{
    class Area
    {
        // public Point[] Card = new Point[] { new Point(200, 400), new Point(300, 500), new Point(500, 300), new Point(400, 200) };


        private int size = 1000;
        public const int count = 10;
        public const int area = 4;
        public Rectangle[][] cities { get; set; } = new Rectangle[4][];
        public Rectangle[] areacolor = new Rectangle[4];
        public Rectangle[] dice = new Rectangle[2];
        private int margin  = City.HEIGHT;
        private Point p = new Point(City.HEIGHT * 10, City.HEIGHT * 9 + City.FOURWH); //시작좌표

       // List<Graphics> DC;
       // Bitmap bt;

        public Area()//count 
        {
            setRect();

         //   this.DC = DC;
          //  this.bt = bt;
            //cities = new Rectangle[count];
        }

        public void setRect()
        {
            
            for (int num = 0; num < area; num++)
            {
                cities[num] = new Rectangle[10];
                if (num == 0) margin *= -1;
                if (num == 2) margin *= -1;
                //(City.HEIGHT * 10), (City.HEIGHT * 9 + City.FOURWH)
                //0, (City.HEIGHT * 10)
                // City.HEIGHT * 2, 0
                //(City.HEIGHT * 9 + City.fours.Height)
                for (int i = 0; i < count; i++)
                {
                    if (i == 9)
                    {
                        if (num == 0)
                        {
                            p.X += margin;
                            cities[num][9] = new Rectangle(p.X, p.Y, 150, 150);
                            p.Y += margin;

                        }
                        else if (num == 1)
                        {
                            p.Y += margin;
                            cities[num][9] = new Rectangle(p.X, p.Y, 150, 150);
                            p.X += City.FOURWH;

                        }
                        else if (num == 2)
                        {
                            cities[num][9] = new Rectangle(p.X, p.Y, 150, 150);
                            p.Y += City.FOURWH;

                        }
                        else if (num == 3)
                            cities[num][9] = new Rectangle(p.X, p.Y, 150, 150);


                    }
                    else if (num == 0) //num == 구역.
                    {
                        cities[num][i] = City.cityWidth(p.X, p.Y);
                        p.X += margin;
                    }
                    else if (num == 1) //citys2
                    {
                        cities[num][i] = City.cityHeight(p.X, p.Y);
                        p.Y += margin;
                    }
                    else if (num == 2)
                    {
                        cities[num][i] = City.cityWidth(p.X, p.Y); //가로
                        p.X += margin;
                    }
                    else if (num == 3)
                    {
                        cities[num][i] = City.cityHeight(p.X, p.Y); //세로
                        p.Y+= margin;
                    }
                }
            }
        }
    }
}
