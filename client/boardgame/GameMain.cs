using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;
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

        Bitmap bit, map, key, nameBt,Arch;
        Graphics DC, dices, Card, nameGp, Arch_GP;
        Bitmap  dice1, cardg, card_red;
        //Area area;
        cityArea[] city = new cityArea[4]; //cityArea 들의 리스트 도시들의 구역들을 지정하고 그리기 위한 클래스 들임.


        Bitmap areacard, areacard1, areacard2, areacard3;
        Bitmap social;
        public List<buildCard> bclist_infor = new List<buildCard>();
        List<buildCard> bclist_all = new List<buildCard>(); //건물 카드들이 들어있는 리스트

        static int Multy_Count { get; set; } = 2;
        public int island { get; set; } = 0; //무인도에 들어가있는 턴

        public bool Uninhabited { get; set; } = false; //무인도에 있는지

        public List<Rectangle> Dicelist = new List<Rectangle>();
        public List<Rectangle> CardList = new List<Rectangle>();
        public List<String> CardText = new List<String>();
        List<Player>[] players = new List<Player>[4];
        List<Player>[][] Multy_Token = new List<Player>[Multy_Count][]; //[][][] 각각 클라이언트 번호, 구역, 블럭 순.
        List<Button> buttonTravel = new List<Button>();

        bool Carddr = false;
        int Multy_Num { get; set; } = -1; //현재 이 클라이언트의 번호를 나타내는 변수.
        Money money = new Money();
        bool Multy_Wait = true;

        public double money_retur() //돈의 값을 받아옴. 주로 다른 FORM인 정보에 데이터를 전송할 때 사용.
        {
            return money.m;
        }

        public int nowcity { get; set; } = 9;//현재 구역의 도시 번호
        public int nowblock { get; set; } = 3; //현재 구역의 번호.
        int bfcity { get; set; } = 9; //전의 구역의 도시 번호
        int bfblock { get; set; } = 3; //전에 있던 구역의 번호.

        List<int> Nowcity_List = new List<int>();
        List<int> Nowblock_List = new List<int>();
        List<int> Before_city = new List<int>();
        List<int> Before_block = new List<int>();

        public int cardnum;
        public int[] buildnum = new int[36];
        Rectangle[][] playerrect = new Rectangle[4][];
        Rectangle[][][] buildrect = new Rectangle[4][][];

        public int money_so { get; set; } = 100;
        Rectangle[][] buyground = new Rectangle[4][];


        information inform = new information();

        public List<string>[] name = new List<string>[4];

        int skip = 0;

        MySocket server;  //서버 연결시 사용하는 소켓



        public bool b_building, b_villa, b_ground, b_hotel; //현재 땅에 어떤 건물이 지어졌는지 확인하는 bool 변수.


        int color_num { get; set; } = -1; //토큰의 색. 0번 하늘색, 1번 검정색, 2번 빨간색, 3번 회색
        List<Bitmap> player_1 = new List<Bitmap>(); //플레이어 말의 가로.
        List<Bitmap> player_2 = new List<Bitmap>(); //플레이어 말의 세로
        List<Bitmap>[] MultyPlayers = new List<Bitmap>[Multy_Count]; //플레이어들의 비트맵을 저장.

        public Point[] C = { new Point(500, 300), new Point(400, 200), new Point(300, 500) }; //카드의 크기.
        bool card_clicked = false;


        public List<Card> cards = new List<Card>(); //카드들이 들어있는 리스트.

       

        int hotel = 0;
        int building = 0;
        int bul = 0;

        bool start_confirm = false; //게임이 시작을 했는지 확인하는변수.

        int loopconfirm = 0; //  보드를 몇바퀴 돌았는 확인하는 변수.
        enum Build_Confirm { GROUND, VILLA, BUILDING, HOTEL };

        private void BCCard_init()
        {
            for(int i = 0; i< name.Length;  i++)
            {
                name[i] = new List<string>();
            }
            string Card_Text = Properties.Resources.build;
            int count = 0;
            foreach (var Text in Card_Text.Split('\n'))
            {
                string[] txtSplit = Text.Split(','); //도시 이름
                name[count].Add(txtSplit[0]);
                if (count != 3 && name[count].Count == 7)
                    count++;                
                else if (count == 3 && name[count].Count == 8)
                    count++;
            }

        }


        //private void ground() //구매 가능한 땅에 대해 구역을 지정해 배열로서 표현.
        //                      //구매 가능한 땅에 지어질 수 있는 건물의 사각형의 크기를 지정함.
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (i == 2)
        //        {
        //            buyground[i] = new Rectangle[7];
        //        }
        //        else
        //        {
        //            buyground[i] = new Rectangle[8];
        //        }
        //        for (int j = 0; j < 8; j++)
        //        {
        //            if (i == 2)
        //            {
        //                if (j == 1)
        //                {
        //                    continue;
        //                }
        //                buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);

        //            }
        //            else
        //            {
        //                if (i < 3)
        //                {
        //                    if (j == 1 || j == 6)
        //                    {
        //                        continue;
        //                    }

        //                }
        //                else
        //                {
        //                    if (j == 4 || j == 7)
        //                    {
        //                        continue;
        //                    }
        //                }
        //                buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);
        //            }
        //        }
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            GC.Collect();
            // Server_connect(); //서버와 연결. 다른 말들의 정보를 받기 위함.
            initDC(); //그래픽들을 할당해 주는 메소드
            initImage(); //Token들의 이미지를 가져와 리스트에 저장.
            //Form_show();
            this.Size = new Size(300, 200);


        }
        private void Turn_Thread()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => Turn_Thread()));
                return;
            }
            //메인쓰레드
            Confirm_Turn(); //서버에서 값을 받아 메인 폼에 적용하거나 서버에 다시 값을 보냄
        }
        private void Confirm_Turn()
        {
        
                if (Turn)//자신의 턴이 돌아왔는지 확인하는 용도.
                    button1.Enabled = true;
                else
                    button1.Enabled = false;
            

        }
        private void Form_show() //게임 화면을 그림.
        {
            this.Size = new Size(cityArea.side - 1, cityArea.down - 1); //폼의 크기를 보드의 크기만큼 변환.
            init();
            BCCard_init();

            //playerimage();
            image(); //황금 카드의 좌표 및 주사위의 크기 및 좌표
            rectcity(); // 구매하면 건설될 건물들의 크기 및 좌표 지정.


            string Card_T = Properties.Resources.Card;
            foreach(var Card_text in Card_T.Split('\n')) //현재 내부에 저장되어있는 카드 정보를 읽어와서 리스트에 저장.
            {
                CardText.Add(Card_text);
           
            }


            Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel); //카드 이미지 그림.


            if (Multi) //멀티모드
            {
                Place_Multy(); //멀티 모드에서의 토큰 위치 설정.
                //List<Player> Enemy = new List<Player>(); //
                for (int i = 0; i < Multy_Count; i++)
                {
                    city[3].play[9].Add(Multy_Token[i][3][9]);
                    city[3].update(i, nowcity); //모든 말들을 다시 그림.
                }
                //Thread Tes = new Thread(new ThreadStart(Turn_Thread));
                //Tes.Start();
            }
            else
            {
                Place_Solo();

                Player player = players[3][9]; //플레이어 말의 위치 정보를 처음 시작하는 구간으로 저장. 첫번째 배열은 도시들의 구역. 두번째 배열은 도시들의 순서.

                city[3].play[9].Add(player); //마지막 구역. 보드판에서는 시작 구역에 플레이어 말을 배치.
                Select_Token Token = new Select_Token();
                Token.Show();
                city[3].update(nowcity); //도시 구역들을 업데이트하여 수정된 정보들을 다시 그림.
            }
            
            Invalidate();
            button1.Visible = true;
            timer3.Start();
        //    Thread thread = new Thread(new ThreadStart(test_thread));
        //    Thread thread1 = new Thread(new ThreadStart(Thread_CardRed));
        //    thread1.IsBackground = true;
        //    thread.IsBackground = true;
        //    thread.Start();
        //    thread1.Start();
        }





        public void FORM_Close()
        {
            this.Close();
        }
        private void GameMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FORM_Close();
            //sock.Close();
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals("boardgame"))
                {
                    process.Kill();
                }
            }
        }

        bool Multi = false;
        private void butt_multi_Click(object sender, EventArgs e)
        {
            butt_solo.Visible = false;
            butt_multi.Visible = false;
            start_confirm = false;
            Multi = true;
            //Thread Turn_thread = new Thread(new ThreadStart(() => Turn_Thread()));
            //Turn_thread.Start();
          
            initMultyImage();
            Server_connect();
           
        }

        bool Turn = false;


        private void bu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butt_solo_Click_1(object sender, EventArgs e)
        {
            butt_solo.Visible = false;
            butt_multi.Visible = false;
            start_confirm = true;
            initMultyImage();
            Form_show();
        }





        private static DateTime Delay(int MS)

        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (true)

            {

               

                ThisMoment = DateTime.Now;
                System.Windows.Forms.Application.DoEvents();
                //Application.
                if (AfterWards <= ThisMoment)
                    break;

            }

            return DateTime.Now;

        }
        private static async Task DoSomethingAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
            //Console.WriteLine($"Task completed after {milliseconds} milliseconds");
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
                city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]); //지움.
                
                
                reset(bfblock,bfcity);
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
