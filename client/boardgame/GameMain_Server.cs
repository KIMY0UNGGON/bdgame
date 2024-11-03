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

        public void sendtomainthread(string data)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => sendtomainthread(data)));
                return;
            }
            //메인쓰레드
            datadta(data);
        }



        private void datadta(string data)
        {
            //UI 사용 가능

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
            if (data.Equals("END TURN"))
            {
                Turn = false;
                server.send("NEXT");
            }
            if (data.Equals("YOUR TURN"))
            {
                Turn = true;
            }
            if (data.Equals("GameStart"))
            {
                Form_show();
            }
            if (data.Contains("bui"))
            {

            }
            if (data.Contains("loc")) // 다른 클라이언트의 위치 받음
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

