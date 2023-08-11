using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class GameMain
    {
        private double Toll_fee(int nowblock, int nowcity)
        {
            bool g_c=confirm_ground(nowblock, nowcity);
            bool v_c = confirm_Villa(nowblock, nowcity);
            bool b_c = confirm_Building(nowblock, nowcity);
            bool h_c = confirm_Hotel(nowblock, nowcity);
            double price_toll = 0;
            if (g_c)
                price_toll += city[nowblock].price[4][nowcity];
            if (v_c)
                price_toll += city[nowblock].price[5][nowcity];
            if (b_c)
                price_toll += city[nowblock].price[6][nowcity];
            if (h_c)
                price_toll += city[nowblock].price[7][nowcity];
            return price_toll;
            //city[nowblock].price[4~7][nowcity]
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random dice = new Random();

            int dicenum1 = dice.Next(1, 7);
            int dicenum2 = dice.Next(1, 7);
            button1.Text = dicenum1.ToString() + "|" + dicenum2.ToString();
            int dicenum = dicenum1 + dicenum2;
            int saveX = 0, saveY = 0;
            //int savepos = 0;

            Rectangle dicepos = new Rectangle(600, 700, 100, 100);
            Rectangle dicepos1 = new Rectangle(710, 700, 100, 100);
            Point x = dicepos.Location;
            dices.DrawImage(dice1, dicepos, Dicelist[dicenum1 - 1], GraphicsUnit.Pixel);
            dices.DrawImage(dice1, dicepos1, Dicelist[dicenum2 - 1], GraphicsUnit.Pixel);
            button1.Enabled = false;
            skip = 0;
            int a = 0;
            cardclick = true;
            if (nowcity == 9 && nowblock == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        if (i == 2 && l == 9)
                        {
                            continue;
                        }
                        buttonTravel.Add(new Button());
                        //butt[a] = new Button();
                        buttonTravel[a].Location = new System.Drawing.Point(city[i].cities[l].X, city[i].cities[l].Y);
                        buttonTravel[a].Name = "button1";
                        buttonTravel[a].Size = new System.Drawing.Size(75, 23);
                        buttonTravel[a].TabIndex = 0;
                        buttonTravel[a].Text = i + "|" + l;
                        buttonTravel[a].UseVisualStyleBackColor = true;
                        Controls.Add(buttonTravel[a]);
                        buttonTravel[a].Click += new System.EventHandler(this.butt_Click);
                        a++;
                    }
                }
                loopconfirm = 0;
            }
            else if (nowcity == 9 && nowblock == 0 && island < 3)
            {
                island++;
                Uninhabited = true;
            }
            else if (dicenum + nowcity > 9)
            {
                int movecity = dicenum + nowcity;
                move_write(dicenum);
                for (int i = bfcity; i <= movecity; i++) //말이 있는 칸부터 주사위 숫자를 더한 칸  
                {
                    if (i > 9) // 구역 넘어감
                    {
                        i = 0; movecity -= 10;
                        if (nowblock < 3) nowblock++;
                        else nowblock = 0;
                    }
                    //else if (i > 9)
                    //{
                    //    i = 0; movecity -= 10;
                    //    if (nowblock < 3) nowblock++;
                    //    else nowblock = 0;
                    //}
                    //if (nowblock == 4)
                    //{
                    //    int a = 0;
                    //    skip++;
                    //    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    //    reset();
                    //    if (bfblock < 3) a = bfblock + 1;
                    //    else a = 0;
                    //    city[nowblock].play[a].Add(players[nowblock][a]);

                    //    city[nowblock].update(nowblock);
                    //    Invalidate();
                    //    Delay(100);
                    //    if (bfblock < 3) nowblock = bfblock+1;
                    //    else nowblock = a;
                    //    bfcity = a;
                    //    bfblock = 4;
                    //    //if (!start)
                    //    //{ movecity--; dicenum--; }
                    //    //nowcity++;

                    //}
                    //else if(nowblock != 4)
                    //{
                    if (nowblock == 3 && i == 8) money.m += 20;//money += 20; // 월급

                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;

                    //}
                }
                //nowcity += dicenum;
                //if (nowcity > 9)
                //{
                //    nowcity -= 10;
                //    nowblock = bfblock;
                //}
                //if(nowcity == 10)
                //{
                //    nowcity = 0;
                //    //nowblock++;
                //    //if (nowblock > 3)
                //    //    nowblock = 0;
                //    nowblock = bfblock;
                //}
                nowblock = bfblock;
                nowcity = bfcity;
                //button3.Text = nowblock.ToString() + "|" + nowcity.ToString();
                if (island >= 3)
                {
                    island = 0;
                    Uninhabited = false;
                }
            }
            else
            {
                //if (nowblock == 1 || nowblock == 3)
                //    player = new Bitmap(Properties.Resources.p1_2);
                //else
                //    player = new Bitmap(Properties.Resources.p1);

                int fq = dicenum + nowcity;

                for (int i = bfcity; i <= fq; i++)
                {
                    if (i > 9)
                    {
                        i = 0; fq -= 10;
                        if (nowblock < 3) nowblock++;
                        else nowblock = 0;
                    }
                    //else if (i > 9)
                    //{
                    //    i =0;
                    //    fq -= 10;

                    //    if (nowblock < 3) nowblock++;
                    //    else nowblock = 0;

                    //}
                    //if (nowblock == 4)
                    //{
                    //    skip++;
                    //    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    //    reset();
                    //    int a = 0;
                    //    if (bfblock < 3) a = bfblock + 1;
                    //    else a = 0;
                    //    city[nowblock].play[a].Add(players[nowblock][a]);
                    //    city[nowblock].update(nowblock);
                    //    Invalidate();
                    //    Delay(100);
                    //    if (bfblock < 3) nowblock = bfblock + 1;
                    //    else nowblock = a;
                    //    bfcity = a;
                    //    bfblock = 4;

                    //}
                    //else if (nowblock != 4)
                    //{
                    skip++;

                    if (nowblock == 3 && i == 9) money.m += 20;//money += 20;

                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;
                    //}



                    savepos = i;
                    //saveX = city[nowblock].cities[i].X;
                    //saveY = city[nowblock].cities[i].Y;


                    Invalidate();
                    //if (i != fq - 1) { reset(); city[nowblock].play[i].Remove(city[nowblock].play[i].Last());  city[nowblock].update(); }
                }
                //w = test;   
                //nowcity += dicenum;
                //if (nowcity > 9)
                //{
                //    nowcity -= 10;
                //    nowblock = bfblock;
                //}
                //if (nowcity ==10)
                //{
                //    nowcity = 0;
                //    //nowblock++;
                //    //if (nowblock > 3)
                //    //    nowblock = 0;
                //    nowblock = bfblock;
                //}
                nowblock = bfblock;
                nowcity = bfcity;
                //button3.Text = nowblock.ToString() + "|" + nowcity.ToString();
                if (island >= 3)
                {
                    island = 0;
                    Uninhabited = false;
                }
            }


            if (nowblock == 1 && nowcity == 9)
            {
                if (money_so != 0)
                {
                    //money += money_so;
                    money.m += money_so;
                    MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                    money_so = 0;
                }
            }
            if (nowblock == 0 && nowcity == 9)
            {
                int n = 4 - island;
                if (island == 0)
                    n = 3;
                MessageBox.Show("무인도에 도착하였습니다. 앞으로 " + n.ToString() + "턴동안 무인도에서 나갈수 없습니다.");
            }








            //if (nowblock == 0)
            //{
            //    saveY += 130;
            //    if (buildnum[nowblock * 8 + savepos] == 1)
            //    {
            //        saveX += 25;
            //    }
            //}
            //else if (nowblock == 1)
            //{
            //    saveX -= 1;
            //    if (buildnum[nowblock * 8 + savepos] == 1)
            //    {
            //        saveY -= saveY;
            //    }
            //}
            //else if (nowblock == 2)
            //{
            //    saveY -= 1;
            //    if (buildnum[nowblock * 8 + savepos] == 1)
            //    {
            //        saveX += 30;
            //    }
            //}
            //else
            //{
            //    saveX += 130;
            //    if (buildnum[nowblock * 8 + savepos] == 1)
            //    {
            //        saveY += saveY;
            //    }
            //}
            Invalidate();
            button1.Enabled = true;
            //Rectangle build = new Rectangle(new Point(saveX, saveY), new Size(20, 20));

            //if (savepos != 0 && ((flatform == 0 && (nowcity == 1 || nowcity == 6)) || (flatform == 1 && (nowcity == 1 || nowcity == 4)) || (flatform == 2 && (nowcity == 4 || nowcity == 7)) || (flatform == 3 && (nowcity == 1 || nowcity == 7))) == false)
            //{
            //if ((nowblock == 0 && (nowcity == 1 || nowcity == 6) || nowblock == 1 && (nowcity == 1 || nowcity == 6) || (nowblock == 2 && nowcity == 1) || (nowblock == 3 && nowcity == 4)) == false)
            //{
            //    if (nowcity < 9)
            //    {
            //        if (city[nowblock].cityRect[nowcity].Count < 3)
            //        {
            //            if (MessageBox.Show("이 땅에 건물을 지으시겠습니까?", "구매", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //            {

            //                money -= 100;
            //                city[nowblock].cityRect[nowcity].Add(buildrect[nowblock][nowcity][city[nowblock].cityRect[nowcity].Count]);
            //                //card.drawrectangle(pens.black, build);
            //                //card.fillrectangle(new solidbrush(color.skyblue), build);

            //                buildnum[flatform * 8 + savepos]++;
            //                city[nowblock].updatecity(nowblock);
            //                Invalidate();
            //            }
            //        }
            //    }
            //}
            buildmessage();
            //}


            if (nowblock == 3 && nowcity == 7)
            {
                //money -= 15;
                money.m -= 15;
                money_so += 15;
                MessageBox.Show("사회복지기금(15만원)을 지불하였습니다.");
            }




            //  Card_red();
            start = false;
            cardclick = false;

            //move_write();
            //next_turn();
        }

        public void uninhabit()
        {
            this.island = 3;
            this.Uninhabited = false;
        }

        private void playerfront()
        {
            city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
            reset();
            city[nowblock].play[nowcity].Add(players[nowblock][nowcity]);
            city[nowblock].update(nowblock);
        }
        private void spacemove(int afbl, int afct) 
        {
            int i = nowcity; // 9
            button1.Enabled = false;
            card_clicked = false;
            while (!(nowblock == afbl && i == afct))
            {

                city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                reset();

                city[nowblock].play[i].Add(players[nowblock][i]);
                city[nowblock].update(nowblock);
                Invalidate();
                Delay(100);
                bfcity = i;
                bfblock = nowblock;
                i++;
                // Card_red();
                nowblock = bfblock;
                nowcity = bfcity;
                if (nowblock == 3 && nowcity == 9) //&& afbl != 0)
                {
                    //money += 20;
                    money.m += 20;
                }


                if (afct == 10)
                {
                    if (afbl != nowblock)
                    {
                        if (bfcity > 9 || i > 9)
                        {
                            if (nowblock < 3)
                                nowblock++;
                            else
                                nowblock = 0;
                            nowcity = 0;
                            i = 0;
                        }
                    }
                }
                else if (afct < 10)
                {
                    if (bfcity > 9 || i > 9)
                    {
                        if (nowblock < 3)
                            nowblock++;
                        else
                            nowblock = 0;
                        nowcity = 0;
                        i = 0;
                    }
                }

            }
            card_clicked = true;
            onemove(i, afbl);
            button1.Enabled = true;
            
            buildmessage();
        }

        private void onemove(int city, int afbl)
        {
            this.city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
            reset();

            this.city[nowblock].play[city].Add(players[nowblock][city]);
            this.city[nowblock].update(nowblock);
            Invalidate();
            Delay(100);
            bfcity = city;
            bfblock = nowblock;
            city++;

            nowblock = bfblock;
            nowcity = bfcity;
            //  Card_red();
            if (nowblock == 3 && nowcity == 9)//&& afbl != 0)
            {
                //money += 20;
                money.m += 20;
            }
            if (nowblock != afbl)
            {
                if (bfcity == 9)
                {
                    if (nowblock < 3)
                        nowblock++;
                    else
                        nowblock = 0;
                    nowcity = 0;
                    city = 0;
                }
            }
        }

        private void cardmove(int afbl, int afct)
        {
            button1.Enabled = false;
            if (nowblock == afbl)
            {
                for (int i = nowcity; i < afct; i++)
                {
                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;

                    nowblock = bfblock;
                    nowcity = bfcity;
                }
            }
            else
            {
                int i = nowcity;
                while (!(nowblock == afbl && i == afct))
                {
                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;
                    i++;

                    nowblock = bfblock;
                    nowcity = bfcity;
                    if (nowblock == 3 && nowcity == 9)//&& afbl != 0)
                    {
                        //money += 20;
                        money.m += 20;
                    }
                    if (nowblock != afbl)
                    {
                        if (bfcity == 9)
                        {
                            if (nowblock < 3)
                                nowblock++;
                            else
                                nowblock = 0;
                            nowcity = 0;
                            i = 0;
                        }
                    }
                }
            }
            if (nowblock == 0 && nowcity == 9)
            {
                MessageBox.Show("무인도에 도착하였습니다. 앞으로 3턴동안 무인도에서 나갈수 없습니다.");
            }
            //button3.Text = nowblock.ToString() + "|" + nowcity.ToString();
            button1.Enabled = true;
            //  Card_red();
        }

        private void Tourmove(int afbl, int afct)
        {
            button1.Enabled = false;
            if (nowblock == afbl && nowcity < afct - 1)
            {
                for (int i = nowcity; i < afct; i++)
                {
                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;

                    nowblock = bfblock;
                    nowcity = bfcity;
                }
            }
            else
            {
                int i = nowcity;
                while (!(nowblock == afbl && i == afct))
                {
                    city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                    reset();
                    city[nowblock].play[i].Add(players[nowblock][i]);
                    city[nowblock].update(nowblock);
                    Invalidate();
                    Delay(100);
                    bfcity = i;
                    bfblock = nowblock;
                    i++;

                    nowblock = bfblock;
                    nowcity = bfcity;

                    if (bfcity == 9)
                    {
                        if (nowblock < 3)
                            nowblock++;
                        else
                            nowblock = 0;
                        nowcity = 0;
                        i = 0;
                    }

                }
            }
            button1.Enabled = true;
            buildmessage();
            // Card_red();
        }

    }

}
