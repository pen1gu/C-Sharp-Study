using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientTest
{
    class Program
    {
        static bool checkEnd = true;
        static Socket socket;
        static void Main(string[] args)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );//소켓 생성
            //인터페이스 결합(옵션)
            //연결
            IPAddress addr = IPAddress.Parse("221.143.21.37");
            IPEndPoint iep = new IPEndPoint(addr, 52217);
            socket.Connect(iep);
            string str;
            byte[] packet = new byte[1024];

            string sendString = string.Empty;
            byte[] bytes = new byte[1024];

            Thread thread = new Thread(() => receiveMsg());
            thread.Start();

            while (true)
            {
                str = Console.ReadLine();
                var sendBytes = Encoding.UTF8.GetBytes(str);

                socket.Send(sendBytes);

                if (str == "exit")
                {
                    /*checkEnd = false;*/
                    break;
                }
            }
            socket.Close();//소켓 닫기
        }
        public static void receiveMsg()
        {
            while (true)
            {
                byte[] packet = new byte[1024];

                /*if (checkEnd == false) return;*/

                var recieveCount = socket.Receive(packet);

                var str = Encoding.UTF8.GetString(packet, 0, recieveCount);

                Console.WriteLine("수신한 메시지:{0}", str);
            }
        }
    }
}
