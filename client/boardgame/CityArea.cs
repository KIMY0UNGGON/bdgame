﻿using System;
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
        public Rectangle[] cities { get; set; }
        public List<Rectangle>[] cityRect { get; set; }

        public Rectangle[] Rect_Ground { get; set; }
        public Rectangle[] Rect_Villa { get; set; }

        public Rectangle[] Rect_Building { get; set; }
        public Rectangle[] Rect_Hotel { get; set; }


        public Dictionary<Player, int> Player_Num = new Dictionary<Player, int>(); //현재 있는 사각형 지역이 어떤 플레이어의 토큰인지 확인.
        public List<Player>[] play{  get;  set; }


        
        Rectangle areacolor;
        Graphics DC;
        Bitmap bt;
       // bool buy;
        List<Bitmap>[] Players_Token;
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
        int Client_num;

        Graphics Arch_GP;
        public cityArea(int Area, Graphics DC, Bitmap bt, int x, int y, int num, int margin, Graphics GP, Bitmap img,Bitmap img2,Graphics name,List<Bitmap>[] Players_Token,Graphics Arch_GP, int Client_num = 0)
        {

            int count = 10; //현재 구역의 칸 수.
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
                else if( num == 2)
                {
                    cities[i] = City.cityWidth(x, y); //가로
                    x += margin;
                }
                else if( num == 3)
                {
                    cities[i] = City.cityHeight(x, y); //세로

                    y += margin;
                }

                cityRect[i] = new List<Rectangle>();
                play[i] = new List<Player>();
            }

            for (int i = 0; i <= 7; i++)
                price[i] = new List<double>();
           

            this.DC = DC;
            this.bt = bt;
            this.count = count;
            this.GP = GP;
            this.img = img;
            this.img2 = img2;
            this.namegp = name;
            this.Area = Area;
            this.Players_Token = Players_Token;
            this.Client_num = Client_num;
            this.Arch_GP = Arch_GP;
        }

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
     
            string BUILD = Properties.Resources.build;
            foreach (var text in BUILD.Split('\n'))
            {
               
                name_i.Add(text); // 7 7 8 7
            }
        }
        private void buildname() //이름과 가격을 txt 파일에서 긁어와 저장.
        {

            string BUILD = Properties.Resources.build; //텍스트파일에는 [도시이름, 땅가격, 호텔, 빌딩, 빌라] 순으로 데이터가 저장되어있음
                                                                                            //price의 0~3번까지는 구매했을 때의 비용이, 5~8번까지는 땅에 도착했을때 지불해야하는 통행료를 저장.
            int countarea = 0;
           
            foreach (var text in BUILD.Split('\n'))
            {
                string[] name1 = text.Split(new char[] { ',' }); //name1은 txt파일에서 긁어온 내용을 ','에 따라 잘라서 저장한 배열. 
                name.Add(name1[0]); // 7 7 8 7

                if (Area == 0) //구역이 0일떄.
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
            
        }
        int namecount;



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

                }
                

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
            

            
            //int x, y;

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
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 75, 25);//75 100
                        DC.FillRectangle(new SolidBrush(color), areacolor);
                        DC.DrawRectangle(Pens.Black, areacolor);

                    }
                   
                }
                else if(num == 1)
                {
                    if (namecount < 7)
                        namecount = 7;

                    
                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150,150);

                        DC.DrawImage(img3, areacolor);
                    }
                    else if (i == 1 || i == 6)
                    {
                        DC.DrawImage(img, cities[i]);
                    }
                    else
                    {
                        areacolor = new Rectangle(125, cities[i].Y, 25, 75);
                        DC.FillRectangle(new SolidBrush(color), areacolor);
                        DC.DrawRectangle(Pens.Black, areacolor);
                    }
                }
                else if(num == 2)
                {
                    if (namecount < 14)
                        namecount = 14;



                    if (i == 9)
                    {
                        areacolor = new Rectangle(cities[i].X, cities[i].Y, 150, 150);
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
                        DC.FillRectangle(new SolidBrush(color), areacolor);
                        DC.DrawRectangle(Pens.Black, areacolor);
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
                        DC.FillRectangle(new SolidBrush(color), areacolor);
                        DC.DrawRectangle(Pens.Black, areacolor);
                        
                    }
               
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
        public void update(int nowblock) //모든 게임 판의 말들을 다시 그림.
        {
            Drawplayer(play[nowblock].Last());
        }
        public void update(int num, int nowblock) //특정 플레이어의 말을 그림.
        {
            DrawPlayer(num, play[nowblock].Last());
        }

        public void update(int nowblock, int nowcity,  Color color, int count = 0)  //nowblock 이 구역 nowcity : 도시
        {
            if (count == 0)
            {
                Groundbuy(nowblock, nowcity, color);
            }
            if (count == 1)
                BuildDraw(Rect_Villa[nowcity], color);
            if (count == 2)
                BuildDraw(Rect_Building[nowcity], color);
            if (count == 3)
                BuildDraw(Rect_Hotel[nowcity], color);
        }


        public void PlayerRemove(int bfblock, int bfcity)
        {
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
                else
                {

                
                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 75, 25);//75 100
                    DC.FillRectangle(new SolidBrush(Color.Red), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);
                }

                if (play[bfcity].Count > 0) //말이 이동한 뒤 그 전 도시에 다른 말들이 존재하는 지 확인
                {
                    foreach (var play_rect in play[bfcity])
                    {
                        DrawPlayer(Player_Num[play_rect], play_rect);
                    }
                    //DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                }
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
                else
                {

                        DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
                    areacolor = new Rectangle(125, cities[bfcity].Y, 25, 75);
                    DC.FillRectangle(new SolidBrush(Color.DarkGreen), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);
                }

                if (play[bfcity].Count > 0) //말이 이동한 뒤 그 전 도시에 다른 말들이 존재하는 지 확인
                {
                    foreach (var play_rect in play[bfcity])
                    {
                        DrawPlayer(Player_Num[play_rect], play_rect);
                    }
                    //DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                }
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
                else
                {


                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
                    areacolor = new Rectangle(cities[bfcity].X, 125, 75, 25);
                    DC.FillRectangle(new SolidBrush(Color.Brown), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);
                }

                if (play[bfcity].Count > 0) //말이 이동한 뒤 그 전 도시에 다른 말들이 존재하는 지 확인
                {
                    foreach (var play_rect in play[bfcity])
                    {
                        DrawPlayer(Player_Num[play_rect], play_rect);
                    }
                    //DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                }
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
                else if (bfcity == 9)
                {


                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 150, 150);
                    DC.FillRectangle(new SolidBrush(Color.Goldenrod), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);
                }
                else
                {


                    DC.FillRectangle(new SolidBrush(Color.DarkSeaGreen), cities[bfcity]);
                    areacolor = new Rectangle(cities[bfcity].X, cities[bfcity].Y, 25, 75);
                    DC.FillRectangle(new SolidBrush(Color.Gray), areacolor);
                    DC.DrawRectangle(Pens.Black, areacolor);

                }
                if (play[bfcity].Count > 0) //말이 이동한 뒤 그 전 도시에 다른 말들이 존재하는 지 확인
                {
                    foreach (var play_rect in play[bfcity])
                    {
                        DrawPlayer(Player_Num[play_rect], play_rect);
                    }
                    //DC.FillRectangle(new SolidBrush(Color.Aquamarine), cities[bfcity]);
                }

            }
        }



        //public void updatebuild(int nowblock, int nowcity)
        //{

        //    for (int i = 0; i < count; i++)
        //    {
        //        for (int a = 0; a < cityRect[i].Count; a++)
        //        {
        //            BuildDraw(cityRect[i][a]);
        //        }
        //    }
        //}

        public int Token_Click(Point pt)
        {
            int idx = -1;

            for (int i = 0; i < play.Length; i++)
            {
                for (int a = 0; a < play[i].Count; a++)
                {
                    if (play[i][a].nowpos.Contains(pt))
                    {
                        idx = Player_Num[play[i][a]];
                        break;
                    }
                }
                if (idx != -1)
                    break;
            }

            return idx;
        }
        public bool containcity(Point pt, int i)
        {
            if (cities[i].Contains(pt))
                return true;
            else return false;
        }
        
        public void Drawplayer(Player play)
        {
            if (Player_Num.ContainsKey(play) == false)
                Player_Num.Add(play, Client_num);
            if (Area % 2 == 0)
                DC.DrawImage(Players_Token[Client_num][0], play.nowpos);
            else
                DC.DrawImage(Players_Token[Client_num][1], play.nowpos);
        }

        public void DrawPlayer(int num, Player play) //num == 다른 클라이언트의 번호.
        {
            if (Player_Num.ContainsKey(play) == false)
                Player_Num.Add(play, num);
            if (Area % 2 == 0)
                DC.DrawImage(Players_Token[num][0], play.nowpos);
            else
                DC.DrawImage(Players_Token[num][1], play.nowpos);
        }
     
        public void Groundbuy(int num, int city, Color color) //땅구매
        {
            //if (num == 0)
            //{
            //    areacolor = new Rectangle(cities[city].X, cities[city].Y + 25, 75, 125);
            //    Rect_Ground[city] = areacolor;
            //}

            //else if (num == 1)
            //{
            //    areacolor = new Rectangle(0, cities[city].Y, 125, 75);
            //    Rect_Ground[city] = areacolor;
            //}
            //else if (num == 2)
            //{
            //    areacolor = new Rectangle(cities[city].X, cities[city].Y, 75, 125);
            //    Rect_Ground[city] = areacolor;
            //}

            //else
            //{
            //    areacolor = new Rectangle(cities[city].X + 25, cities[city].Y, 125, 75);
            //    Rect_Ground[city] = areacolor;
            //}
            cityRect[city].Add(Rect_Ground[city]);
            DC.FillRectangle(new SolidBrush(color), areacolor);
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
            GP.FillRectangle(new SolidBrush(color), rect);
            GP.DrawRectangle(Pens.Black, rect);
        }
        public void BuildDraw(Rectangle rect, Color color)
        {
            GP.FillRectangle(new SolidBrush(color), rect);
            GP.DrawRectangle(Pens.Black, rect);
        }
    }
}