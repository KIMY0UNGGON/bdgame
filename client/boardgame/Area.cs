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
        public Point[] Card = new Point[] { new Point(200, 400), new Point(300, 500), new Point(500, 300), new Point(400, 200) };


        public int down = GameMain.ActiveForm.Size.Height;
        public int side = GameMain.ActiveForm.Size.Width;

        public Rectangle[][] cities = new Rectangle[4][];
        //public Rectangle[] fou = new Rectangle[4];
        public Rectangle[] areacolor = new Rectangle[4];
        public Rectangle[] dice = new Rectangle[2];


        Canvas canvas = new Canvas();


        Graphics DC;
        Bitmap bt;
        int down1 = (City.s1.Width * 9 + City.fours.Width);
        int side1 = (City.s2.Height * 9 + City.fours.Height);
        public Area(Graphics DC, Bitmap bt)//count 
        {
        
          
            this.DC = DC;
            this.bt = bt;
            //cities = new Rectangle[count];
        }
        private void left()
        {
            int y = City.s2.Height;
            cities[1] = new Rectangle[10];



            for (int i = cities[1].Length-1; i >= 0 ; i--)
            {
                 

                if (i == 0)
                {
                    //y += City.fours.Height;
                    cities[1][0] = City.four(0, down1);
                    DC.DrawRectangle(Pens.Black, cities[1][i]);
                    DC.FillRectangle(new SolidBrush(Color.Salmon), cities[1][i]);
                }
                else
                {
                   y += City.s2.Height;
                    cities[1][i] = City.cityHeight(0, y);
                    DC.DrawRectangle(Pens.Black, cities[1][i]);

                    if (i == 1 || i == 4 || i == 6)
                    {
                        continue;
                    }
                    else
                    {
                        areacolor[1] = new Rectangle(125, y, 25, 75);
                        DC.FillRectangle(new SolidBrush(Color.DarkGreen), areacolor[1]);
                        DC.DrawRectangle(Pens.Black, areacolor[1]);

                    }
                }
            }

        }
        private void top()
        {
            int x = City.s1.Width;
            cities[2] = new Rectangle[10];
            for (int i = 0; i < cities[2].Length; i++)
            {
                

                if (i == 0)
                {
                    //x += City.fours.Width;
                    cities[2][0] = City.four(0, 0);
                    DC.DrawRectangle(Pens.Black, cities[2][i]);
                    DC.FillRectangle(new SolidBrush(Color.LightGreen), cities[2][i]);
                }
                else
                {
                    x += City.s1.Width;
                    cities[2][i] = City.cityWidth(x, 0);
                    DC.DrawRectangle(Pens.Black, cities[2][i]);
                    // LCity[2] = new List<Rectangle>();
                    if (i == 1 || i == 4 || i == 7)
                    {
                        continue;
                    }
                    else
                    {
                        areacolor[2] = new Rectangle(x, 125, 75, 25);
                        DC.FillRectangle(new SolidBrush(Color.Brown), areacolor[2]);
                        DC.DrawRectangle(Pens.Black, areacolor[2]);

                    }
                }
            }

        }
        private void right()
        {
            int x = (City.s2.Height * 9 + City.fours.Height);
            int y = City.s2.Height;
            cities[3] = new Rectangle[10];
          
            for (int i = 0; i < cities[3].Length; i++)
            {
               
                
                if(i == 0 )
                {
                    //y += City.fours.Height;
                    cities[3][0] = City.four(side1,0);
                    DC.DrawRectangle(Pens.Black, cities[3][i]);
                    DC.FillRectangle(new SolidBrush(Color.LightBlue), cities[3][i]);
                }
                
                else {
                     y += City.s2.Height;
                    cities[3][i] = City.cityHeight(x, y);
                    DC.DrawRectangle(Pens.Black, cities[3][i]);
                    // LCity[3] = new List<Rectangle>();
                    if (i == 1 || i == 4 || i == 7)
                    {
                        continue;
                    }
                    else
                    {
                        areacolor[3] = new Rectangle(x, y, 25, 75);
                        DC.FillRectangle(new SolidBrush(Color.Gray), areacolor[3]);
                        DC.DrawRectangle(Pens.Black, areacolor[3]);

                    } 
                }
            }

        }
        
        private void bottom()
        {
            int x = City.s1.Width;
            int y = (City.s1.Width * 9 + City.fours.Width);
            cities[0] = new Rectangle[10];
            
            for (int i = cities[0].Length-1; i >= 0; i--)
            {
                

                if (i == 0)
                {
                   // x += City.fours.Width;
                    cities[0][0] = City.four(side1, down1);
                    DC.DrawRectangle(Pens.Black, cities[0][i]);
                    DC.FillRectangle(new SolidBrush(Color.Goldenrod), cities[0][i]);
                }
                else
                {
                    x += City.s1.Width;
                    cities[0][i] = City.cityWidth(x, y);


                    DC.DrawRectangle(Pens.Black, cities[0][i]);
                    //   LCity[0] = new List<Rectangle>();
                    if (i == 1 || i == 4 || i == 6)
                    {
                        continue;
                    }
                    else
                    {
                        areacolor[0] = new Rectangle(x, y, 75, 25);
                        DC.FillRectangle(new SolidBrush(Color.Red), areacolor[0]);
                        DC.DrawRectangle(Pens.Black, areacolor[0]);

                    }
                }
            }

       

        }
       
        public void draw()
        {

           
            left();
            top();
            bottom();
            right();
            System.Drawing.Image img = Properties.Resources.areacard;
            bt = new Bitmap(img);
            //0,1  0,6  1,1 1,4  2,4  2,6  3,1  3,7
            DC.DrawImage(bt, cities[0][1]);
            DC.DrawImage(bt, cities[0][6]);
            img = Properties.Resources.areacard2;
            bt = new Bitmap(img);
            DC.DrawImage(bt, cities[1][1]);
            DC.DrawImage(bt, cities[1][4]);
            img = Properties.Resources.areacard3;
            bt = new Bitmap(img);
            DC.DrawImage(bt, cities[2][4]);
            DC.DrawImage(bt, cities[2][7]);
            img = Properties.Resources.areacard1;
            bt = new Bitmap(img);
            DC.DrawImage(bt, cities[3][1]);
            DC.DrawImage(bt, cities[3][7]);
            // fourplace();
            dice[0] = new Rectangle(600, 700, 100, 100);
            dice[1] = new Rectangle(710, 700, 100, 100);
            DC.DrawRectangle(Pens.Black, dice[0]);
            DC.DrawRectangle(Pens.Black, dice[1]);

            //keyCard.StrokeThickness = 1;
            //pointCollection.Add(new System.Windows.Point(500, 300));
            //pointCollection.Add(new System.Windows.Point(400, 200));
            //pointCollection.Add(new System.Windows.Point(300, 500));
            //keyCard.Points = pointCollection;
            //keyCard.Stroke = System.Windows.Media.Brushes.Black;
            //keyCard.StrokeThickness = 1;

            //canvas.Children.Add(keyCard);
            //new Point(500, 300), new Point(400, 200), new Point(300, 500)
            DC.DrawPolygon(Pens.Black , Card);
        }

        public void Emptyrect()
        {
            DC.Clear(Color.Silver);
            draw();
        }
        
    }
}
