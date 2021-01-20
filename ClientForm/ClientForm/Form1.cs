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

namespace ClientForm
{
    public partial class Form1 : Form
    {

        Socket socket;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 7000);
                socket.Connect(ep);
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message);
                MessageBox.Show("연결할 수 없습니다.");
                return;
            }

            MessageBox.Show("연결되었습니다.");

            /*byte[] receiverBuff = new byte[8192];

            byte[] buff = Encoding.UTF8.GetBytes(textBox1.Text);

            socket.Send(buff, SocketFlags.None);

            int n = socket.Receive(receiverBuff);

            string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
            richTextBox1.AppendText(data);*/
        }
    }
}
