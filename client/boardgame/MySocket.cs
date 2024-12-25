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
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000); //127.0.0.1 포트 7000에 접속하기 위해 인스턴스 생성.
            sever.Connect(ep); //인스턴스의 생성자로 입력한 정보들로 서버에 접속 요청.

            //thread recv
            Thread recvTh = new Thread(new ThreadStart(recv)); //서버에서 오는 메시지를 받아 확인하기 위해 쓰레드 생성
            recvTh.Start(); //쓰레드를 시작

        }


        void recv() //서버의 메시지 값을 받는 메소드
        {

            GameMain gm = Application.OpenForms["GameMain"] as GameMain; //현재 열려있는 메인 폼의 인스턴스를 참조.
            if(gm == null) return; //만약 켜져있는 Gamemain 폼이 없으면 메소드 종료

            while (true) //값을 게속 받도록 대기.
            {
                try //에러 발생을 대비해 try-catch사용
                {
                    receiverBuff = new byte[8192]; //서버가 보내오는 값을 받기위한 byte변수.
                    int n = sever.Receive(receiverBuff); //받은 값을 저장하고 그 길이를 n에 저장.
                    string data = Encoding.UTF8.GetString(receiverBuff, 0, n); //받은 byte값과 n을 사용하여 문자열로 인코딩.
                    gm.sendtomainthread(data);   //메인쓰레드에 전송

                }
                catch {
                    break;
                }
            }
            // (5) 소켓 닫기
            sever.Close(); //값을 받는 것이 완벽하게 끝나면 소켓통신종료
        }

        public void send(string msg) //클라이언트에서 서버로 메시지를 보내기 위한 메소드
        {
            byte[] receiverBuff = Encoding.UTF8.GetBytes(msg); //보낼 메시지를 바이트로 변환
            sever.Send(receiverBuff, 0, receiverBuff.Length, SocketFlags.None); //받은 값과 길이를 서버에 전송.
        }

    }
}
