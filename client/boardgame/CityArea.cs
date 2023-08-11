using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    class cityArea
    {
        public Rectangle[] cities
        {
            get;
            set;

        }
        public List<Rectangle>[] cityRect
        {
            get;
            set;

        }

        public Rectangle[] Rect_Ground
        {
            get;
            set;
        }
        public Rectangle[] Rect_Villa
        {
            get;
            set;
        }

        public Rectangle[] Rect_Building
        {
            get;
            set;
        }
        public Rectangle[] Rect_Hotel
        {
            get;
            set;
        }



        public List<Player>[] play
        {
            get;
            set;

        }


        
        Rectangle areacolor;
        Graphics DC;
        Bitmap bt;
       // bool buy;
        List<Bitmap> pl, pl2,thispl;
        int count;
        Graphics GP;

        Graphics namegp;
        //Bitmap namebt;
        public static int down = (1016);//    1016
        public static int side = (993);//     993
        Bitmap img2;
        Bitmap img;
        Bitmap img3;
        List<string> name = new List<string>();
        public List<string> name_str = new List<string>();

        public List<double>[] price = new List<double>[8];
        int Area;
        
        public cityArea(int Area,int count, Graphics DC, Bitmap bt, int x, int y, int num, int margin, List<Bitmap> pl, List<Bitmap> pl2, Graphics GP, Bitmap img,Bitmap img2,Graphics name,List<Bitmap> thispl)
        {
 

            int x1 = x;
            int y1 = y;

            cities = new Rectangle[count];
            cityRect = new List<Rectangle>[count];
            play = new List<Player>[count];
            Rect_Building = new Rectangle[count];
            Rect_Ground = new Rectangle[count];
            Rect_Hotel = new Rectangle[count];
            Rect_Villa = new Rectangle[count];
            
            for (int i = 0; i < count; i++)
            {
                if(i == 9)
                {
                    if (num == 0)
                        cities[i] = new Rectangle(x - 75, y, 150, 150);
                    else if (num == 1)
                        cities[i] = new Rectangle(x, y - 75, 150, 150);
                    else if(num == 2)
                        cities[i] = new Rectangle(x, y, 150, 150);
                    else if(num == 3)
                        cities[i] = new Rectangle(x, y, 150, 150);

                }
                else if (num == 0) //citys1
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
                else if( num == 2)
                {
                    cities[i] = City.cityWidth(x, y);
                    x += margin;
                }
                else if( num == 3)
                {
                    cities[i] = City.cityHeight(x, y);

                    y += margin;
                }
                //else if(num == 2)//four
                //{
                //    if (i == 1) {  x = 0; } 
                //    else if (i == 2) { x = 0; y = 0; }
                //    else if (i == 3) { x = 825; }
                //    cities[i] = City.four(x, y); // x,y x,0 0,0  y,0
                    
                //}
                cityRect[i] = new List<Rectangle>();
                play[i] = new List<Player>();
            }

            for (int i = 0; i <= 7; i++)
                price[i] = new List<double>();
           

            //name[0] = new List<string>[7];
            this.DC = DC;
            this.bt = bt;
            this.pl = pl;
            this.pl2 = pl2;
            this.count = count;
            this.GP = GP;
            this.img = img;
            this.img2 = img2;
            this.namegp = name;
            this.Area = Area;
            this.thispl = thispl;
            //this.namebt = namebt;

        }
        //public List<Player>[] Play2
        //{
        //    bool a;

        //}
        //areacolor = new Rectangle(x, y, 75, 25);
        //DC.FillRectangle(new SolidBrush(Color.Red), areacolor);
        //DC.DrawRectangle(Pens.Black, areacolor);

        //areacolor = new Rectangle(x, y, 25, 75);
        //DC.FillRectangle(new SolidBrush(Color.Gray), areacolor);
        //DC.DrawRectangle(Pens.Black, areacolor);
        public void drawname(int num)
        {
            buildname();

            for (int i = 0; i < cities.Count(); i++)
            {
                stringrotate(num, i);
            }
        }

        public void name_input(List<string> name_i)
        {
            StreamReader sr = new StreamReader(new FileStream("build.txt", FileMode.Open));
            while (sr.EndOfStream == false)
            {
                string[] name1 = sr.ReadLine().Split(new char[] { ',' });
                name_i.Add(name1[0]); // 7 7 8 7
            }
            sr.Close();
        }
        private void buildname() //이름과 가격
        {

            StreamReader sr = new StreamReader(new FileStream("build.txt", FileMode.Open));
            int countarea = 0;
           
            while (sr.EndOfStream == false)
            {
                string[] name1 = sr.ReadLine().Split(new char[] { ',' });
                name.Add(name1[0]); // 7 7 8 7

                if (Area == 0)
                {
                    if (countarea > 8)
                    {
                        countarea++;
                        continue;
                    }
                    else
                    {
                        if (countarea == 1 || countarea == 6)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                price[i].Add(0);
                            }
                            name_str.Add(null);
                            countarea++;
                        }
                        if (name1.Count() > 4) {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[1].Add(Int32.Parse(name1[4]));
                            price[2].Add(Int32.Parse(name1[3]));
                            price[3].Add(Int32.Parse(name1[2]));
                            price[4].Add(Double.Parse(name1[5]));
                            price[5].Add(Int32.Parse(name1[6]));
                            price[6].Add(Int32.Parse(name1[7]));
                            price[7].Add(Int32.Parse(name1[8]));
                            name_str.Add(name1[0]);
                            countarea++;
                        }
                        else
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[4].Add(Int32.Parse(name1[2]));
                            name_str.Add(name1[0]);
                            for (int i = 1; i < 8; i++)
                            {
                                if (i == 4)
                                    continue;
                                price[i].Add(0);
                            }
                            countarea++;
                        }

                    }

                }
                
                else if(Area == 1)
                {
                    if (countarea <= 6 || countarea > 15)
                    {
                        countarea++;
                        continue;
                    }
                    else
                    {

                        if (countarea == 8 || countarea == 13)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                price[i].Add(0);
                            }
                            name_str.Add(null);
                            countarea++;
                        }
                        if (name1.Count() > 4)
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[1].Add(Int32.Parse(name1[4]));
                            price[2].Add(Int32.Parse(name1[3]));
                            price[3].Add(Int32.Parse(name1[2]));
                            price[4].Add(Double.Parse(name1[5]));
                            price[5].Add(Int32.Parse(name1[6]));
                            price[6].Add(Int32.Parse(name1[7]));
                            price[7].Add(Int32.Parse(name1[8]));
                            name_str.Add(name1[0]);
                            countarea++;
                        }
                        else
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[4].Add(Int32.Parse(name1[2]));
                            for (int i = 1; i < 8; i++)
                            {
                                if (i == 4)
                                    continue;
                                price[i].Add(0);
                            }
                            name_str.Add(name1[0]);
                            countarea++;
                        }

                    }
           
                }
                else if (Area == 2)
                {
                    if (countarea <= 13 || countarea > 22)
                    {
                        countarea++;
                        continue;
                    }
                    else
                    {
                        if (countarea == 15)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                price[i].Add(0);
                            }
                            name_str.Add(null);
                            countarea++;
                        }
                        if (name1.Count() > 4)
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[1].Add(Int32.Parse(name1[4]));
                            price[2].Add(Int32.Parse(name1[3]));
                            price[3].Add(Int32.Parse(name1[2]));
                            price[4].Add(Double.Parse(name1[5]));
                            price[5].Add(Int32.Parse(name1[6]));
                            price[6].Add(Int32.Parse(name1[7]));
                            price[7].Add(Int32.Parse(name1[8]));
                            name_str.Add(name1[0]);
                            countarea++;
                        }
                        else
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[4].Add(Int32.Parse(name1[2]));
                            for (int i = 1; i < 8; i++)
                            {
                                if (i == 4)
                                    continue;
                                price[i].Add(0);
                            }
                            name_str.Add(name1[0]);
                            countarea++;

                        }

                    }

                }
                else if (Area == 3)
                {
                    if (countarea <= 21)
                    {
                        countarea++;
                        continue;
                    }
                    else
                    {
                        if (countarea == 26 || countarea == 29)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                price[i].Add(0);
                            }
                            name_str.Add(null);
                            countarea++;
                        }
                        if (name1.Count() > 4)
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[1].Add(Int32.Parse(name1[4]));
                            price[2].Add(Int32.Parse(name1[3]));
                            price[3].Add(Int32.Parse(name1[2]));
                            price[4].Add(Double.Parse(name1[5]));
                            price[5].Add(Int32.Parse(name1[6]));
                            price[6].Add(Int32.Parse(name1[7]));
                            price[7].Add(Int32.Parse(name1[8]));
                            name_str.Add(name1[0]);
                            countarea++;
                        }
                        else
                        {
                            price[0].Add(Int32.Parse(name1[1]));
                            price[4].Add(Int32.Parse(name1[2]));
                            for (int i = 1; i < 8; i++)
                            {
                                if (i == 4)
                                    continue;
                                price[i].Add(0);
                            }
                            name_str.Add(name1[0]);
                            countarea++;
                        }

                    }

                }
                
            }
            sr.Close();
        }
        int namecount;
        //private void rotate(Graphics gp, int angle, string txt, int x, int y, Font font, Brush brush) 
        //{
        //    GraphicsState state = gp.Save();
        //    gp.ResetTransform();
        //    gp.RotateTransform(angle);
        //    gp.TranslateTransform(x, y, MatrixOrder.Append);
        //    gp.DrawString(txt, font, brush, 0, 0);
        //    gp.Restore(state);
        //}


        private void stringrotate(int num, int i)
        {
            Font font = new Font("Bold", 10);
            GraphicsState state = namegp.Save();
            namegp.ResetTransform();

            if(num == 0)
            {
                if (i == 1)
                    return;
                else if (i == 6)
                    return;
                else if (i == 9)
                    return;
                namegp.DrawString(name[namecount], font, new SolidBrush(Color.Black), cities[i].X+3, cities[i].Y+130);
                namecount++;
            }
            if (num == 1)
                {
                int x = cities[i].X;
                int y = cities[i].Y;
                if (i == 1)
                    return;
               
                else if (i == 6)
                    return;
                else if (i == 9)
                    return;

                namegp.RotateTransform(90);

                if (i == 4)
                {
                    string[] konkod = name[namecount].Split(new char[] { ' ' });
                   namegp.TranslateTransform(x + 20, y + 3, MatrixOrder.Append);
                    
                   namegp.DrawString(konkod[1], font, new SolidBrush(Color.Black), 0, 0);
                   namegp.DrawString(konkod[0], font, new SolidBrush(Color.Black), 0, -20);
                }
                else
                {
                    namegp.TranslateTransform(x + 20, y + 3, MatrixOrder.Append);

                    namegp.DrawString(name[namecount], font, new SolidBrush(Color.Black), 0, 0);
                    //rotate(namegp, 90, name[namecount], x + 20, y + 3, font, new SolidBrush(Color.Black));
                }
                
                // Point point = new Point(-sz.Width, -sz.Height);//
                //TextRenderer.DrawText(namegp,name[namecount], font, new Rectangle(new Point(x, y), new Size(40, 75)), Color.Black, TextFormatFlags.WordBreak);
                namecount++;
     
                }
            if(num == 2)
            {
                if (namecount < 14)
                    namecount = 14;

            

                if (i == 0 )
                {
                    string[] lue = name[namecount].Split(new char[] { ' ' });
                    namegp.DrawString(lue[0], font, new SolidBrush(Color.Black), cities[i].X, cities[i].Y);
                    namegp.DrawString(lue[1], font, new SolidBrush(Color.Black), cities[i].X, cities[i].Y + 15);
                }
                else if (i == 1)
                    return;
                else if(i == 7)
                {
                    string[] queen = name[namecount].Split(new char[] { ' ' });
                    namegp.DrawString(queen[0], font, new SolidBrush(Color.Black), cities[i].X+30, cities[i].Y);
                    namegp.DrawString(queen[1], font, new SolidBrush(Color.Black), cities[i].X, cities[i].Y+15);
                    namegp.DrawString(queen[2], font, new SolidBrush(Color.Black), cities[i].X+30, cities[i].Y+28);
                }
                else if (i == 9)
                    return;
                else
                    namegp.DrawString(name[namecount], font, new SolidBrush(Color.Black), cities[i].X, cities[i].Y);




                //TextRenderer.DrawText(namegp, name[namecount], font, new Point(cities[i].X, cities[i].Y), Color.Black);
                namecount++;
            }
            if (num == 3)
            {
                if (namecount < 22)
                    namecount = 21;
              
                if (i == 5)
                    return;
                else if (i == 8)
                    return;
                else if (i == 10)
                    return;
             
                int x = cities[i].X;
                int y = cities[i].Y;

                namegp.RotateTransform(270);

                if (i == 2)
                {
                    string[] kolom = name[namecount].Split(new char[] { ' ' });
                    namegp.TranslateTransform(x + 117, y, MatrixOrder.Append);
                    namegp.DrawString(kolom[0], font, new SolidBrush(Color.Black), 0, 0);
                    namegp.DrawString(kolom[1], font, new SolidBrush(Color.Black), 20, 17);
                }
                else
                {
                    namegp.TranslateTransform(x + 130, y, MatrixOrder.Append);
                    namegp.DrawString(name[namecount], font, new SolidBrush(Color.Black), 0, 0);
                }
                    namecount++;
                
            }
            namegp.Restore(state);

        }
        public void drawcity(Color color, int num)
        {
            

            
            int x, y;

            Image img1 = Properties.Resources._3;
            img3 = new Bitmap(img1);
      
            for (int i = 0; i < cities.Count(); i++)
            {
               namegp.DrawRectangle(Pens.Black, cities[i]);
                if(num == 0)
                {
                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150, 150);
                        //DC.FillRectangle(new SolidBrush(Color.Salmon), areacolor);
                        //DC.DrawRectangle(Pens.Black, areacolor);
                        Image img4 = Properties.Resources._2;
                        img3 = new Bitmap(img4);
                        DC.DrawImage(img3, areacolor);
                    }
                    else if(i == 1 || i == 6)
                    {
                        DC.DrawImage(img, cities[i]);
                    }
                    else
                    {
                         //x = ;
                         //y = ;
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 75, 25);//75 100
                        GP.FillRectangle(new SolidBrush(color), areacolor);
                        GP.DrawRectangle(Pens.Black, areacolor);
              
                        //DC.DrawString(name[namecount], new Font("Bold", 10), new SolidBrush(Color.Black), x, y+ 115);
                        //namecount++;
                    }
                   
                }
                else if(num == 1)
                {
                    if (namecount < 7)
                        namecount = 7;

                    
                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150,150);
                        //DC.FillRectangle(new SolidBrush(Color.SkyBlue), areacolor);
                        //DC.DrawRectangle(Pens.Black, areacolor);
                        DC.DrawImage(img3, areacolor);
                    }
                    else if (i == 1 || i == 6)
                    {
                        DC.DrawImage(img, cities[i]);
                    }
                    else
                    {
                        areacolor = new Rectangle(125, cities[i].Y, 25, 75);
                        GP.FillRectangle(new SolidBrush(color), areacolor);
                        GP.DrawRectangle(Pens.Black, areacolor);
                        //stringrotate(num, i);
                    }
                }
                else if(num == 2)
                {
                    if (namecount < 14)
                        namecount = 14;



                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150, 150);
                        //DC.FillRectangle(new SolidBrush(Color.DarkSlateGray), areacolor);
                        //DC.DrawRectangle(Pens.Black, areacolor);
                        Image img4 = Properties.Resources._4;
                        img3 = new Bitmap(img4);
                        DC.DrawImage(img3, areacolor);
                    }
                    else if (i == 1)
                    {
                        DC.DrawImage(img, cities[i]);
                    }
                    else
                    {
                        areacolor = new Rectangle(cities[i].X, 125, 75, 25);
                        GP.FillRectangle(new SolidBrush(Color.Brown), areacolor);
                        GP.DrawRectangle(Pens.Black, areacolor);


                        //x = cities[i].X;
                        //y = cities[i].Y;
                        //DC.DrawString(name[namecount], new Font("Bold", 10), new SolidBrush(Color.Black),x ,y );
                        //namecount++;
                    }
                }
                else if(num == 3)
                {
                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150, 150);
                        DC.FillRectangle(new SolidBrush(Color.Goldenrod), areacolor);
                        DC.DrawRectangle(Pens.Black, areacolor);
                    }       
                    else if (i == 4 ) 
                        DC.DrawImage(img, cities[i]);
                    else if (i == 7)
                        DC.DrawImage(img2, cities[i]);
                    
                    else
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 25, 75);
                        GP.FillRectangle(new SolidBrush(color), areacolor);
                        GP.DrawRectangle(Pens.Black, areacolor);
                        
                    }
                   // stringrotate(num, i);
               
                }
            }
        }




        public void build(Color color, int num,Rectangle rect)
        {
            
                DC.DrawRectangle(Pens.Black, rect);
                DC.FillRectangle(new SolidBrush(color), rect);
            
            
        }









        public void drawfour()
        {
            DC.DrawRectangle(Pens.Black, cities[0]);
            DC.FillRectangle(new SolidBrush(Color.Goldenrod), cities[0]);

            DC.DrawRectangle(Pens.Black, cities[1]);
            DC.FillRectangle(new SolidBrush(Color.Salmon), cities[1]);

            DC.DrawRectangle(Pens.Black, cities[2]);
            DC.FillRectangle(new SolidBrush(Color.LightGreen), cities[2]);

            DC.DrawRectangle(Pens.Black, cities[3]);
            DC.FillRectangle(new SolidBrush(Color.LightBlue), cities[3]);
        }
        public void update(int nowblock)
        {
            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < play[i].Count; a++)
                {
                    Drawplayer(play[i][a], nowblock);

                }
            }
        }
        public void other_update(int nowblock, int num)
        {
            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < play[i].Count; a++)
                {
                    DrawPlayer(num, play[i][a], nowblock);

                }
            }
        }

        public void PlayerRemove(int bfblock, int bfcity)
        {
            //if (bfcity == 9) 
            //{ 
            //    if(bfblock == 0)
            //    {
            //        areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
            //        Image img1 = Properties.Resources._2;
            //        img3 = new Bitmap(img1);
            //        DC.DrawImage(img3, areacolor);
            //    }



            //}
            //else
            //{


            if (bfblock == 0) 
            {
                if (bfcity == 1 || bfcity == 6)
                {
                    DC.DrawImage(img, cities[bfcity]);
                }
                else if (bfcity == 9)
                {

                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
                    Image img4 = Properties.Resources._2;
                    img3 = new Bitmap(img4);
                    DC.DrawImage(img3, areacolor);

                }

                else if (cityRect[bfcity].Count > 0)
                    DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                else
                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);

            }
            else if(bfblock == 1) { 
                if (bfcity == 1 || bfcity == 6)
                {
                    DC.DrawImage(img, cities[bfcity]);
                }
                else if (bfcity == 9)
                {

                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
                    Image img4 = Properties.Resources._3;
                    img3 = new Bitmap(img4);
                    DC.DrawImage(img3, areacolor);

                }

                else if (cityRect[bfcity].Count > 0)
                    DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                else
                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
            }
            else if (bfblock == 2)
            {
                if (bfcity == 1)
                {
                    DC.DrawImage(img, cities[bfcity]);
                }
                else if (bfcity == 9)
                {

                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
                    Image img4 = Properties.Resources._4;
                    img3 = new Bitmap(img4);
                    DC.DrawImage(img3, areacolor);
                }
                else if (cityRect[bfcity].Count > 0)
                    DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                else
                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
            }
            else if (bfblock == 3)
            {
                if (bfcity == 4)
                {
                    DC.DrawImage(img, cities[bfcity]);
                }
                else if (bfcity == 7)
                {
                    DC.DrawImage(img2, cities[bfcity]);
                }
                else if(bfcity == 9)
                {


                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
                    DC.FillRectangle(new SolidBrush(Color.Goldenrod), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);
                }
                else if (cityRect[bfcity].Count > 0)
                    DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                else
                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
            }
            //if (cityRect[bfblock].Count != 0)
            //        DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
            //    else
            //        DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
            //}

        
        }

        public void updatecity(int nowblock, int nowcity, int count = 0)  //nowblock 이 구역 nowcity : 도시
        {
            if (count == 0)
            {
                Groundbuy(nowblock, nowcity, Color.Aquamarine);
            }

            //for (int i = 0; i < count; i++)
            //{
            //    for (int a = 1; a < cityRect[i].Count; a++)
            //    {'
           // if((nowblock == 0 &&(now)))
            if(count == 1)
                BuildDraw(Rect_Villa[nowcity]);
            if(count == 2)
                BuildDraw(Rect_Building[nowcity]);
            if (count == 3)
                BuildDraw(Rect_Hotel[nowcity]); 
            //    }
            //} 
        }

        public void updatebuild(int nowblock, int nowcity)
        {

            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < cityRect[i].Count; a++)
                {
                    BuildDraw(cityRect[i][a]);
                }
            }
        }

        public bool cacontain(Point pt)
        {
            int idx = -1;

            for (int i = 0; i < play.Length; i++)
            {
                for (int a = 0; a < play[i].Count; a++)
                {
                    if (play[i][a].nowpos.Contains(pt))
                    {
                        idx = i;
                        break;
                    }
                }
                if (idx != -1)
                    break;
            }
            if (idx == -1)
                return false;
            else
                return true;
        }
        public bool containcity(Point pt, int i)
        {
            if (cities[i].Contains(pt))
                return true;
            else return false;
        }
        public void Drawplayer(Player play, int city)
        {
            if(city % 2 == 0)
                DC.DrawImage(thispl[0], play.nowpos);
            else
                DC.DrawImage(thispl[1], play.nowpos);
        }

        public void DrawPlayer(int num, Player play, int city)
        {
            if (city % 2 == 0)
                DC.DrawImage(pl[num], play.nowpos);
            else
                DC.DrawImage(pl2[num], play.nowpos);
        }
     
        public void Groundbuy(int num, int city, Color color)
        {
            if (num == 0)
            {
                areacolor = new Rectangle(cities[city].X, cities[city].Y + 25, 75, 125);
                Rect_Ground[city] = areacolor;
            }

            else if (num == 1)
            {
                areacolor = new Rectangle(0, cities[city].Y, 125, 75);
                Rect_Ground[city] = areacolor;
            }
            else if (num == 2)
            {
                areacolor = new Rectangle(cities[city].X, cities[city].Y, 75, 125);
                Rect_Ground[city] = areacolor;
            }

            else
            {
                areacolor = new Rectangle(cities[city].X + 25, cities[city].Y, 125, 75);
                Rect_Ground[city] = areacolor;
            }
            cityRect[city].Add(areacolor);
            DC.FillRectangle(new SolidBrush(color), areacolor);
            //0 cities[i].X, cities[i].Y-25,100,75
            //1 0,cities[i].Y 100 75
            //2 cities[i].X, cities[i].Y,100,75
            //3 25,cities[i].Y 100 75 
            //Color.Aquamarine
        }
        public void GroundSell(int nowblock, int city)
        {
            Color color;
            Rectangle rect;
            if (nowblock == 0) {
                areacolor = new Rectangle(cities[city].X, cities[city].Y + 25, 75, 125);
                rect = new Rectangle(cities[city].X, cities[city].Y, 75, 25);

                color = Color.Red;
            }

            else if (nowblock == 1) {
                areacolor = new Rectangle(0, cities[city].Y, 125, 75);
                rect = new Rectangle(125, cities[city].Y, 25, 75);

                color = Color.DarkGreen;
            }
            else if (nowblock == 2) {
                areacolor = new Rectangle(cities[city].X, cities[city].Y, 75, 125);
                rect = new Rectangle(cities[city].X, 125, 75, 25);

                color = Color.Brown;
            }
            else {
                areacolor = new Rectangle(cities[city].X + 25, cities[city].Y, 125, 75);
                rect = new Rectangle(cities[city].X, cities[city].Y, 25, 75);

                color = Color.Gray;
            }
            DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), areacolor);
            //GP.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), areacolor);
            GP.FillRectangle(new SolidBrush(color), rect);
            //GP.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), rect);

            GP.DrawRectangle(Pens.Black, rect);
        }
        public void BuildDraw(Rectangle rect)
        {
            //GP.DrawRectangle(Pens.Black, rect);
            
            GP.FillRectangle(new SolidBrush(Color.Blue), rect);
            GP.DrawRectangle(Pens.Black, rect);
        }
    }
}