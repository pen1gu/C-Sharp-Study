using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 동전게임
{
    public partial class Form1 : Form
    {
        string serverIP = "192.168.1.107";
        int serverPort = 8080;

        Socket socket;
        System.Threading.Timer GameTmr;

        bool exitstatus = false;
        string userHash = "";
        string nickname = "";
        string gameindex = "0";
        DateTime GameEndTime = new DateTime(1970, 1, 1);

        #region 로그작성 함수
        private delegate void SetTextCallback(string strText);

        public void log(string text)
        {
            try
            {
                if (this.logbox.InvokeRequired)
                    this.Invoke((Delegate)new Form1.SetTextCallback(this.log), (object)text);
                else
                {
                    if (logbox.Lines.Count() >= 300)
                        logbox.Clear();

                    this.logbox.AppendText(text + "\r\n");
                    logbox.SelectionStart = logbox.Text.Length;
                    logbox.ScrollToCaret();
                }
            }
            catch { }

        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //크로스스레드 오류 띄우지않기
            CheckForIllegalCrossThreadCalls = false;

            IPEndPoint serverEp = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveTimeout = -1;

            try { socket.Connect(serverEp); }
            catch (Exception)
            {
                MessageBox.Show("게임서버에 접속할 수 없습니다." + Environment.NewLine +
                                "프로그램을 종료합니다.");
                Application.Exit();
                return;
            }

            Thread th = new Thread(ConnectToGameServer);
            th.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GameTmr != null) GameTmr.Dispose();

            try { Send("DISCONNECT|" + userHash + "|" + nickname); }
            catch { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            exitstatus = true;
            try { socket.Close(); }
            catch { }
            Application.Exit();
        }

        void ConnectToGameServer()
        {
            ModalForm frm = new ModalForm(socket, "", "", "");

            const int maxMessageSize = 1024;
            byte[] response = {};
            int received = 0;
            
            Send("GETUSERS");
            while (true)
            {
                if (exitstatus)
                    break;

                try
                {
                    response = new byte[maxMessageSize];
                    received = socket.Receive(response);
                    if (received == 0)
                    {
                        MessageBox.Show("게임서버와 통신이 원활하지않습니다." + Environment.NewLine +
                                        "프로그램을 종료합니다.");
                        this.Close();
                    }

                    List<byte> respBytesList = new List<byte>(response);
                    respBytesList.RemoveRange(received, maxMessageSize - received);
                    string receiveData = Encoding.UTF8.GetString(respBytesList.ToArray());
                    if (receiveData.Contains("USERLIST"))
                    {
                        int totalCnt = Convert.ToInt32(receiveData.Split(':')[0].Split('|')[1]);
                        //유저 랜덤해쉬 생성 [게임처리 중 닉네임변경으로 인해 결과반환 오류방지]
                        userHash = MD5Hash(GetRandomString(new Random().Next(5, 20)) + Convert.ToString(new Random().Next(1, 50000)));
                        nickname = "USER-" + Convert.ToString(new Random().Next(1, 10000));
                        txtNickName.Text = nickname;
                        Send("LOGIN|" + userHash + "|" + nickname);

                        lvConnectUser.Items.Add(nickname);
                        string[] userAry = receiveData.Split(':')[1].Split('|');
                        foreach (string item in userAry)
                        {
                            if (string.IsNullOrEmpty(item))
                                continue;
                            lvConnectUser.Items.Add(item);
                        }
                    }
                    else if (receiveData.Contains("DISCONNECTMSG"))
                    {
                        string user = receiveData.Split('|')[1];
                        try { lvConnectUser.FindItemWithText(user).Remove(); }
                        catch { }
                        log("== " + user + "님이 퇴장하셨습니다.");
                    }
                    else if (receiveData.Contains("CONNECTMSG"))
                    {
                        string user = receiveData.Split('|')[1];
                        log("== " + user + "님이 접속하셨습니다.");
                        lvConnectUser.Items.Add(user);
                    }
                    else if (receiveData.Contains("RENAMEMSG"))
                    {
                        string oldName = receiveData.Split('|')[1];
                        string newName = receiveData.Split('|')[2];
                        log("== " + "[" + oldName + "] 님이 닉네임을 [" + newName + "]로 변경하셧습니다.");
                        lvConnectUser.FindItemWithText(oldName).Text = newName;
                    }
                    else if (receiveData.Contains("RENAMERESULTMSG"))
                    {
                        string ResultName = receiveData.Split('|')[1];
                        if (ResultName == nickname)
                        {
                            txtNickName.Text = ResultName;
                            asyncMsgBox("[" + receiveData.Split('|')[2] + "]은(는) 이미 사용중인 닉네임입니다.");
                        }
                        else
                        {
                            lvConnectUser.FindItemWithText(nickname).Text = txtNickName.Text;
                            nickname = txtNickName.Text;
                            txtNickName.Text = ResultName;
                        }
                    }
                    else if (receiveData == "ICE")
                    {
                        if (!txtChatMsg.ReadOnly)
                        {
                            txtChatMsg.ReadOnly = true;
                            log("== 관리자가 채팅을 얼렸습니다.");
                        }
                    }
                    else if (receiveData == "UNICE")
                    {
                        if (txtChatMsg.ReadOnly)
                        {
                            txtChatMsg.ReadOnly = false;
                            log("== 관리자가 채팅을 녹였습니다.");
                        }
                    }
                    else if (receiveData.Contains("CHATMSG"))
                    {
                        string user = receiveData.Split('|')[1];
                        string msg = receiveData.Split('|')[2];
                        log(user + " : " + msg);
                    }
                    else if (receiveData.Contains("KICKMSG"))
                    {
                        string user = receiveData.Split('|')[1];
                        if (user == nickname)
                        {
                            MessageBox.Show(this,"당신은 관리자에 의해 강제퇴장되셨습니다.");
                            this.Close();
                            return;
                        }
                        else
                        {
                            lvConnectUser.FindItemWithText(user).Remove();
                            log("== " + user + "님이 관리자에 의해 강제퇴장되셨습니다.");
                        }
                    }
                    else if (receiveData.Contains("GAMESTART") || receiveData.Contains("NOWGAME"))
                    {
                        if (frm.ShowInTaskbar)
                        {
                            frm.Close();
                        }

                        pbGameScreen.Image = Properties.Resources.spin;
                        btnFront.BackColor = SystemColors.Control;
                        btnBack.BackColor = SystemColors.Control;
                        btnFront.Enabled = true;
                        btnBack.Enabled = true;

                        gameindex = receiveData.Split('|')[1];
                        string gametime = receiveData.Split('|')[2];

                        GameEndTime = new DateTime(1970, 1, 1).AddSeconds(Convert.ToInt32(gametime));
                        GameTmr = new System.Threading.Timer(new TimerCallback(StartTimer), null, 0, 500);
                    }
                    else if (receiveData.Contains("GAMERESULT"))
                    {
                        string result = receiveData.Split('|')[1];

                        if (result == "F")
                        {
                            UpdateGameLabel(gameindex + "회차" + Environment.NewLine +
                                            "결과 : 앞면");
                            pbGameScreen.Image = Properties.Resources.front;
                        }
                        else if (result == "B")
                        {
                            UpdateGameLabel(gameindex + "회차" + Environment.NewLine +
                                            "결과 : 뒷면");
                            pbGameScreen.Image = Properties.Resources.back;
                        }
                        else
                        {
                            UpdateGameLabel(gameindex + "회차" + Environment.NewLine +
                                            "결과 : 오류");
                            pbGameScreen.Image = Properties.Resources.spin;
                        }
                    }
                    else if (receiveData.Contains("BETRESULT"))
                    {
                        string gameresult = receiveData.Split('|')[1];
                        string betresult = receiveData.Split('|')[2];
                        if (betresult == "True")
                            betresult = "적중";
                        else
                            betresult = "미적중";

                        frm = new ModalForm(socket, gameindex, gameresult, betresult);
                        this.Invoke((MethodInvoker)delegate()
                        {
                            frm.Show(this);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + ex.StackTrace);
                    return;
                }
            }
        }

        void StartTimer(object state)
        {
            this.Invoke((Action)this.GameTimer);
        }

        void GameTimer()
        {
            TimeSpan remaining = GameEndTime - DateTime.Now;

            if (remaining.TotalSeconds < 1)
            {
                GameTmr.Dispose();
            }
            else if (remaining.TotalSeconds < 11)
            {
                btnFront.Enabled = false;
                btnBack.Enabled = false;
            }

            UpdateGameLabel(Convert.ToString(gameindex) + "회차" + Environment.NewLine +
                                "남은시간 : " + remaining.Minutes + "분 " + remaining.Seconds + "초");
        }

        void UpdateGameLabel(string str)
        {
            if (lblGameTime.InvokeRequired)
                lblGameTime.BeginInvoke((MethodInvoker)delegate() { lblGameTime.Text = str; });
            else
                lblGameTime.Text = str;
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            btnFront.BackColor = Color.DimGray;
            btnFront.Enabled = false;
            btnBack.Enabled = false;
            Send("BETTING|" + userHash + "|F");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.DimGray;
            btnFront.Enabled = false;
            btnBack.Enabled = false;
            Send("BETTING|" + userHash + "|B");
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtChatMsg.Text))
                return;

            Send("CHAT|" + txtChatMsg.Text);
            log(txtNickName.Text + " : " + txtChatMsg.Text);
            txtChatMsg.Clear();
            txtChatMsg.Select();
        }

        private void txtNickName_Leave(object sender, EventArgs e)
        {
            if (nickname == txtNickName.Text)
                return;

            Send("RENAME|" + nickname + "|" + txtNickName.Text);
        }

        private void txtChatMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnSendChat.PerformClick();
        }

        void Send(string msg)
        {
            socket.Send(Encoding.UTF8.GetBytes(msg));
        }

        void asyncMsgBox(string msg)
        {
            Task.Run(() =>
            {
                MessageBox.Show(msg);
            });
        }

        string GetRandomString(int length) 
        { 
            Random r = new Random(); 
            string randStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, length).Select(x => randStr[r.Next(0, randStr.Length)]); 
            return new string(chars.ToArray()); 
        }

        string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        private void tmrChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                Send("ping");
            }
            catch
            {
                tmrChecker.Enabled = false;
                MessageBox.Show("게임서버와 통신이 원활하지않습니다." + Environment.NewLine +
                                        "프로그램을 종료합니다.");
                this.Close();
            }
        }  
    }
}
