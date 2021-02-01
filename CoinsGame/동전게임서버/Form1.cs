using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 동전게임서버
{
    public partial class Form1 : Form
    {
        List<Socket> connections = new List<Socket>(1);
        List<string> HashList = new List<string>();
        List<string> NicknameList = new List<string>();
        List<string> GameBettingList = new List<string>();
        public Socket socket;

        bool exitstatus = false;
        
        bool startgame = false;
        int gameIndex = 0;
        int gameEndTime = 0;
        string gameResult = "null";

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

            //람다식으로 ipv4 주소를 가지고옴
            IPAddress ipv4Addresses = Array.Find(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            txtIPAddress.Text = ipv4Addresses.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            exitstatus = true;
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            finally
            {
                try { socket.Close(0); }
                catch { }
            }
            Application.Exit();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            btnServerStart.Enabled = false;
            Thread th = new Thread(WorkServer);
            th.Start();
        }

        void WorkServer()
        {
            int backlog = -1;

            connections = new List<Socket>(Convert.ToInt32(nudMaxUserCnt.Value));

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveTimeout = -1;

            try
            {
                log("서버를 시작합니다.");
                socket.Bind(new IPEndPoint(IPAddress.Parse(txtIPAddress.Text), Convert.ToInt32(nudPort.Value)));
                socket.Listen(backlog);
            }
            catch
            {
                log("서버 시작에 실패하였습니다.");
                return;
            }

            log("서버가 정상적으로 구동되었습니다.");

            while (true)
            {
                if (exitstatus)
                    break;

                if (Convert.ToInt32(nudMaxUserCnt.Value) <= connections.Count)
                    continue;

                Socket client = socket.Accept();
                new Thread(() =>
                {
                    try { ClientProcess(client); }
                    catch (Exception ex) { log("클라이언트 연결중 오류 발생 : " + ex.Message + ex.StackTrace); }
                }).Start();

                Application.DoEvents();
            }
        }

        void ClientProcess(Socket client)
        {
            connections.Add(client);
            GameBettingList.Add("null");
            log("클라이언트가 접속했습니다. [" + client.RemoteEndPoint + "]");

            if (connections.Count >= 1 && !startgame)
            {
                startgame = true;
                new Thread(() =>
                {
                    GameProcess();
                }).Start();
            }

            const int maxMessageSize = 1024;
            byte[] response;
            int received;
            string clientHash = "";
            string clientNickName = "";

            while (true)
            {
                if (exitstatus)
                    break;

                response = new byte[maxMessageSize];
                try
                {
                    received = client.Receive(response);
                    if (received == 0)
                    {
                        SendAll("DISCONNECTMSG|" + clientNickName, client);
                        return;
                    }
                }
                catch
                {
                    SendAll("DISCONNECTMSG|" + clientNickName, client);
                    return;
                }

                List<byte> respBytesList = new List<byte>(response);
                respBytesList.RemoveRange(received, maxMessageSize - received); // truncate zero end

                string receiveData = Encoding.UTF8.GetString(respBytesList.ToArray());
                if (receiveData == "ping")
                {
                    Send(client, "pong");
                }
                //로그인
                else if (receiveData.Contains("LOGIN"))
                {
                    try
                    {
                        clientHash = receiveData.Split('|')[1];
                        clientNickName = receiveData.Split('|')[2];
                        HashList.Add(clientHash);
                        NicknameList.Add(clientNickName);
                        ListViewItem lvi = new ListViewItem(Convert.ToString(lvUserList.Items.Count + 1));
                        lvi.SubItems.Add(clientNickName);
                        lvi.SubItems.Add(clientHash);
                        lvUserList.Items.Add(lvi);

                        SendAll("CONNECTMSG|" + clientNickName, client);

                        //만약 게임이 생성되어 있을경우
                        if (startgame)
                        {
                            Send(client, "NOWGAME|" + Convert.ToString(gameIndex) + "|" + Convert.ToString(gameEndTime));
                        }
                    }
                    catch
                    {
                        log(client.RemoteEndPoint + "의 닉네임은 설정되지않았습니다.");
                        KickClient(client, "", "");
                    }
                }
                //연결종료
                else if (receiveData.Contains("DISCONNECT"))
                {
                    SendAll("DISCONNECTMSG|" + clientNickName, client);
                    KickClient(client, receiveData.Split('|')[1], receiveData.Split('|')[2]);
                }
                //접속유저 반환
                else if (receiveData.Contains("GETUSERS"))
                {
                    string tmpstr = Convert.ToString(NicknameList.Count) + ":";
                    foreach (string item in NicknameList)
                        tmpstr += item + "|";

                    Send(client, "USERLIST|" + tmpstr);
                }
                // 이름변경
                else if (receiveData.Contains("RENAME"))
                {
                    string oldName = receiveData.Split('|')[1];
                    string newName = receiveData.Split('|')[2];
                    //이름이 이미있는지 람다식으로 체크
                    bool exists = NicknameList.Exists(a => a == newName);
                    if (exists)
                        Send(client, "RENAMERESULTMSG|" + oldName + "|" + newName);
                    else
                    {
                        int index = NicknameList.FindIndex(a => a == oldName);
                        NicknameList[index] = newName;
                        clientNickName = newName;
                        lvUserList.Items.Cast<ListViewItem>().Where(x => (x.SubItems[1].Text == oldName)).FirstOrDefault().SubItems[1].Text = newName;

                        Send(client, "RENAMERESULTMSG|" + newName);
                        SendAll("RENAMEMSG|" + oldName + "|" + newName, client);
                    }
                }
                // 채팅
                else if (receiveData.Contains("CHAT"))
                {
                    SendAll("CHATMSG|" + clientNickName + "|" +  receiveData.Split('|')[1], client);
                }
                // 배팅
                else if (receiveData.Contains("BETTING"))
                {
                    string hash = receiveData.Split('|')[1];
                    string betway = receiveData.Split('|')[2];
                    int index = HashList.FindIndex(a => a == hash);
                    GameBettingList[index] = betway;
                    //SendAll("BETTINGMSG|" + clientNickName + "|" + betway);
                }
                
                if(receiveData != "ping")
                {
                    try { log(clientNickName + "[" + client.RemoteEndPoint + "] : " + receiveData); }
                    catch { }
                }
            }
        }

        void GameProcess()
        {
            Random r = new Random();

            do
            {
                //클라이언트가 아무도 없을경우 게임생성관두기
                if (connections.Count == 0)
                    break;

                //게임 초기화
                gameResult = "null";
                GameBettingList.ForEach(a => a = "null");

                //게임 종료시간을 초단위 TimeStamp로 계산 후 제작
                gameEndTime = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                gameEndTime += Convert.ToInt32(nudGameTIme.Value);
            
                //회차 증가하며 로그생성
                log("== " + Convert.ToString(++gameIndex) + "회차 게임생성 [" + Convert.ToString(gameEndTime) + "]");

                //접속된 클라이언트들에게 게임시작되었다고 전송
                SendAll("GAMESTART|" + Convert.ToString(gameIndex) + "|" + Convert.ToString(gameEndTime));

                //게임 종료시간이 될 때 까지 무한루프 하여 마감을 대기함
                do {} while (gameEndTime >= (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

                //혹시 모르니까 결과반환 전 클라이언트가 접속중인지 체크
                if (connections.Count == 0)
                    break;

                //게임 결과 생성 랜덤 50:50 으로 결과지정
                if (r.Next(1, 100) <= 50)
                    gameResult = "F";
                else
                    gameResult = "B";

                log("== " + Convert.ToString(gameIndex) + "회차 게임마감 [" + gameResult + "]");
                SendAll("GAMERESULT|" + gameResult);
                //클라이언트들에게 결과를 반환함
                for (int i = 0; i < NicknameList.Count; i++)
                {
                    string nickname = NicknameList[i];
                    string betway = GameBettingList[i];
                    Socket client = connections[i];

                    //배팅위치가 없다면 건너뜀
                    if (betway == "null")
                        continue;

                    //결과를 정함
                    bool result = false;
                    if (gameResult == betway)
                        result = true;

                    //클라이언트에게 결과반환
                    Send(client, "BETRESULT|" + gameResult + "|" + Convert.ToString(result));
                    GameBettingList[i] = "null";
                }

                //다음게임 시작 전 10초를 대기함
                Thread.Sleep(10000);
            } while (true);

            //만약 dowhile이 탈출이 된다면 게임이 실행중이 아님으로 지정
            startgame = false;
        }

        private void cmsbtnKick_Click(object sender, EventArgs e)
        {
            int index = lvUserList.SelectedItems[0].Index;
            string nickname = lvUserList.Items[index].SubItems[1].Text;
            string hash = lvUserList.Items[index].SubItems[2].Text;
            Socket client = connections[index];
            SendAll("KICKMSG|" + nickname);
            KickClient(client, hash, nickname);
            asyncMsgBox(nickname + "님을 강퇴했습니다.");
        }

        private void cmsbtnIceUser_Click(object sender, EventArgs e)
        {
            int index = lvUserList.SelectedItems[0].Index;
            string nickname = lvUserList.Items[index].SubItems[1].Text;
            Socket client = connections[index];
            lvUserList.Items[index].BackColor = Color.DodgerBlue;
            Send(client, "ICE");
            asyncMsgBox(nickname + "님의 채팅방을 얼렸습니다.");
        }

        private void cmsbtnUnIceUser_Click(object sender, EventArgs e)
        {
            int index = lvUserList.SelectedItems[0].Index;
            string nickname = lvUserList.Items[index].SubItems[1].Text;
            Socket client = connections[index];
            lvUserList.Items[index].BackColor = Color.White;
            Send(client, "UNICE");
            asyncMsgBox(nickname + "님의 채팅방을 녹였습니다.");
        }

        private void cmsbtnIceAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvUserList.Items)
            {
                lvi.BackColor = Color.DodgerBlue;
            }
            SendAll("ICE");
            asyncMsgBox("채팅방을 얼렸습니다.");
        }

        private void cmsbtnUnIceAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvUserList.Items)
            {
                lvi.BackColor = Color.White;
            }
            SendAll("UNICE");
            asyncMsgBox("채팅방을 녹였습니다.");
        }

        void Send(Socket client, string msg)
        {
            try { client.Send(Encoding.UTF8.GetBytes(msg)); }
            catch { }
        }

        void SendAll(string msg, Socket client = null)
        {
            foreach (Socket socket in connections)
            {
                if (client == socket)
                    continue;
                try { socket.Send(Encoding.UTF8.GetBytes(msg)); }
                catch { }
            }
        }

        void KickClient(Socket client, string hash, string nickname)
        {
            try
            {
                lvUserList.Items.Cast<ListViewItem>().Where(x => (x.SubItems[1].Text == nickname)).FirstOrDefault().Remove();
                connections.Remove(client);
                int index = HashList.FindIndex(a => a == hash);
                try { HashList.RemoveAt(index); }
                catch { }
                try { NicknameList.RemoveAt(index); }
                catch { }
                try { GameBettingList.RemoveAt(index); }
                catch { }
                client.Close();
            }
            catch { }
        }

        void asyncMsgBox(string msg)
        {
            Task.Run(() =>
            {
                MessageBox.Show(msg);
            });
        }
    }
}
