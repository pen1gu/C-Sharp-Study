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
        static Socket sock;
        static void Main(string[] args)
        {
            sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );//소켓 생성
            //인터페이스 결합(옵션)
            //연결
            IPAddress addr = IPAddress.Parse("192.168.56.1");
            IPEndPoint iep = new IPEndPoint(addr, 3000);
            sock.Connect(iep);
            string str;
            byte[] packet = new byte[1024];
            
            string sendString = string.Empty;
            byte[] bytes = new byte[1024];

            Thread thread = new Thread(()=>receiveMsg());
            thread.Start();

            while (true)
            {
                str = Console.ReadLine();
                MemoryStream ms = new MemoryStream(packet);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(str);

                bw.Close();
                ms.Close();
                sock.Send(packet);

                if (str == "exit")
                {
                    break;
                }


            }
            sock.Close();//소켓 닫기
        }
        public static void receiveMsg()
        {
            while (true)
            {
                byte[] packet2 = new byte[1024];
                string str2 = null;

                sock.Receive(packet2);

                MemoryStream ms2 = new MemoryStream(packet2);
                BinaryReader br = new BinaryReader(ms2);
                str2 = br.ReadString();
                Console.WriteLine("수신한 메시지:{0}", str2);
                br.Close();
                ms2.Close();
            }
        }
    }
}
