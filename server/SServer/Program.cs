using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Program pr = new Program();
            pr.start();
        }
        Socket sock;
        static int count = 0; //현재 서버에 접속중인 클라이언트 수.
        SortedSet<string> ready_count = new SortedSet<string>(); //준비가 되었는지 확인하기 위한 집합.
        static int Thread_count = 1;
        Dictionary<int, Socket> clients= new Dictionary<int, Socket>(); //클라이언트 정보 저장. 들어온 순서대로 0번부터 시작.
        //Dictionary<Socket,string> loc = new Dictionary<Socket, string>(); //위치 저장
        //Dictionary<Socket, string> build = new Dictionary<Socket, string>(); //건물 저장
        List<string> Color_Token = new List<string>();
        Thread recive;
        static int Multy_count = 2;
        void start() { 
            // (1) 소켓 객체 생성 (TCP 소켓)
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 포트에 바인드
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 7000);
            sock.Bind(ep);

            // (3) 포트 Listening 시작
            sock.Listen(10);
            Thread thread = new Thread(new ThreadStart(Confirm_Start));
            thread.Start();
            
            while (true)
            {
                try
                {
                    Socket clientSock = sock.Accept();
                    clients.Add(count, clientSock);
                    //loc.Add(clientSock, "3|9");
                    //build.Add(clientSock, "");
                    Console.WriteLine(((IPEndPoint)clientSock.RemoteEndPoint).ToString()+"가 서버에 접속하였습니다.");

                    recive = new Thread(new ThreadStart(recv));//
                    recive.Start();

                

                }
                catch (Exception ex)
                {
                    break;
                }
            }
   
            

           // while (!Console.KeyAvailable) 
          //  {

           // }

            sock.Close();
        }


        bool Ready_flag = true;
        void recv()
        {

            byte[] buff = new byte[8192];
            int Client_Counts = count++;
            int color = -1;
            Socket clientSock = clients[Client_Counts];

            //clientSock.Send(Encoding.UTF8.GetBytes((Client_Counts+1).ToString()));// n= 0 : 파랑, n = 1: 검정 , n=2 : 빨강, n=3 : 회색
            Thread start = new Thread(new ThreadStart(() => pasing(clientSock, $"{Client_Counts + 1}/{Multy_count}")));
            start.Start();
            start.Join();
            if (Client_Counts == 0)
            {
                //clientSock.Send(Encoding.UTF8.GetBytes("TOKEN"));
                pasing(clientSock, "TOKEN");
            }
            string msg = "";
            bool turn = false;
            bool turnover = false;
            if (Client_Counts == 0)
            {
                turn = true; //처음 턴을 받는 말.
            }
            if(Color_Token.Count != 0 && Client_Counts == Color_Token.Count)
            {
                string tk = "TOKEN";
                foreach (var tk_s in Color_Token)
                {
                    tk += (tk_s + "/");
                }
                clientSock.Send(Encoding.UTF8.GetBytes(tk));
                Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : {tk}");
            }

            while (true)
            {
                try
                {

                    int len = clientSock.Receive(buff);
                    string data = Encoding.UTF8.GetString(buff, 0, len);
                    
                    Console.WriteLine(((IPEndPoint)clientSock.RemoteEndPoint).ToString() + ":" + data);

                    //msg = n.ToString() + "번";


                    if (data.StartsWith("T")) //토큰 색깔 전달 받았을 때.
                    {
                        //Console.WriteLine($"{Client_Counts}번 스레드가 받은 명령 : {data}");
                        string color_s = data.Split('T')[1]; //뒤의 숫자.
                        color = Convert.ToInt32(color_s); //현재 쓰레드의 색깔 할당.
                        Color_Token.Add(color_s); //토큰 색깔 추가.
                        if (clients.Count > Color_Token.Count) //클라이언트의 수와 토큰을 선택한 클라이언트의 수 비교.
                        {
                            string tk = "TOKEN";
                            foreach(var tk_s in Color_Token)
                            {
                                tk += (tk_s + "/");
                            }
                            pasing(clients[Color_Token.Count], tk);
                            Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : {tk}");
                        }
                    }
                    string[] sp = data.Split('/');

                    
                    if (data.Contains("NEXT"))
                    {
                        var co = Client_Counts + 1;
                        if (co == clients.Count)
                        {
                            co = 0;
                        }
                        pasing(clients[co], "YOUR TURN");
                        Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : YOUR TURN");
                    }
                    if (data.Equals("READY"))
                    {
                        //Ready_flag = false;
                        string message = "";
                        for(int i = 0; i< Color_Token.Count; i++)
                        {
                            if (i == Client_Counts)
                                continue;
                            message += $"{i}/{Color_Token[i]}<"; //클라이언트번호/색깔번호<순.
                        }
                        msg = $"info<{message}";

                    }
                    if (data.EndsWith("COMPLETE")) //집합 사용. complete를 불러온 클라이언트의 번호는 1~4로 4가지
                    {
                        ready_count.Add(sp[0]); //같은 클라이언트가 오류로 두번 이상 complete를 불러와도 카운트가 두번 쌓이지 않음.

                    }


                    if (sp[0].Equals("w")) //클라이언트가 다른 클라이언트의 정보를 원하는 경우
                    {
                        int want_num = Convert.ToInt32(sp[1]);
                        pasing(clients[want_num], data); //받은 메시지 그대로 넘김
                    }
                    if (sp[0].Equals("m")) //클라이언트가 움직였다고 알릴때.
                    {
                        Except_send($"l/{sp[1]}/{Client_Counts}",clientSock,Client_Counts); //l(위치)/{움직일 칸 수}/{움직인 클라 넘버}
                    }
                    if (sp[0].Equals("b")) //클라이언트가 건물을구매했을때.
                    {
                       // Except_send($"b/{}/{Client_Counts}", clientSock, Client_Counts) //b(건물 구매함)/{구매한 것}/{구매한 클라 넘버}


                        turn = true;
                        turnover = true;
                    }
                   
                    if (sp[0].Equals("te")) //플레이어간의 턴을 넘길때
                    {
                        turn = false;
                        pasing(clientSock, "END TURN");
                        Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : END TURN");
                        Console.WriteLine(Client_Counts.ToString() + " END TURN");
                        turnover = false;
                    }
                    if (sp[0].Equals("return")) //다른클라이언트에서 받은 정보를, 원하던 클라이언트에 넘김.
                    {
                        int return_num = Convert.ToInt32(sp[2]);
                        pasing(clients[return_num], data);
                    }

                    // (6) 소켓 송신
                    //thread
                    //pasing
                    Thread pathread = new Thread(() => { pasing(clientSock, msg);Console.WriteLine($"{Client_Counts}번 메소드의 명령 {msg}"); });
                    pathread.Start();
                    pathread.Join();
                    msg = "";


                }
                catch (Exception ex)
                {
                    break;
                }
            }
            clientSock.Close();
            clients.Remove(Client_Counts);

            Console.WriteLine((Client_Counts + 1).ToString()+"번 말이 나갔습니다.");
        }

        void Confirm_Start()
        {
            while (true)
            {
                if (Color_Token.Count == Multy_count && count == Multy_count) //마지막 클라이언트가 토큰을 결정했을 때.
                {
                    count = 0;
                    if (Ready_flag)
                    {
                        Ready_off();
                        All_send("READY");
                    }
                }
                if (ready_count.Count == Multy_count) //모두 준비 완료 시.
                {
                    ready_count.Clear();
                    All_send("GameStart");
                    break;
                }
            }
        }
        void pasing(Socket clientSock,string msg)
        {
            send(clientSock, Encoding.UTF8.GetBytes(msg));
            //thread.Start();
            //thread.Join();
            
            //Console.WriteLine($"{count}번 스레드가 보낸 명령 : " + msg);

        }
        void Ready_off()
        {
            Ready_flag = false;
        }
        void All_send( string msg, int Client_Counts = -1)
        {
            foreach (var socket in clients.Values)
            {
                pasing(socket, msg); //모든 클라이언트들에게 준비가 되었는지 물음.
                Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : {msg}");
            }
        }
        void Except_send(string msg, Socket Client, int Client_Counts = -1)
        {
            foreach (var socket in clients.Values)
            {
                if (socket != Client)
                {
                    pasing(socket, msg); //모든 클라이언트들에게 준비가 되었는지 물음.
                    Console.WriteLine($"{Client_Counts}번 스레드가 보낸 명령 : {msg}");
                }
            }
        }
        void send(Socket clientSock,byte[] buff)
        {
            //
            try
            {
                clientSock.Send(buff, 0, buff.Length, SocketFlags.None);  //echo
            }
            catch(Exception ex)
            {

            }
        }
    }
}
