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
        static int count = 0;
        Dictionary<int, Socket> clients= new Dictionary<int, Socket>();
        Dictionary<Socket,string> loc = new Dictionary<Socket, string>();
        Dictionary<Socket, string> build = new Dictionary<Socket, string>();
        Thread recive;
        void start() { 
            // (1) 소켓 객체 생성 (TCP 소켓)
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 포트에 바인드
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 7000);
            sock.Bind(ep);

            // (3) 포트 Listening 시작
            sock.Listen(10);

            while (true)
            {
                try
                {
                    Socket clientSock = sock.Accept();
                    clients.Add(count, clientSock);
                    loc.Add(clientSock, "3|9");
                    build.Add(clientSock, "");
                    Console.WriteLine(((IPEndPoint)clientSock.RemoteEndPoint).ToString());

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

        void recv()
        {

            byte[] buff = new byte[8192];
            int n = count++;
            Socket clientSock = clients[n];

            clientSock.Send(Encoding.UTF8.GetBytes((n+1).ToString()));// n= 0 : 파랑, n = 1: 검정 , n=2 : 빨강, n=3 : 회색
            string msg = n.ToString() + "번";
            bool turn = false;
            bool turnover = false;
            if (n == 0)
            {
                turn = true;
            }



            while (true)
            {
                try
                {
                    int len = clientSock.Receive(buff);
                    string data = Encoding.UTF8.GetString(buff, 0, len);

                    Console.WriteLine(((IPEndPoint)clientSock.RemoteEndPoint).ToString() + ":" + data);


                    if (n == 1 && data.Equals("2번"))
                    {
                        byte[] bt = Encoding.UTF8.GetBytes("GameStart");
                        foreach (var sock in clients.Values)
                            sock.Send(bt, 0, bt.Length, SocketFlags.None);

                    }

                      
                    //Console.WriteLine(data);
                    string[] sp = data.Split('/');
                    if (sp[0].Equals("move"))
                    {
                        loc[clientSock] = sp[1];
                        //Console.WriteLine((n + 1).ToString()+"번말 loc" +sp[1]);
                        msg += " loc" + sp[1];
                        Console.WriteLine(msg + " 확인용");
                        turn = true;
                        turnover = true;
                    }
                    if (sp[0].Equals("build"))
                    {
                        build[clientSock] = sp[1];
                       
                        msg += " bui" + sp[1];
                        Console.WriteLine(msg +" 확인용");

                        turn = true;
                        turnover = true;
                    }
                    if (data.Contains("NEXT"))
                    {
                        
                        var co = n+1;
                        if(co == clients.Count)
                        {
                            co = 0;
                        }
                        pasing(clients[co], "YOUR TURN");
                              
                      

                    }
                    foreach(var sock in clients.Values)
                    {
                        if (!clientSock.Equals(sock))
                        {
                            pasing(sock, msg);
                        }
                    }
                    if (turn && turnover)
                    {
                        turn = false;
                        pasing(clientSock, "END TURN");
                        Console.WriteLine(n.ToString() + " END TURN");
                        turnover = false;
                    }

                    // (6) 소켓 송신
                    //thread
                    //pasing
                    Thread pathread = new Thread(() => pasing(clientSock, msg));
                    pathread.Start();
                    msg = n.ToString() + "번";
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            clientSock.Close();
            clients.Remove(n);
            //count--;
            Console.WriteLine((n + 1).ToString()+"번 말이 나갔습니다.");
          //  count--;
           
        }


        void pasing(Socket clientSock,string msg)
        {
            send(clientSock, Encoding.UTF8.GetBytes(msg));
        }


        void send(Socket clientSock,byte[] buff)
        {
            //
            try
            {
                clientSock.Send(buff, 0, buff.Length, SocketFlags.None);  // echo
            }
            catch(Exception ex)
            {

            }
        }
    }
}
