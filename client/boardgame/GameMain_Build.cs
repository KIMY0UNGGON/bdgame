﻿using System;
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



        //private void build_rect(int nowblock, int nowcity, Rectangle rect, int count = 0)
        //{
        //    money.m -= city[nowblock].price[count][nowcity];
        //    rect = buildrect[nowblock][nowcity][count];
        //    city[nowblock].updatecity(nowblock, nowcity, rect,count);
        //}
        

        private void buildmessage()
        {

            //if(city[nowblock].cityRect[nowcity].)

            if (((nowblock == 0 && (nowcity == 1 || nowcity == 6)) || (nowblock == 1 && (nowcity == 1 || nowcity == 6)) || (nowblock == 2 && nowcity == 1) || (nowblock == 3 && (nowcity == 7 || nowcity == 4))) == false)
            {
                if (nowcity < 9)
                {
                    
                    
                    if (city[nowblock].cityRect[nowcity].Count <= 3)    
                    {
                        if (!((((nowblock == 0 && nowcity == 4) || (nowblock == 1 && nowcity == 4) || (nowblock == 2 && (nowcity == 4 || nowcity == 7)) || (nowblock == 3 && (nowcity == 8 || nowcity == 1)))) && city[nowblock].cityRect[nowcity].Count != 0))
                        {

                            if (MessageBox.Show("이 땅에 건물을 지으시겠습니까?", "구매", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Select_build sb = new Select_build();

                                sb.ShowDialog();


                                

                                //int count = city[nowblock].cityRect[nowcity].Count;
                                if (b_ground)
                                {
                                    money.m -= city[nowblock].price[0][nowcity];
                                    city[nowblock].Rect_Ground[nowcity] = buildrect[nowblock][nowcity][0];
                                    city[nowblock].updatecity(nowblock, nowcity);
                                    // build_rect(nowblock, nowcity, city[nowblock].Rect_Ground[nowcity]);
                                }
                                if (b_villa)
                                {
                                    money.m -= city[nowblock].price[1][nowcity];
                                    city[nowblock].Rect_Villa[nowcity] = buildrect[nowblock][nowcity][1];
                                    city[nowblock].cityRect[nowcity].Add(city[nowblock].Rect_Villa[nowcity]);
                                    city[nowblock].updatecity(nowblock, nowcity, 1);
                                    // build_rect(nowblock, nowcity, city[nowblock].Rect_Villa[nowcity],1);
                                }
                                if (b_building)
                                {
                                    money.m -= city[nowblock].price[2][nowcity];
                                    city[nowblock].Rect_Building[nowcity] = buildrect[nowblock][nowcity][2];
                                    city[nowblock].cityRect[nowcity].Add(city[nowblock].Rect_Building[nowcity]);
                                    city[nowblock].updatecity(nowblock, nowcity, 2);
                                    // build_rect(nowblock, nowcity, city[nowblock].Rect_Building[nowcity],2);
                                }
                                if (b_hotel)
                                {
                                    money.m -= city[nowblock].price[3][nowcity];
                                    city[nowblock].Rect_Hotel[nowcity] = buildrect[nowblock][nowcity][3];
                                    city[nowblock].cityRect[nowcity].Add(city[nowblock].Rect_Hotel[nowcity]);
                                    city[nowblock].updatecity(nowblock, nowcity, 3);
                                    // build_rect(nowblock, nowcity, city[nowblock].Rect_Hotel[nowcity], 3);
                                }

                                // city[nowblock].cityRect[nowcity].Add(buildrect[nowblock][nowcity][city[nowblock].cityRect[nowcity].Count]);


                                //playerfront();
                                buildCard bcard = new buildCard();
                                bcard.color = nowblock; //어느 구역인지 저장.
                                bcard.city_address = nowcity;
                                if (nowblock < 2) //여기부터 loca변수에 건물이 어디있는지 loca에 값을 줌. loca값은 타이베이 부터 서울까지의 도시만의 수.
                                {
                                    if (nowcity > 6)
                                        bcard.loca = nowblock * 7 + nowcity - 2;
                                    else if (nowcity > 1)
                                        bcard.loca = nowblock * 7 + nowcity - 1;
                                    else
                                        bcard.loca = nowblock * 7 + nowcity;
                                }
                                else
                                {
                                    if (nowblock == 2)
                                    {
                                        if (nowcity > 1)
                                            bcard.loca = nowblock * 7 + nowcity - 1;
                                        else
                                            bcard.loca = nowblock * 7 + nowcity;
                                    }
                                    else
                                    {
                                        if (nowcity > 7)
                                            bcard.loca = 22 + nowcity - 2;
                                        else if (nowcity > 4)
                                            bcard.loca = 22 + nowcity - 1;
                                        else
                                            bcard.loca = 22 + nowcity;
                                    }

                                }

                                if (b_ground)
                                    bcard.Show();
                                bclist_all.Add(bcard);
                                playerfront();
                                Invalidate();
                                //write_build(); 서버에 건물을 지었다고 전송.
                            }
                        }
                    }
                }
            }
        }
    }
}


