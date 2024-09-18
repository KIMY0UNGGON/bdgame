using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    internal class MySocket
    {
        Socket sever;
        byte[] receiverBuff;
        public void init()
        {
            sever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 서버에 연결
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
            sever.Connect(ep);

            //thread recv
            Thread recvTh = new Thread(new ThreadStart(recv));
            recvTh.Start();


            //// Q 를 누를 때까지 계속 Echo 실행
            //while ((cmd = Console.ReadLine()) != "Q")
            //{
            //    byte[] buff = Encoding.UTF8.GetBytes(cmd);

            //    // (3) 서버에 데이타 전송
            //    sever.Send(buff, SocketFlags.None);

            //    // (4) 서버에서 데이타 수신
            //   

            //    
            //    Console.WriteLine(data);
            //}

        }


        void recv()
        {

            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            if(gm == null) return;

            while (true)
            {
                try
                {
                    receiverBuff = new byte[8192];
                    int n = sever.Receive(receiverBuff);
                    string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                    gm.sendtomainthread(data);

                    //Thread se = new Thread(() => send(gm.Text));
                    //se.Start();
                }
                catch {
                    break;
                }
            }
            // (5) 소켓 닫기
            sever.Close();
        }

        public void send(string msg)
        {
            byte[] receiverBuff = Encoding.UTF8.GetBytes(msg);
            sever.Send(receiverBuff, 0, receiverBuff.Length, SocketFlags.None);
        }

    }
}
