using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace UdpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) UdpClient 객체 성성
            UdpClient cli = new UdpClient();

            string msg = Console.ReadLine();
            byte[] datagram = Encoding.UTF8.GetBytes(msg);

            string serverIp = "127.0.0.1";
            // (2) 데이타 송신
            cli.Send(datagram, datagram.Length, serverIp, 3000);
            WriteLine("[Send] {0} 로 {1} 바이트 전송", serverIp,datagram.Length);

            // (3) 데이타 수신
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = cli.Receive(ref epRemote);
            WriteLine("[Receive] {0} 로부터 '{1}' 수신", epRemote.ToString(), Encoding.UTF8.GetString (bytes));

            // (4) UdpClient 객체 닫기
            cli.Close();
        }
    }
}