using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class GameMain
    {

        Label la;


        public bool b_building, b_villa, b_ground, b_hotel;

        public int savepos { get; set; } = 0;
        public int round = 0;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImageUnscaled(bit, 0, 0);
            e.Graphics.DrawImageUnscaled(key, 0, 0);
            
            
            e.Graphics.DrawImageUnscaled(nameBt, 0, 0);
            e.Graphics.DrawImageUnscaled(map, 0, 0);
            

        }

        private void init()
        {
            List<Bitmap> player_1 = new List<Bitmap>();
            List<Bitmap> player_2 = new List<Bitmap>();

            Image play = Properties.Resources.p1;
            player_1.Add(new Bitmap(play));
            Image play1 = Properties.Resources.p1_2;
            player_2.Add( new Bitmap(play1));

            Image p2 = Properties.Resources.p2;
            player_1.Add(new Bitmap(p2));
            Image p2_2 = Properties.Resources.p2_2;
            player_2.Add( new Bitmap(p2_2));

            Image p3 = Properties.Resources.p3;
            player_1.Add(new Bitmap(p3));
            Image p3_2 = Properties.Resources.p3_2;
            player_2.Add(new Bitmap(p3_2));

            Image p4 = Properties.Resources.p4;
            player_1.Add(new Bitmap(p4));
            Image p4_2 = Properties.Resources.p4_2;
            player_2.Add( new Bitmap(p4_2));

            List<Bitmap> thisplayer = new List<Bitmap>();
            if (!Multi)
            {
                thisplayer.Add(new Bitmap(play));
                thisplayer.Add(new Bitmap(play1));
            }
            else
            {
                if (BLUE)
                {
                    thisplayer.Add(new Bitmap(play));
                    thisplayer.Add(new Bitmap(play1));
                }
                if (BLACK)
                {
                    thisplayer.Add(new Bitmap(p2));
                    thisplayer.Add(new Bitmap(p2_2));
                }
                if (RED)
                {
                    thisplayer.Add(new Bitmap(p3));
                    thisplayer.Add(new Bitmap(p3_2));
                }
                if (GRAY)
                {
                    thisplayer.Add(new Bitmap(p4));
                    thisplayer.Add(new Bitmap(p4_2));
                }
            }
            Image cardspace = Properties.Resources.areacard;
            areacard = new Bitmap(cardspace);
            Image cardspace1 = Properties.Resources.areacard2;
            areacard1 = new Bitmap(cardspace1);
            Image cardspace2 = Properties.Resources.areacard3;
            areacard2 = new Bitmap(cardspace2);
            Image cardspace3 = Properties.Resources.areacard1;
            areacard3 = new Bitmap(cardspace3);
            Image img = Properties.Resources.사회복지기금;
            social = new Bitmap(img);
            
            city[0] = new cityArea(0,10, DC, bit, (City.s2.Height * 10), (City.s1.Width * 9 + City.fours.Width), 0, -City.s1.Width, player_1, player_2, Card, areacard, social, nameGp,thisplayer);
            city[1] = new cityArea(1,10, DC, bit, 0, (City.s2.Height * 10), 1, -City.s2.Height, player_1, player_2, Card, areacard1, social, nameGp, thisplayer);
            city[2] = new cityArea(2,10, DC, bit, City.s1.Width * 2, 0, 2, City.s1.Width, player_1, player_2, Card, areacard2, social, nameGp, thisplayer);
            city[3] = new cityArea(3,10, DC, bit, (City.s2.Height * 9 + City.fours.Height), City.s2.Height * 2, 3, City.s2.Height, player_1, player_2, Card, areacard3, social, nameGp, thisplayer);

            city[0].drawcity(Color.Red, 0);
            city[1].drawcity(Color.DarkGreen, 1);
            city[2].drawcity(Color.Brown, 2);
            city[3].drawcity(Color.Gray, 3);

            for (int i = 0; i < 4; i++)
            {
                city[i].drawname(i);
            }
        }

        private void reset()
        {

            city[bfblock].PlayerRemove(bfblock, bfcity);

        }

        private void rectcity() //건물들의 크기및 좌표 지정
        {
            for (int i = 0; i < 4; i++)
            {
                buildrect[i] = new Rectangle[9][];

                for (int l = 0; l < 9; l++)
                {
                    int n = 0;
                    buildrect[i][l] = new Rectangle[4];

                    for (int j = 0; j < 3; j++)
                    {

                        if (i == 0)
                        {

                            buildrect[i][l][j + 1] = City.citybuildA(city[i].cities[l].X + n, city[i].cities[l].Y);
                            n += 25;
                        }
                        else if (i == 2)
                        {
                            buildrect[i][l][j + 1] = City.citybuildA(city[i].cities[l].X + n, 125);
                            n += 25;
                        }
                        else if (i == 1)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(125, city[i].cities[l].Y + n);
                            n += 25;
                        }
                        else if (i == 3)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(city[i].cities[l].X, city[i].cities[l].Y + n);
                            n += 25;
                        }
                        //buildrect[i][l][0] = new Rectangle(new Point(1, 1), new Size(1, 0));
                    }
                }
            }
        }

        private void imagea() //플레이어의 말 크기와 좌표, 도시의 구역들의 좌표 지정.
        {
            for (int l = 0; l < 4; l++)
            {
                for (int i = 0; i < 10; i++)
                {
                    //if (l == 4)
                    //{
                    //    for (int a = 0; a < 4; a++)
                    //    {
                    //        players[l] = new List<Player>();
                    //    }
                    //}
                    //else
                    players[l] = new List<Player>();
                }

            }

            for (int i = 0; i < 4; i++)
            {
                playerrect[i] = new Rectangle[10];
                if (i == 0 || i == 2)
                {
                    for (int a = 0; a < 10; a++)
                    {

                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y + 30), new Size(75, 64));
                        playerrect[i][a] = pl;
                        Player player = new Player(playerrect, i, a);
                        players[i].Add(player);
                    }
                }
                else
                {
                    for (int a = 0; a < 10; a++)
                    {
                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X + 30, city[i].cities[a].Y), new Size(64, 75));
                        playerrect[i][a] = pl;
                        Player player = new Player(playerrect, i, a);
                        players[i].Add(player);

                    }
                }
            }
        }
        private void image() //주사위와 황금 카드들의 이미지 및 크기 지정.
        {
            const int Dice_X = 98;
            const int DICE_Y = 95;
            const int X_MARGIN = 10;
            const int Y_MARGIN = 4;
            Image dice2 = Properties.Resources.dice;
            dice1 = new Bitmap(dice2);
            Rectangle dice = new Rectangle(17, 13, 98, 95);
            Image card1 = Properties.Resources.keycard_3;
            cardg = new Bitmap(card1);
            Image cardred = Properties.Resources.keycard_6;
            card_red = new Bitmap(cardred);
            for (int y = 13; y <= DICE_Y * 2 + Y_MARGIN * 2 + 13; y += DICE_Y + Y_MARGIN)
            {
                for (int x = 17; x <= Dice_X + X_MARGIN + 17; x += Dice_X + X_MARGIN)
                {
                    Rectangle dice3 = new Rectangle(x, y, 98, 95);
                    Dicelist.Add(dice3);
                }
            }
            CardList.Add(new Rectangle(0, 0, 182, 393));//이미지에서의 키 카드 크기
            CardList.Add(new Rectangle(189, 0, 200, 280));//이미지에서의 황금열쇠 뒷면 크기

        }

        private void CardGraphic()
        {
            key = new Bitmap(this.Width, this.Height);
            Card = Graphics.FromImage(key);
        }

        private void initDC()
        {

            bit = new Bitmap(this.Width, this.Height);
            DC = Graphics.FromImage(bit);
            map = new Bitmap(this.Width, this.Height);
            dices = Graphics.FromImage(map);
            key = new Bitmap(this.Width, this.Height);
            Card = Graphics.FromImage(key);
            nameBt = new Bitmap(this.Width, this.Height);
            nameGp = Graphics.FromImage(nameBt);
    

        }

    }
}
