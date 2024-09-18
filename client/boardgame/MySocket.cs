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
