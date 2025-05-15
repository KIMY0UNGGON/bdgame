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
        private bool Is_Uninhabited() //무인도인지 확인하는 메소드
        {
            if (nowblock == 0 && nowcity == 9) //현재 위치가 무인도라면 TRUE 반환.
                return true;
            return false;
        }
        private double Toll_fee(int nowblock, int nowcity)
        {
            bool g_c = confirm_ground(nowblock, nowcity);
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

        }
        private void button1_Click(object sender, EventArgs e) //주사위를 던지는 버튼을 클릭.
        {
            player_stop = false;
            Random dice = new Random(); //주사위의 수를 표현하기위해 랜덤클래스 사용.

            int dicenum1 = dice.Next(1, 7); //첫번째 주사위의 수.
            int dicenum2 = dice.Next(1, 7); //두번째 주사위의 수.
            button1.Text = dicenum1.ToString() + "|" + dicenum2.ToString(); //버튼에 첫번째와 두번째 주사위의 수를 표시.
            int dicenum = dicenum1 + dicenum2; //말이 움직일 수를 표현.


            Rectangle dicepos = new Rectangle(600, 700, 100, 100); //첫번째 주사위의 위치. 그림으로 표현되는 주사위.
            Rectangle dicepos1 = new Rectangle(710, 700, 100, 100); //두번째 주사위의 위치.
            Point x = dicepos.Location;
            dices.DrawImage(dice1, dicepos, Dicelist[dicenum1 - 1], GraphicsUnit.Pixel); //주사위를 그림. 아래코드도 마찬가지.
            dices.DrawImage(dice1, dicepos1, Dicelist[dicenum2 - 1], GraphicsUnit.Pixel);
            button1.Enabled = false; //주사위가 굴러갈때 주사위를 한번더 굴리는 걸 방지.
           // skip = 0;

            if (nowcity == 9 && nowblock == 2) //우주여행 칸일 때 버튼들을 생성해 움직일 수 있게 함.
            {
                SpaceTrip();
            }
 
            if (Multi)
            {
                MultyMove(dicenum);
            }
            else
            {
                onemove(dicenum);
            }

            Invalidate();
            button1.Enabled = true;

            buildmessage(); //건물을 지을것인지 질문.

            if (nowblock == 1 && nowcity == 9) //사회복지기금
            {
                if (money_so != 0)
                {
                    money.m += money_so;
                    MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                    money_so = 0;
                }
            }
            if (nowblock == 3 && nowcity == 7)
            {
                money.m -= 15;
                money_so += 15;
                MessageBox.Show("사회복지기금(15만원)을 지불하였습니다.");
            }
            //start = false;
            player_stop = true;
            Turn = false;
        }

        

        public void uninhabit()
        {
            this.island = 3;
            this.Uninhabited = false;
        }

        private void playerfront() //플레이어를 다시 앞으로 그리기
        {
            Players_Token.ForEach(x => city[bfblock].play.Remove(x));
            reset(bfblock,bfcity);
            Players_Token.ForEach(x => city[nowblock].play.Add(x));
            city[nowblock].update(nowblock);
        }
        private void butt_Click(object sender, EventArgs e)
        {
            //loopconfirm++;
            player_stop = true;
            Test_Turn = true;
            string butt_name = ((Button)sender).ToString();
            string[] bname = butt_name.Split(' ');
            string[] number = bname[2].Split('|');
            int block = Convert.ToInt32(number[0]);
            int city = Convert.ToInt32(number[1]);
            if (test_mode == false)
            {
                for (int i = 0; i < buttonTravel.Count; i++)
                {
                    Controls.Remove(buttonTravel[i]);
                }
            }

            spacemove(block, city);
            if (nowblock == 3 && nowcity == 7)
            {
                money.m -= 15;
                money_so += 15;
            }
            if (nowblock == 1 && nowcity == 9)
            {
                if (money_so != 0)
                {
                    money.m += money_so;
                    MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                    money_so = 0;
                }
            }

        }
        private void SpaceTrip()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int l = 0; l < 10; l++)
                {
                    if (i == 2 && l == 9)
                    {
                        continue;
                    }
                    Button space_butt = new Button();
                    space_butt.Location = new System.Drawing.Point(city[i].cities[l].X, city[i].cities[l].Y);
                    space_butt.Name = "button1";
                    space_butt.Size = new System.Drawing.Size(75, 23);
                    space_butt.TabIndex = 0;
                    space_butt.Text = i + "|" + l;
                    space_butt.UseVisualStyleBackColor = true;
                    Controls.Add(space_butt);
                    space_butt.Click += new System.EventHandler(this.butt_Click);

                }
            }

        }


        private int move_calc(int move_block, int move_city)//매개변수는 이동할 좌표. 이동할 좌표까지 현재 위치에서 몇칸 움직여야 하는지 반환하는 메소드 
        {
            int result = nowblock - move_block; // 현재 블럭에서 이동할 블럭의 차. 
            if (result < 0)
            {
                result = (Math.Abs(result) - 1) * 10 + (10 - nowcity) + move_city;
            }
            else if (result > 0)
            {
                result = (3 - Math.Abs(result)) * 10 + (10 - nowcity) + move_city;
            }
            else
            {
                if (nowcity >= move_city)
                {
                    result = 30 + (10 - nowcity) + move_city;
                }
                else result = move_city - nowcity;

            }
            return result;
        }
        private void spacemove(int afbl, int afct)
        {
            button1.Enabled = false;
            //card_clicked = false;
            player_stop = true;
            Test_Turn = true;
            onemove(move_calc(afbl, afct));
            card_clicked = false;

            button1.Enabled = true;

            buildmessage();
        }
        private void MultyMove(int move, bool penalty = false, bool Back = false) //멀티 플레이시 움직임.
        {
            player_stop = true;
            if (!Inhabit_move)
            {
                server.Thread_send($"m/{move}"); //움직일 횟수를 서버에 전송.
                player_stop = false;

                for (int i = 0; i < move; i++)
                {
                    //this.city[bfblock].play[bfcity].Remove(Multy_Token[Multy_Num - 1][bfblock][bfcity]); //현재 위치의 말을 삭제.
                    this.city[bfblock].play.Remove(Players_Token[Multy_Num - 1]); //현재 위치의 말을 삭제.

                    reset(bfblock, bfcity); //판을 다시그림.
                    if (!Back)
                        nowcity++;
                    else
                        nowcity--;
                    if (nowcity > 9)
                    {
                        if (nowblock < 3)
                            nowblock++;
                        else
                            nowblock = 0;
                        nowcity = 0;
                    }
                    else if (nowcity < 0)
                    {
                        if (nowblock < 0)
                            nowblock = 3;
                        else
                            nowblock--;
                    }
                    if (!penalty && nowblock == 3 && nowcity == 9) //출발지 월급
                    {
                        money.m += 20;
                    }
                    this.city[nowblock].play.Add(Players_Token[Multy_Num - 1]);
                    this.city[nowblock].update(nowcity);
                    Invalidate();

                    Delay(100);



                    bfcity = nowcity;
                    bfblock = nowblock;
                }
                //Test_Turn = false;
                //  Card_red();


            }
            player_stop = true;
            Test_Turn = true; //말이 움직였을 때 턴이 넘어감.
        }
        private void onemove(int move, bool penalty = false, bool Back = false) //현재 이동 매커니즘의 중심. 매개변수 페널티는 황금열쇠에서 나온 무인도로 가는 것을 표시하기 위함.
        {
            //뒤로 움직일 때.
            player_stop = true;
            if (!Inhabit_move)
            {
                
                player_stop = false;
         
                for (int i = 0; i < move; i++)
                {
                    this.city[bfblock].play.Remove(Players_Token[Multy_Num - 1]); //현재 위치의 말을 삭제.
                    reset(bfblock,bfcity); //판을 다시그림.
                    if (!Back)
                        nowcity++;
                    else
                        nowcity--;
                    if (nowcity > 9)
                    {
                        if (nowblock < 3)
                            nowblock++;
                        else
                            nowblock = 0;
                        nowcity = 0;
                    }
                    else if(nowcity < 0)
                    {
                        if (nowblock < 0)
                            nowblock = 3;
                        else
                            nowblock--;
                    }
                    if (!penalty && nowblock == 3 && nowcity == 9) //출발지 월급
                    {
                        money.m += 20;
                    }
                    this.city[nowblock].play.Add(Players_Token[Multy_Num - 1]);
                    this.city[nowblock].update(nowcity);
                    Invalidate();
                    Delay(100);
                    bfcity = nowcity;
                    bfblock = nowblock;
                }
                //Test_Turn = false;
                //  Card_red();


            }
            player_stop = true;
            Test_Turn = true; //말이 움직였을 때 턴이 넘어감.
            if (LOSE_CONDITION())
            {
                if (Multi)
                {
                    server.send($"LOSE/{Multy_Num}");
                }

            }
        }

        private async void Move_OtherToken(int move, int num)// 다른 클라이언트의 플레이어 그리기
        {

            for (int i = 0; i < move; i++) //말이 있는 칸부터 주사위 숫자를 더한 칸  
            {
                Nowcity_List[num]++;
                if (Nowcity_List[num] > 9) // 구역 넘어감
                {
                    if (Nowblock_List[num] < 3) Nowblock_List[num]++;
                    else Nowblock_List[num] = 0;
                    Nowcity_List[num] = 0;
                }
                city[Before_block[num]].play.Remove(Players_Token[num - 1]);
           
                reset(Before_block[num], Before_city[num]);
                city[Nowblock_List[num]].play[num - 1].Visible = true;
                city[Nowblock_List[num]].update(num);
                Invalidate();
                //Delay(100);
                var task1 = DoSomethingAsync(100);
                await Task.WhenAll(task1);
                Before_city[num] = Nowcity_List[num];
                Before_block[num] = Nowblock_List[num];
            }
        }

    }
}

        //private void cardmove(int afbl, int afct)
        //{
        //    button1.Enabled = false;
        //    if (nowblock == afbl)
        //    {
        //        for (int i = nowcity; i < afct; i++)
        //        {
        //            city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
        //            reset();
        //            city[nowblock].play[i].Add(players[nowblock][i]);
        //            city[nowblock].update(nowblock);
        //            Invalidate();
        //            Delay(100);
        //            bfcity = i;
        //            bfblock = nowblock;

        //            nowblock = bfblock;
        //            nowcity = bfcity;
        //        }
        //    }
        //    else
        //    {
        //        int i = nowcity;
        //        while (!(nowblock == afbl && i == afct))
        //        {
        //            city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
        //            reset();
        //            city[nowblock].play[i].Add(players[nowblock][i]);
        //            city[nowblock].update(nowblock);
        //            Invalidate();
        //            Delay(100);
        //            bfcity = i;
        //            bfblock = nowblock;
        //            i++;

        //            nowblock = bfblock;
        //            nowcity = bfcity;
        //            if (nowblock == 3 && nowcity == 9)//&& afbl != 0)
        //            {
        //                //money += 20;
        //                money.m += 20;
        //            }
        //            if (nowblock != afbl)
        //            {
        //                if (bfcity == 9)
        //                {
        //                    if (nowblock < 3)
        //                        nowblock++;
        //                    else
        //                        nowblock = 0;
        //                    nowcity = 0;
        //                    i = 0;
        //                }
        //            }
        //        }
        //    }
        //    if (nowblock == 0 && nowcity == 9)
        //    {
        //        MessageBox.Show("무인도에 도착하였습니다. 앞으로 3턴동안 무인도에서 나갈수 없습니다.");
        //    }

        //    button1.Enabled = true;
        //    //  Card_red();
        //}

        //private void Tourmove(int afbl, int afct)
        //{
        //    button1.Enabled = false;
        //    if (nowblock == afbl && nowcity < afct - 1)
        //    {
        //        for (int i = nowcity; i < afct; i++)
        //        {
        //            city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
        //            reset();
        //            city[nowblock].play[i].Add(players[nowblock][i]);
        //            city[nowblock].update(nowblock);
        //            Invalidate();
        //            Delay(100);
        //            bfcity = i;
        //            bfblock = nowblock;

        //            nowblock = bfblock;
        //            nowcity = bfcity;
        //        }
        //    }
        //    else
        //    {
        //        int i = nowcity;
        //        while (!(nowblock == afbl && i == afct))
        //        {
        //            city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
        //            reset();
        //            city[nowblock].play[i].Add(players[nowblock][i]);
        //            city[nowblock].update(nowblock);
        //            Invalidate();
        //            Delay(100);
        //            bfcity = i;
        //            bfblock = nowblock;
        //            i++;

        //            nowblock = bfblock;
        //            nowcity = bfcity;

        //            if (bfcity == 9)
        //            {
        //                if (nowblock < 3)
        //                    nowblock++;
        //                else
        //                    nowblock = 0;
        //                nowcity = 0;
        //                i = 0;
        //            }

        //        }
        //    }
        //    button1.Enabled = true;
        //    buildmessage();
        //    // Card_red();
        //}


