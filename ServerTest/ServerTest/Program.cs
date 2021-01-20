using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) 소켓 객체 생성 (TCP 소켓)
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 포트에 바인드
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 7000);
            sock.Bind(ep);

            // (3) 포트 Listening 시작
            
            sock.Listen(10);
            Socket clientSock = sock.Accept();
            while (true)
            {
                Console.WriteLine("연결을 기다리는 중..");
                // (4) 연결을 받아들여 새 소켓 생성 (하나의 연결만 받아들임)
            }

            Console.WriteLine("연결되었습니다.");

            byte[] buff = new byte[8192];

            while (!Console.KeyAvailable) // 키 누르면 종료
            {
                // (5) 소켓 수신
                int n = clientSock.Receive(buff);

                string data = Encoding.UTF8.GetString(buff, 0, n);
                Console.WriteLine(data);

                // (6) 소켓 송신
                clientSock.Send(buff, 0, n, SocketFlags.None);  // echo
            }

            // (7) 소켓 닫기
            clientSock.Close();
            sock.Close();
        }
    }
}
