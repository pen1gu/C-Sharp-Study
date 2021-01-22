using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerTest
{
    class Program
    {
        static List<Socket> connections = null;
        byte[] msgPacket = new byte[1024];
        static void Main(string[] args)
        {
            Socket sock = null;
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
                );//소켓 생성 

            connections = new List<Socket>();
            //인터페이스와 결합
            IPAddress addr = IPAddress.Any;
            IPEndPoint iep = new IPEndPoint(addr, 52217);
            sock.Bind(iep);

            //백로그 큐 크기 설정
            sock.Listen(10);
            Socket dosock;

            Console.WriteLine("연결을 기다리는 중입니다.");

            while (true)//AcceptLoop
            {
                dosock = sock.Accept();
                Console.WriteLine("연결되었습니다.");
                Thread t = new Thread(new ParameterizedThreadStart(DoIt));
                t.Start(dosock);
                connections.Add(dosock);
            }
        }
         
        public static void DoIt(Object socket)
        {
            Socket dosock = (Socket)socket;
            try
            {
                byte[] packet = new byte[1024];
                IPEndPoint iep = dosock.RemoteEndPoint as IPEndPoint;
                while (true)
                {
                    dosock.Receive(packet);
                    MemoryStream ms = new MemoryStream(packet);
                    BinaryReader br = new BinaryReader(ms);
                    string msg = br.ReadString();
                    br.Close();
                    ms.Close();

                    if (msg == "exit")
                    {
                        Console.WriteLine("{0}:{1} 님이 나가셨습니다.", iep.Address, iep.Port);
                        break;
                    }

                    Console.WriteLine("{0}:{1} → {2}", iep.Address, iep.Port, msg);
                    for(int i = 0; i < connections.Count; i++)
                    {
                        connections[i].Send(packet);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                dosock.Close();
            }
        }
    }
}
