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
        public Point[] C = { new Point(500, 300), new Point(400, 200), new Point(300, 500) };
        bool cardclick = false;
        bool card_clicked = false;

        public List<Card> cards = new List<Card>();

        public int b_count = -1;

        public void Save_Cards(Card card)
        {
            cards.Add(card);
        }

        public void save_buildcount(int count)
        {

        }

        private void Card_red()
        {

            if ((nowblock == 0 && (nowcity == 1 || nowcity == 6)) || (nowblock == 1 && (nowcity == 1 || nowcity == 6)) || (nowblock == 2 && nowcity == 1) || (nowblock == 3 && nowcity == 4))
            {
                if (card_clicked)
                    Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel);
                else
                    Card.DrawImage(card_red, C, CardList[0], GraphicsUnit.Pixel);
            }
            else
            {
                Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel);
                card_clicked = false;
            }
            Invalidate();
        }
        bool start_b = false;
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //flatform,  bfk2
            //0,1  0,6  1,1 1,4  2,4  2,6  3,1  3,7
            //if (drawcard == false) {
            if (start_b) { 
                for (int i = 0; i < 4; i++)
                {
                    if (city[i].cacontain(e.Location))
                    {
                        information frm3 = new information();
                        //frm3.money = money;
                        frm3.Show();

                        frm3.money = money.m;
                    }
                }
                if (city[1].containcity(e.Location, 9))
                {
                    socialfund frm4 = new socialfund();
                    frm4.money = money_so;
                    frm4.ShowDialog();
                }
                if (!cardclick)
                {
                    if ((nowblock == 0 && (nowcity == 1 || nowcity == 6)) || (nowblock == 1 && (nowcity == 1 || nowcity == 6)) || (nowblock == 2 && nowcity == 1) || (nowblock == 3 && nowcity == 4))
                    {
                        if (containkey(PtoV.ToVector(e.Location), pArrtoV.Vect(C1)))
                        {
                            //Card.Dispose();
                            //CardGraphic();
                            card_clicked = true;
                            if (Carddr == false)
                            {

                                Card frm2 = new Card();
                                frm2.StartPosition = FormStartPosition.Manual;
                                frm2.Location = new Point(this.Location.X + 1000, this.Location.Y);

                                frm2.Show();
                                button1.Enabled = false;

                                Delay(2000);

                                money.m -= frm2.minus;
                                //money -= frm2.minus;
                                //if (frm2.backm != 0)
                                //{

                                //    //backmove(backm);

                                //}
                                int backm = frm2.backm;

                                cardnum = frm2.Cardnum;


                                timer2.Start();

                                if (cardnum == 4) //무인도
                                {
                                    cardmove(0, 10);
                                }
                                else if (cardnum == 6) //관광여행 제주도
                                {
                                    Tourmove(0, 5);
                                }
                                else if (cardnum == 10 || cardnum == 28)
                                {
                                    backmove(backm);
                                    buildmessage();
                                }
                                else if (cardnum == 11) // 고속도로 출발지까지 이동
                                {
                                    cardmove(3, 10);
                                    //money += 20;
                                    money.m += 20;
                                }
                                else if (cardnum == 14) //항공여행 타이베이
                                {

                                    int i = nowcity;
                                    while (!(nowblock == 0 && i == 1))
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
                                        if (nowblock == 3 && nowcity == 9)
                                        {
                                            money.m += 20;

                                        }
                                        if (bfcity == 9)
                                        {
                                            if (nowblock < 3)
                                                nowblock++;
                                            else
                                                nowblock = 0;
                                            nowcity = 0;
                                            i = 0;
                                        }
                                        // 비용추가

                                    }
                                    buildmessage();
                                }
                                else if (cardnum == 15)
                                {
                                    Confirm_buildCount();
                                    money.m -= 10 * hotel;
                                    money.m -= 6 * building;
                                    money.m -= 3 * bul;
                                }
                                else if (cardnum == 16) // 유람선 여행 베이징 행 퀸 엘리자베스 호 탑승료 지불
                                {
                                    Confirm_buildCount();
                                    money.m -= 5 * hotel;
                                    money.m -= 3 * building;
                                    money.m -= 1 * bul;

                                }
                                else if (cardnum == 17) // 유람선 여행 베이징 행 퀸 엘리자베스 호 탑승료 지불
                                {
                                    Tourmove(0, 3);
                                    // 비용추가
                                }
                                else if (cardnum == 18) // 관광여행 부산
                                {
                                    Tourmove(2, 5);
                                    //통행료 지불
                                }
                                else if (cardnum == 21)
                                {
                                    Confirm_buildCount();
                                    money.m -= 15 * hotel;
                                    money.m -= 10 * building;
                                    money.m -= 3 * bul;
                                }
                                else if (cardnum == 23) //관광여행 서울
                                {
                                    Tourmove(3, 9);
                                    //통행료 지불 기능
                                }
                                else if (cardnum == 24 || cardnum == 30)
                                {
                                    sell_buildCount();
                                }
                                else if (cardnum == 25) // 우주여행 초청장
                                {
                                    cardmove(2, 10);
                                }
                                else if (cardnum == 27) //세계일주 초대권
                                {
                                    int i = nowcity;
                                    int bl = nowblock;
                                    int nc = nowcity;
                                    for (int l = nowcity; l < 10; l++)
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
                                            if (nowblock == 3 && nowcity == 9)
                                            {
                                                money.m += 20;
                                                //money.Plus(20);
                                            }
                                            if (nowblock < 3)
                                                nowblock++;
                                            else
                                                nowblock = 0;
                                            nowcity = 0;
                                            i = 0;
                                        }
                                    }

                                    while (!(nowblock == bl && i == nc))
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
                                            if (nowblock == 1 && nowcity == 9)
                                            {
                                                if (money_so != 0)
                                                {
                                                    money.m += money_so;
                                                    //money.Plus(money_so);
                                                    MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                                                    money_so = 0;
                                                }
                                            }

                                            if (nowblock == 3 && nowcity == 9)
                                            {
                                                money.m += 20;
                                                //    money.Plus(20);
                                            }
                                            if (nowblock < 3)
                                                nowblock++;
                                            else
                                                nowblock = 0;
                                            nowcity = 0;
                                            i = 0;
                                        }

                                    }
                                    onemove(i, bl);

                                }
                                else if (cardnum == 29) //사회복지기금 배당
                                {
                                    cardmove(1, 10);
                                    if (money_so != 0)
                                    {
                                        money.m += money_so;
                                        //money.Plus(money_so);
                                        MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                                        money_so = 0;
                                    }
                                }
                                button1.Enabled = true;
                            }
                        }
                    }
                }

                //if (frm2.startpoint)
                //{
                //    for(int i = 0; i < 4; i++)
                //    {
                //        for(int l = 0; l < 9; l++)
                //        {

                //        }
                //    }
                //}
                //Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel); //93,69 번호 //70 119 제목 //21 167 내용 // 37,201 행동
                //200 : 280                                               
                //    Card.DrawImage(cardg, new Rectangle(600, 400, 200, 280), CardList[1], GraphicsUnit.Pixel);
                //    te();
                //    test = true;
                else
                {
                    // Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel);
                    Carddr = false;
                }
                Invalidate();
                cardclick = true;
                //drawcard = true;
                //}
                loopconfirm = 0;

                // }
                // }
            }
        }
        int hotel = 0;
        int building = 0;
        int bul = 0;



        private void sell_buildCount()
        {
            var confirm = new List<KeyValuePair< double , string >>();
            //MessageBox.Show(city[0].price[0][0].ToString());
         
            for(int i  = 0; i< 4; i++)
            {
                for(int j = 0; j< 9; j++)
                {
                    var price = new Double();
                    if (city[i].cityRect[j].Count > 0) //Rect_g cityRect[j][0]
                    {
                        price = city[i].price[0][j];
                        //if (city[i].cityRect[j].Count > 1)  
                        //{
                        //    for (int k = 1; k < city[i].cityRect[j].Count; k++)
                        //        price += city[i].price[k][j];
                        //    //confirm.Add(new KeyValuePair<double, string>(price,i.ToString()+" "+j.ToString()));
                        //}

                            
                        
                    }
                    if(city[i].Rect_Villa[j].X != 0)
                    {
                        price += city[i].price[1][j];
                    }
                    if(city[i].Rect_Building[j].X != 0)
                    {
                        price += city[i].price[2][j];
                    }
                    if(city[i].Rect_Hotel[j].X != 0)
                    {
                        price += city[i].price[3][j];
                    }
                    confirm.Add(new KeyValuePair<double, string>(price, i.ToString() + " " + j.ToString()));
                }
            }


            confirm.Sort((x,y) =>  x.Key.CompareTo(y.Key));
            if (confirm.Count > 0)
            {
                
                
                
                money.m += confirm.Last().Key;
                string[] xy = confirm.Last().Value.Split(new string[] { " " },StringSplitOptions.None);
                int city_b = Convert.ToInt32(xy[0]);
                int block_b = Convert.ToInt32(xy[1]);
                city[city_b].cityRect[block_b].Clear();





                //city[city_b].PlayerRemove(city_b, block_b);
                city[city_b].GroundSell(city_b, block_b);
                //city[city_b].update(block_b);
                //city[city_b].updatecity(city_b, block_b);
                Invalidate();
                string name = city[city_b].name_str[block_b];

                delete_name = name;

                for(int i = 0; i< bclist_all.Count; i++)
                {
                    if (bclist_all[i].Cards[0].Equals(name))
                    {
                        bclist_all[i].Close();
                        if(bclist_infor.Contains(bclist_all[i]))
                            bclist_infor.Remove(bclist_all[i]);
                        bclist_all.RemoveAt(i);
                        
                        
                        break;
                    }
                }

                //city[city_b].name[] //링크?  (city_b-1)*7+block_b 
        
            }
        } // 1195 1226  21 //현재의 건물가격+땅가격 통행료X


        public string delete_name = null;


        private void Confirm_buildCount() //방범비 위한 개수확인
        {
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (((i == 0 && (j == 1 || j == 6)) || (i == 1 && (j == 1 || j == 6)) || (i == 2 && j == 1) || (i == 3 && (j == 7 || j== 4))) == false)
                    {
                        if (city[i].cityRect[j].Count() == 4)
                        {
                            hotel++;
                            building++;
                            bul++;
                        }
                        else if (city[i].cityRect[j].Count() == 3)
                        {
                            bul++;
                            building++;
                        }
                        else if (city[i].cityRect[j].Count() == 2)
                        {
                            bul++;
                        }
                    }
                }
            }
        }
    }
}
