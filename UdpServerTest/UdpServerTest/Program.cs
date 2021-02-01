using System;
using System.Net;
using System.Net.Sockets;

namespace UdpSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) UdpClient 객체 성성. 포트 7777 에서 Listening
            UdpClient srv = new UdpClient(3000);

            // 클라이언트 IP를 담을 변수
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("연결을 기다리고 있습니다.");

            while (true)
            {
                // (2) 데이타 수신
                byte[] dgram = srv.Receive(ref remoteEP);
                Console.WriteLine("[Receive] {0} 로부터 {1} 바이트 수신", remoteEP.ToString(), dgram.Length);

                
                // (3) 데이타 송신
                srv.Send(dgram, dgram.Length, remoteEP);
                /*Console.WriteLine("[Send] {0} 로 {1} 바이트 송신", remoteEP.ToString(), dgram.Length);*/
                Console.WriteLine(System.Text.Encoding.Default.GetString(dgram));
            }
        }
    }
}