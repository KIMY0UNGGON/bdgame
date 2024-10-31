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
        public Point[] C = { new Point(500, 300), new Point(400, 200), new Point(300, 500) }; //카드의 크기.
        bool card_clicked = false;
       

        public List<Card> cards = new List<Card>(); //카드들이 들어있는 리스트.

        public int b_count = -1;

        public void Save_Cards(Card card)
        {
            cards.Add(card);
        }

        public void save_buildcount(int count)
        {

        }

        private void Card_red() //황금 열쇠 카드를 빨갛게 물들임. 카드를 뽑을 수 있는 상태.
        {
            if (player_stop)
            {

                if (IS_KEY()) //현재 위치가 키 카드인지 확인.
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
        }
        int hotel = 0;
        int building = 0;
        int bul = 0;



        private void sell_buildCount()
        {
            var confirm = new List<KeyValuePair< double , string >>();

         
            for(int i  = 0; i< 4; i++)
            {
                for(int j = 0; j< 9; j++)
                {
                    var price = new Double();
                    if (city[i].cityRect[j].Count > 0) //Rect_g cityRect[j][0]
                    {
                        price = city[i].price[0][j];

                            
                        
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


                city[city_b].GroundSell(city_b, block_b);

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

        
            }
        } // 1195 1226  21 //현재의 건물가격+땅가격 통행료X


        public string delete_name = null;


        private void Confirm_buildCount() //방범비 위한 지은 빌딩개수확인
        {
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!(IS_KEY() || (i == 3 && j == 7 )))
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
