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
        
        public GameMain()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }


        public double money_retur() //돈의 값을 받아옴. 주로 다른 FORM인 정보에 데이터를 전송할 때 사용.
        {
            return money.m;
        }


        

        private void Form1_Load(object sender, EventArgs e)
        {
            //GC.Collect();
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
           


            string Card_T = Properties.Resources.Card;
            foreach(var Card_text in Card_T.Split('\n')) //현재 내부에 저장되어있는 카드 정보를 읽어와서 리스트에 저장.
            {
                CardText.Add(Card_text);
           
            }


         


            if (Multi) //멀티모드
            {
                initPlace_Multy(); //멀티 모드에서의 토큰 위치 설정.
                init();
                //Players_Token.ForEach(x => city[3].play.Add(x));
               
            }
            else
            {
                Select_Token Token = new Select_Token();

                Token.ShowDialog();
                Place_Solo();
                init();
                
                //city[3].play.Add(Players_Token.First()); //마지막 구역. 보드판에서는 시작 구역에 플레이어 말을 배치.
              
            }
      
            BCCard_init();

            //playerimage();
            image(); //황금 카드의 좌표 및 주사위의 크기 및 좌표
            rectcity(); // 구매하면 건설될 건물들의 크기 및 좌표 지정.
            Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel); //카드 이미지 그림.
            city[3].updateAll(); //모든 말들을 다시 그림.


            Invalidate();
            button1.Visible = true;
            timer3.Start();

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



        private void bu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butt_solo_Click_1(object sender, EventArgs e)
        {
            butt_solo.Visible = false;
            butt_multi.Visible = false;
            start_confirm = true;
            Multy_Num = 1;
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
                    if (nowblock > 0) nowblock -= 1;
                    else nowblock = 3;
                }
                nowcity -= 1;
                if(nowcity < 0)
                {
                    nowcity += 10;
                }

                if (nowblock == 3 && nowcity == 9) money.m += 20;
                city[bfblock].play[Multy_Num - 1].Visible = false; //지움.
                
                
                reset(bfblock,bfcity);
                city[nowblock].play[Multy_Num - 1].Visible = true;
                city[nowblock].update(Multy_Num - 1);
                Delay(100);
                
                Invalidate();
                bfblock = nowblock;
                bfcity = nowcity;
            }
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
