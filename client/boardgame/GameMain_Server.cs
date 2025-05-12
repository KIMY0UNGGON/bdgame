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
        //int number = -1; //현재 클라이언트넘버
      

        private void Server_connect()
        {
            server = new MySocket();
            server.init();
        }

        public void sendtomainthread(string data) //서버에서 보내온 값을 받아 쓰레드를 계속해서 실행(멀티쓰레드)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => sendtomainthread(data)));
                return;
            }
            //메인쓰레드
            Implement_Data(data); //서버에서 값을 받아 메인 폼에 적용하거나 서버에 다시 값을 보냄
        }

        private void Token(string[] str)
        {
            Select_Token Token = new Select_Token();
            Token.Confirm_Token(str);
            Token.Show();
        }

        private void Implement_Data(string data) //서버의 데이터를 받아 값에 따라 send와 receive 값을 정해 실행
        {
            //UI 사용 가능
            //받은 값의 첫번째 값에 따라 실행하는 명령이 달라지게 할 예정.
            //m 이면 움직임과 관련 (이때 상대방이 움직였으면 바로 다음플레이어에게 턴이 넘어가도록 변경.), s면 건물의 판매, b면 구매.

            //들어온 순서에 따라 서버에서 클라이언트에 번호를 지정해줌. 그 지정한 번호에 따라 자신의 번호를 변경하고 값을 받았음을 서버에 다시 알림.
            //현재는 색깔이 번호에 따라 지정되어있지만 선착순으로 선택하도록 할 예정. 즉 번호를 지정받고 말을 선택하는 폼을 띄운 뒤 플레이어가 선택하면 서버에 전송하는 방식.
            //자신 보다 먼저온 플레이어가 말을 선택하지 않았으면 다른 사람들은 선택을 하지 못하도록 서버에서 통제.
            string[] split_data = data.Split('/');
            
            if (split_data[0].Equals("1"))
            {
                Multy_Count = Convert.ToInt32(split_data[1]);
                Multy_Num = Convert.ToInt32(split_data[0]);
                this.Text = split_data[0] + "번 말";
                Turn = true;
            }
            if (split_data[0].Equals("2"))
            {
                Multy_Count = Convert.ToInt32(split_data[1]);
                Multy_Num = Convert.ToInt32(split_data[0]);
                this.Text = split_data[0] + "번 말";
            }
            if (split_data[0].Equals("3"))
            {
                Multy_Count = Convert.ToInt32(split_data[1]);
                Multy_Num = Convert.ToInt32(split_data[0]);
                this.Text = split_data[0] + "번 말";

            }
            if (split_data[0].Equals("4"))
            {
                Multy_Count = Convert.ToInt32(split_data[1]);
                Multy_Num = Convert.ToInt32(split_data[0]);
                this.Text = split_data[0] + "번 말";
               

            }
            if (data.Equals("READY"))
            {
                if (Multy_Num > -1) //클라이언트의 넘버를 부여받았을 경우에만 서버에 전송. 보통은 접속과 동시에 선착순으로 번호를 부여받음.
                {
                    server.Thread_send("READY"); //준비가 되었다고 서버에 다시 전송.
                }
            }
            if (data.StartsWith("info"))
            {
                //클라이언트의 넘버와 컬러를 전송받음.
                string[] data_split = data.Split('<'); //0번째와 마지막은 의미없는 내용.
                for(int i = 1; i< data_split.Length-1; i++)
                {
                    string[] split_2 = data_split[i].Split('/');
                    int Token_Num = Convert.ToInt32(split_2[0]);
                    int Token_Color = Convert.ToInt32(split_2[1]);
                    Multy_init(Token_Num, Token_Color); //전달 받은 클라이언트의 넘버와 컬러를 Multy_init에 넘겨 해당 클라이언트의 번호에 지정된 색깔을 지정.               
                }
                if (Confirm_AllReady())
                {
                    server.Thread_send($"{Multy_Num}/COMPLETE"); //현재 이 클라이언트의 준비가 끝났다고 서버에 전송.
                }
            }
            if (data.Equals("GameStart"))
            {
                
                Multy_Wait = false;
                Form_show(); //모두 준비 되었을때 폼 그림.
            }
            if (data.StartsWith("TOKEN"))
            {
                string tk = data.Split(new string[] {"TOKEN"} , StringSplitOptions.None)[1];
                string[] tk_str = tk.Split('/');
                Token(tk_str);
            }

            // 자신의 턴이 끝났을 때 서버에 턴이 끝났음을 알림.
            if (data.Equals("END TURN"))
            {
                Turn = false;
                server.Thread_send("NEXT");
            }
            //서버가 내 턴이라고 메시지를 보내오면 자신의 턴으로 변경.
            if (data.Equals("YOUR TURN"))
            {
                Turn = true;
            }

            if (split_data[0].Equals("w")){ //다른 클라이언트가 정보를 원한다고 서버에서 알림.
                server.send($"return/{money_retur()}/{split_data[2]}"); //현재 이 클라이언트의 돈의 정보를 split_data[2]에 전달.

            }
            if (split_data[0].Equals("return")) //다른 클라이언트의 정보를 받아옴.
            {
                information info = new information();
                info.Multi = true;
                info.money = Convert.ToInt32(split_data[1]);
                info.Show();
            }
            //게임 도중 건물을 구매했을 경우 내가 구매한 구역의 좌표를 보내고 다른 플레이어가 구매를 하였으면 그 좌표와 얼마나 지었는지를 서버에서 받아 자신의 메인 폼에 적용.
            if (data.Contains("b"))
            {
                string[] data_build = data.Split(new char[] { '/' }); // 토큰분리
                int num = Convert.ToInt32(data_build.Last()); //클라 번호

                other_build(data_build, num);

                //추가 및 받는 값 변경필요.
            }
            if (data.StartsWith("l")) // 다른 클라이언트의 위치 받음. 현재 이동 메소드를 전체적으로 변경하여 변경필요.
            {
                string[] data_split = data.Split(new char[] { '/' }); //데이터를 '/' 단위로 쪼갬
                int move = Int32.Parse(data_split[1]); //이동할 칸수
                int num = Int32.Parse(data_split[2]); //클라이언트 번호
                Move_OtherToken(move, num);
                //dataspl[0] 말 번호
                //dataspl[1] 위치 주사위 수
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

