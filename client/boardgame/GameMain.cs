using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace boardgame
{
    public partial class GameMain : Form
    {
        bool player_stop = true;
        public Point[] C1 = { new Point(500, 300), new Point(400, 200), new Point(200, 400), new Point(300, 500) };
        public GameMain()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }


        bool Turn = false; //현재 자신의 턴인지 확인.

        Bitmap bit;
        Graphics DC, dices, Card, nameGp;
        Bitmap map, dice1, key, cardg, player, player2, nameBt, card_red;
        //Area area;
        cityArea[] city = new cityArea[4]; //cityArea 들의 리스트 도시들의 구역들을 지정하고 그리기 위한 클래스 들임.


        Bitmap areacard, areacard1, areacard2, areacard3;
        Bitmap social;
        public List<buildCard> bclist_infor = new List<buildCard>();
        List<buildCard> bclist_all = new List<buildCard>(); //건물 카드들이 들어있는 리스트

        public int island { get; set; } = 0; //무인도에 들어가있는 턴

        public bool Uninhabited { get; set; } = false; //무인도에 있는지

        public List<Rectangle> Dicelist = new List<Rectangle>();
        public List<Rectangle> CardList = new List<Rectangle>();
        public List<String> CardText = new List<String>();
        List<Player>[] players = new List<Player>[4];

        List<Button> buttonTravel = new List<Button>();

        bool Carddr = false;

        Money money = new Money();

        public double money_retur()
        {
            return money.m;
        }
        bool RED = false;
        bool BLUE = false;
        bool GRAY = false;
        bool BLACK = false;
        public int nowcity { get; set; } = 9;
        public int nowblock { get; set; } = 3;
        int bfcity { get; set; } = 9;
        int bfblock { get; set; } = 3;

        bool start = true;

        public int cardnum;
        public int[] buildnum = new int[36];
        Rectangle[][] playerrect = new Rectangle[4][];
        Rectangle[][][] buildrect = new Rectangle[4][][];

        public int money_so { get; set; } = 100;
        Rectangle[][] buyground = new Rectangle[4][];


        information inform = new information();







        private void ground() //구매 가능한 땅에 대해 구역을 지정해 배열로서 표현.
                              //구매 가능한 땅에 지어질 수 있는 건물의 사각형의 크기를 지정함.
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 2)
                {
                    buyground[i] = new Rectangle[7];
                }
                else
                {
                    buyground[i] = new Rectangle[8];
                }
                for (int j = 0; j < 8; j++)
                {
                    if (i == 2)
                    {
                        if (j == 1)
                        {
                            continue;
                        }
                        buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);

                    }
                    else
                    {
                        if (i < 3)
                        {
                            if (j == 1 || j == 6)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (j == 4 || j == 7)
                            {
                                continue;
                            }
                        }
                        buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);
                    }
                }
            }
        }




        Button bt;
        private void Form1_Load(object sender, EventArgs e)
        {

            // Server_connect(); //서버와 연결. 다른 말들의 정보를 받기 위함.
            initDC(); //그래픽들을 할당해 주는 메소드

            //Form_show();
            this.Size = new Size(300, 200);
          


            
        }

        private void Form_show() //게임 화면을 그림.
        {
            //bt.Visible = false;
         
            this.Size = new Size(cityArea.side - 1, cityArea.down - 1); //폼의 크기를 보드의 크기만큼 변환.
  
            


            init();


            imagea(); //도시들의 좌표, 플레이어의 말 크기 및 좌표.
            //playerimage();
            image(); //황금 카드의 좌표 및 주사위의 크기 및 좌표
            rectcity(); // 구매하면 건설될 건물들의 크기 및 좌표 지정.
            //timer1.Start();
            timer2.Start();
            StreamWriter s1 = new StreamWriter(new FileStream("buildSave.txt", FileMode.Create));
            s1.Close();

            StreamReader sr = new StreamReader(new FileStream("Card.txt", FileMode.Open)); //황금 카드들의 내용들을 불러옴.

            while (sr.EndOfStream == false) //읽어온 파일들을 끝까지 반복.
            {
                CardText.Add(sr.ReadLine());  //읽어온 파일의 내용을 리스트에 저장.
            }

            sr.Close();

            StreamWriter sw = new StreamWriter(new FileStream("Save.txt", FileMode.Create));
            sw.Close();

            Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel);


            Player player = players[3][9]; //플레이어 말의 위치 정보를 처음 시작하는 구간으로 저장. 첫번째 배열은 도시들의 구역. 두번째 배열은 도시들의 순서.
            city[3].play[9].Add(player); //마지막 구역. 보드판에서는 시작 구역에 플레이어 말을 배치.

            city[3].update(nowblock); //도시 구역들을 업데이트하여 수정된 정보들을 다시 그림.
            Invalidate();
            for (int i = 0; i < 4; i++) //이새끼 뭐야?
            {
                name[i] = new List<string>();
                name[i] = city[i].name_str;
            }

            button1.Visible = true;
            timer3.Start();
        }





        public List<string>[] name = new List<string>[4];



        int skip = 0;

        private void GameMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //sock.Close();
        }

      



        private void bu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butt_solo_Click_1(object sender, EventArgs e)
        {
            butt_solo.Visible = false;
            butt_multi.Visible = false;
            start_confirm = true;
            Form_show();
        }





        private static DateTime Delay(int MS)

        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)

            {

                System.Windows.Forms.Application.DoEvents();

                ThisMoment = DateTime.Now;

            }

            return DateTime.Now;

        }


        private bool containkey(Vector2 point, Vector2[] polygon)
        {

            int polygonlength = polygon.Length, i = 0;
            bool inside = false;

            float pointX = point.X, pointY = point.Y;
            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonlength - 1];
            endX = endPoint.X;
            endY = endPoint.Y;

            while (i < polygonlength)
            {
                startX = endX; startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.X; endY = endPoint.Y;

                inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                && /* if so, test if it is under the segment */
                ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            return inside;
        }




        private void DataReceiveEvent(bool data) //무인도 정보 받음
        {
            if (data == false)
            {
                island = 3;
                Uninhabited = false;
                //timer2.Stop();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //string[] butt = button3.Text.Split('|');
            //Select_build sb = new Select_build();
            //sb.ShowDialog();

        }

 
        MySocket server;

        private void backmove(int movec)
        {
            int fq;
            if (nowcity < movec)
            {
                fq = 10 + (nowcity - movec);

           
            }
            else fq = nowcity - movec;

            for (int i = 0; i < movec; i++)
            {
                
                if (nowcity == 0)
                {
                    if (nowblock > 0)
                        nowblock -= 1;
                    else
                        nowblock = 3;
                }
                nowcity -= 1;
                if(nowcity < 0)
                {
                    nowcity += 10;
                }

                if (nowblock == 3 && nowcity == 9) money.m += 20;
                city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                
                
                reset();
                city[nowblock].play[nowcity].Add(players[nowblock][nowcity]);
                city[nowblock].update(nowblock);
                Delay(100);
                
                Invalidate();
                bfblock = nowblock;
                bfcity = nowcity;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            city[0].play[0].Add(players[0][0]);
            city[0].update(0);
            Invalidate();
        }
        public void input_carddata(buildCard bc) //카드 집어 넣는 메소드
        {
            inform.Card_put(bc);
            bclist_infor.Add(bc);
        }

       // public void input_bclist(buildCard bc)
       // {
       //     bclist_all.Add(bc);
       // }

        private void button2_Click(object sender, EventArgs e) //삭제한 버튼 이벤트의 잔재.
        {
            // inform = new information();
            inform.money = money.m;
            inform.ShowDialog();

        }

        public void card_sell(buildCard bc)
        {
            List<double> price_card = new List<double>();
            List<bool> build_list = Confirm_ALL_build(bc.color, bc.city_address);//카드의 건물들이 어느것이 있는지 확인. 0부터 땅, 빌라, 빌딩, 호텔 순. enum으로 정해놨음.
            for(int i = 0; i < 4; i++)
                price_card.Add(city[bc.color].price[i][bc.city_address]);
            if (build_list[(int)Build_Confirm.GROUND])
                money.m += (price_card[0]*0.7);
            if (build_list[(int)Build_Confirm.VILLA])
                money.m += price_card[1] * 0.7;
            if (build_list[(int)Build_Confirm.BUILDING])
                money.m += price_card[2] * 0.7;
            if (build_list[(int)Build_Confirm.HOTEL])
                money.m += price_card[3] * 0.7;
            MessageBox.Show(money.m.ToString());
            //건물들을 팔고 얻을 수 있는 돈을 추가.
            //건물들이 있던 위치를 원상태로 복구.
            bclist_all.Remove(bc);
        }
    }

}
