using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class GameMain
    {
        //Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int number = -1;
        byte[] receiverBuff = new byte[8192];




        private void Server_connect()
        {
            server = new MySocket();
            server.init();

            /*
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
            sock.BeginConnect(ep,new AsyncCallback(ConnectCallback),sock);


       
            int n = sock.Receive(receiverBuff);

            string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
            this.Text = data + "번 말";
            number = Int32.Parse(data);

            if (data.Equals("1"))
                BLUE = true;
            if (data.Equals("2"))
                BLACK = true;
            if (data.Equals("3"))
                RED = true;
            if (data.Equals("4"))
                GRAY = true;


            
            */





        }

        public void sendtomainthread(string data) //서버에서 보내온 값을 받아 쓰레드를 계속해서 실행(멀티쓰레드)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => sendtomainthread(data)));
                return;
            }
            //메인쓰레드
            datadta(data); //서버에서 값을 받아 메인 폼에 적용하거나 서버에 다시 값을 보냄
        }



        private void datadta(string data) //서버의 데이터를 받아 값에 따라 send와 receive 값을 정해 실행
        {
            //UI 사용 가능
            //받은 값의 첫번째 값에 따라 실행하는 명령이 달라지게 할 예정.
            //m 이면 움직임과 관련 (이때 상대방이 움직였으면 바로 다음플레이어에게 턴이 넘어가도록 변경.), s면 건물의 판매, b면 구매.

            //들어온 순서에 따라 서버에서 클라이언트에 번호를 지정해줌. 그 지정한 번호에 따라 자신의 번호를 변경하고 값을 받았음을 서버에 다시 알림.
            //현재는 색깔이 번호에 따라 지정되어있지만 선착순으로 선택하도록 할 예정. 즉 번호를 지정받고 말을 선택하는 폼을 띄운 뒤 플레이어가 선택하면 서버에 전송하는 방식.
            //자신 보다 먼저온 플레이어가 말을 선택하지 않았으면 다른 사람들은 선택을 하지 못하도록 서버에서 통제.
            if (data.Equals("1"))
            {
                BLUE = true;
                this.Text = data + "번 말";
                server.send("1번");
                Turn = true;
                // MessageBox.Show("당신은 " + this.Text + "입니다.");
            }
            if (data.Equals("2"))
            {
                BLACK = true;
                this.Text = data + "번 말";
                server.send("2번");
                //MessageBox.Show("Test");

            }
            if (data.Equals("3"))
            {
                RED = true;
                this.Text = data + "번 말";
                //server.send("3번");
            }
            if (data.Equals("4"))
            {
                GRAY = true;
                this.Text = data + "번 말";
                //server.send("4번");

            }



            // 자신의 턴이 끝났을 때 서버에 턴이 끝났음을 알림.
            if (data.Equals("END TURN"))
            {
                Turn = false;
                server.send("NEXT");
            }
            //서버가 내 턴이라고 메시지를 보내오면 자신의 턴으로 변경.
            if (data.Equals("YOUR TURN"))
            {
                Turn = true;
            }

            //서버가 게임을 시작하라고 하면 게임화면을 변경.
            if (data.Equals("GameStart"))
            {
                Form_show();
            }

            //게임 도중 건물을 구매했을 경우 내가 구매한 구역의 좌표를 보내고 다른 플레이어가 구매를 하였으면 그 좌표와 얼마나 지었는지를 서버에서 받아 자신의 메인 폼에 적용.
            if (data.Contains("bui"))
            {
                //추가 및 받는 값 변경필요.
            }
            if (data.Contains("loc")) // 다른 클라이언트의 위치 받음. 현재 이동 메소드를 전체적으로 변경하여 변경필요.
            {
                string[] dataspl = data.Split(new char[] { ' ' });
                string[] numspl = dataspl[0].Split(new string[] { "번" }, StringSplitOptions.None);
                int num_cl = Int32.Parse(numspl[0]); // 말번호
                string[] posspl = dataspl[1].Split(new string[] { "loc" }, StringSplitOptions.None);
                string[] posspl2 = posspl[1].Split(new string[] { "|" }, StringSplitOptions.None); //block , city, dice
                int bl = Int32.Parse(posspl2[0]);
                int ct = Int32.Parse(posspl2[1]);
                int dc = Int32.Parse(posspl2[2]);
                move_otherpl(ct, bl, dc, num_cl);
                //dataspl[0] 말 번호
                //dataspl[1] 위치 주사위 수
            }

        }

        private void move_otherpl(int city1, int block, int dice, int num)// 다른 클라이언트의 플레이어 움직이기
        {
            int movecity = city1 + dice;
            int befbl = block;
            int befct = city1;
            for (int i = city1; i <= movecity; i++) //말이 있는 칸부터 주사위 숫자를 더한 칸  
            {
                if (i > 9) // 구역 넘어감
                {
                    i = 0; movecity -= 10;
                    if (block < 3) block++;
                    else block = 0;
                }
                city[befbl].play[befct].Remove(players[befbl][befct]);
                reset();
                city[block].play[i].Add(players[block][i]);
                city[block].other_update(block, num);
                Invalidate();
                Delay(100);
                bfcity = i;
                befbl = block;
            }
        }


            public void move_write(int dice1) //얼마만큼 움직였는지 서버에 전송. 다른 클라이언트들이 this 플레이어의 정보를 확인하기 위한 용도.
            {
            
                try
                {
                    server.send($"m{dice1}");
                }
                catch (Exception e)
                {

                }
            
            }


            private void write_build()
            {
                string msg = "";
                if (confirm_ground(nowblock, nowcity))
                {
                    msg += "Grd ";
                }
                if (confirm_Villa(nowblock, nowcity))
                {
                    msg += "Villa ";
                }
                if (confirm_Building(nowblock, nowcity))
                {
                    msg += "Bld ";
                }
                if (confirm_Hotel(nowblock, nowcity))
                {
                    msg += "Hotel";
                }
                try
                {
                    server.send("build/ " + msg);
                }
                catch (Exception e)
                {
                    // MessageBox.Show("서버와의 연결이 끊어졌습니다.");
                }
            }

            //private void next_turn()
            //{
            //try
            //{
            //int n = sock.Receive(receiverBuff);
            //string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
            //if (data.Equals("Your Turn"))
            //{
            //    Turn = true;
            // }
            //sock.Send(Encoding.UTF8.GetBytes("NEXT"));
            //}
            //catch(Exception e)
            //{

            //}
            //}
            //TcpClient server;

            //private void Server_connect()
            //{
            //    try
            //    {
            //        server = new TcpClient("127.0.0.1", 36666);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("서버와의 연결이 끊어졌습니다.");
            //    }
            //}
            //private void write_build()
            //{
            //    string msg = "";
            //    if (confirm_ground(nowblock, nowcity))
            //    {
            //        msg += "Grd ";
            //    }
            //    if (confirm_Villa(nowblock, nowcity))
            //    {
            //        msg += "Villa ";
            //    }
            //    if (confirm_Building(nowblock, nowcity))
            //    {
            //        msg += "Bld ";
            //    }
            //    if (confirm_Hotel(nowblock, nowcity))
            //    {
            //        msg += "Hotel";
            //    }
            //    try
            //    {
            //        NetworkStream networkStream = server.GetStream();
            //        StreamWriter writer = new StreamWriter(networkStream);
            //        writer.WriteLine("build/" + msg);
            //        writer.Flush();
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show("서버와의 연결이 끊어졌습니다.");
            //    }
            //}
            //private void move_write()
            //{
            //    try
            //    {
            //        NetworkStream networkStream = server.GetStream();
            //        StreamWriter writer = new StreamWriter(networkStream);
            //        writer.WriteLine("move/" + nowblock + "|" + nowcity);
            //        writer.Flush();
            //    }
            //    catch (Exception e)
            //    {

            //    }
            //}
        }
    }

