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
        bool test_mode = true;

        private bool IS_KEY() //카드 키인지 확인하는 메소드.
        {
            return (nowblock == 0 && (nowcity == 1 || nowcity == 6)) || (nowblock == 1 && (nowcity == 1 || nowcity == 6)) || (nowblock == 2 && nowcity == 1) || (nowblock == 3 && nowcity == 4);
        }

        private void TestMode_Click(object sender, EventArgs e) //테스트 모드.
        {
            butt_solo.Visible = false;
            butt_multi.Visible = false;
            start_confirm = true;
            initMultyImage();
            Form_show();
            test_mode = true;
            SpaceTrip();

        }
        private void Form1_MouseUp(object sender, MouseEventArgs e) //마우스 클릭이벤트
        {
            
            if (e.Button == MouseButtons.Right && test_mode)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem test_activate = new ToolStripMenuItem("테스트 모드");
                menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { test_activate});
                menu.Size = new System.Drawing.Size(181, 70);
                test_activate.Size = new System.Drawing.Size(180, 22);
                test_activate.Text = "테스트 모드";
                test_activate.Click += new System.EventHandler(this.TestMode_Click);
                menu.Show(MousePosition);
            }
            if (start_confirm) //게임이 시작중인지 확인.
            {
                //for (int i = 0; i < 4; i++)
                //{
                int Want_Num; //현재 클릭한 말의 클라이언트 넘버.
                if ((Want_Num = city[nowblock].Token_Click(e.Location)) != -1) //플레이어의 말 클릭시 플레이어의 정보를 확인할 수 있는 인터페이스를 열음.
                {
                    if (Multi)
                    {
                        server.send($"w/{Want_Num}/{Multy_Num}"); //서버에 클릭한 말의 정보를 달라고 요청.
                    }
                    else
                    {
                        information frm3 = new information(); //말의 정보가 들어있는 form의 인스턴스를 만들고 보여줌,
                        frm3.Show();
                    }
                }
                //}
                if (city[1].containcity(e.Location, 9)) //사회 기금.
                {
                    socialfund frm4 = new socialfund();
                    frm4.money = money_so;
                    frm4.ShowDialog();
                }
                if (!card_clicked) //황금열쇠를 클릭했는지 확인. 안했으면 TRUE
                {
                    if (IS_KEY()) //지금 위치가 황금열쇠인지.
                    {
                        if (containkey(PtoV.ToVector(e.Location), pArrtoV.Vect(C1))) //클릭한 좌표가 황금열쇠의 안인지 확인. 
                        {
                            card_clicked = true; //황금열쇠를 클릭했다고 변경.
                            if (Carddr == false)
                            {

                                Card frm2 = new Card();
                                frm2.StartPosition = FormStartPosition.Manual;
                                frm2.Location = new Point(this.Location.X + 1000, this.Location.Y);

                                frm2.Show();
                                button1.Enabled = false;

                                Delay(2000);

                                money.m -= frm2.minus;

                                int backm = frm2.backm;

                                cardnum = frm2.Cardnum;

                                if (cardnum == 4) //무인도카드
                                {
                                    onemove(move_calc(0, 9),true);
                                }
                                else if (cardnum == 6) //관광여행 제주도 카드
                                {
                                    onemove(move_calc(0, 5));
                                }
                                else if (cardnum == 10 || cardnum == 28) //이사 뒤로 3칸.
                                {
                                    onemove(Math.Abs(backm),Back:true);
                                    buildmessage();
                                }
                                else if (cardnum == 11) // 고속도로 출발지까지 이동 카드
                                {
                                    onemove(move_calc(3, 9));
                                    //money += 20;
                                    //money.m += 20;
                                }
                                else if (cardnum == 14) //항공여행 타이베이 카드
                                {

                                    int i = nowcity;
                                    while (!(nowblock == 0 && i == 1))
                                    {
                                        city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                                        reset(bfblock,bfcity);
                                        city[nowblock].play[i].Add(players[nowblock][i]);
                                        city[nowblock].update(nowblock);
                                        Invalidate();
                                        Delay(100);
                                        bfcity = i;
                                        bfblock = nowblock;
                                        i++;

                                        nowblock = bfblock;
                                        nowcity = bfcity;
                                        //if (nowblock == 3 && nowcity == 9)
                                        //{
                                        //    money.m += 20;

                                        //}
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
                                    //Tourmove(0, 3);
                                    // 비용추가
                                }
                                else if (cardnum == 18) // 관광여행 부산
                                {
                                    onemove(move_calc(2, 5));
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
                                    onemove(move_calc(3, 9));
                                    //통행료 지불 기능
                                }
                                else if (cardnum == 24 || cardnum == 30)
                                {
                                    sell_buildCount();
                                }
                                else if (cardnum == 25) // 우주여행 초청장
                                {
                                    onemove(move_calc(2, 9));
                                }
                                else if (cardnum == 27) //세계일주 초대권
                                {
                                    int i = nowcity;
                                    int bl = nowblock;
                                    int nc = nowcity;
                                    for (int l = nowcity; l < 10; l++)
                                    {
                                        city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                                        reset(bfblock,bfcity);
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
                                        reset(bfblock,bfcity);
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
                                   // onemove(ref i, nowblock); //어떤 것인지 확인 필요

                                }
                                else if (cardnum == 29) //사회복지기금 배당
                                {
                                    onemove(move_calc(1, 9));
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
                //drawcard = true;
                //}
                //loopconfirm = 0;

                // }
                // }
            }
        }
    }
 
}
