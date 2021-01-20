using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineTetris
{
    public partial class ServerForm : Form
    {
        Socket clientSocket = null;
        Socket socket = null;
        public ServerForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void BtnConnect_Click(object sender, EventArgs e) // 줌 처럼 학생 받듯이 사용
        {
            
            try {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 7000);

                socket.Bind(ep);
                socket.Listen(10);
                clientSocket = socket.Accept();
            }
            catch (Exception ex) {
                TxtClientLog.AppendText(ex.Message);
            }
        }


        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // 보류
            try
            {
                byte[] buff = new byte[8192];
                int n = clientSocket.Receive(buff);
                string data = Encoding.UTF8.GetString(buff, 0, n);
                TxtClientLog.AppendText(data);
                
            }
            catch (Exception exception)
            {
                TxtClientLog.AppendText(exception.Message + "\n");
            }
            /*clientSocket.Send(buff, 0, n, SocketFlags.None);*/
        }
    }
}
