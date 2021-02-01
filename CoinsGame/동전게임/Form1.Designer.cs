namespace 동전게임
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbGameScreen = new System.Windows.Forms.PictureBox();
            this.lblGameTime = new System.Windows.Forms.Label();
            this.btnFront = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendChat = new System.Windows.Forms.Button();
            this.txtChatMsg = new System.Windows.Forms.TextBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvConnectUser = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.tmrChecker = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameScreen)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbGameScreen);
            this.panel1.Controls.Add(this.lblGameTime);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 342);
            this.panel1.TabIndex = 0;
            // 
            // pbGameScreen
            // 
            this.pbGameScreen.Image = global::동전게임.Properties.Resources.spin;
            this.pbGameScreen.Location = new System.Drawing.Point(3, 44);
            this.pbGameScreen.Name = "pbGameScreen";
            this.pbGameScreen.Size = new System.Drawing.Size(294, 294);
            this.pbGameScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGameScreen.TabIndex = 2;
            this.pbGameScreen.TabStop = false;
            // 
            // lblGameTime
            // 
            this.lblGameTime.Font = new System.Drawing.Font("굴림", 12F);
            this.lblGameTime.Location = new System.Drawing.Point(3, 0);
            this.lblGameTime.Name = "lblGameTime";
            this.lblGameTime.Size = new System.Drawing.Size(294, 41);
            this.lblGameTime.TabIndex = 1;
            this.lblGameTime.Text = "-회차\r\n남은시간 : 00분00초";
            this.lblGameTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFront
            // 
            this.btnFront.BackColor = System.Drawing.SystemColors.Control;
            this.btnFront.Location = new System.Drawing.Point(12, 360);
            this.btnFront.Name = "btnFront";
            this.btnFront.Size = new System.Drawing.Size(144, 43);
            this.btnFront.TabIndex = 1;
            this.btnFront.Text = "앞면";
            this.btnFront.UseVisualStyleBackColor = false;
            this.btnFront.Click += new System.EventHandler(this.btnFront_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnBack.Location = new System.Drawing.Point(170, 360);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(144, 43);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "뒷면";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendChat);
            this.groupBox1.Controls.Add(this.txtChatMsg);
            this.groupBox1.Controls.Add(this.txtNickName);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(320, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 390);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "채팅방";
            // 
            // btnSendChat
            // 
            this.btnSendChat.Location = new System.Drawing.Point(187, 359);
            this.btnSendChat.Name = "btnSendChat";
            this.btnSendChat.Size = new System.Drawing.Size(69, 21);
            this.btnSendChat.TabIndex = 6;
            this.btnSendChat.Text = "전송";
            this.btnSendChat.UseVisualStyleBackColor = true;
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);
            // 
            // txtChatMsg
            // 
            this.txtChatMsg.Location = new System.Drawing.Point(78, 359);
            this.txtChatMsg.Name = "txtChatMsg";
            this.txtChatMsg.Size = new System.Drawing.Size(103, 21);
            this.txtChatMsg.TabIndex = 5;
            this.txtChatMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChatMsg_KeyPress);
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(6, 359);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(66, 21);
            this.txtNickName.TabIndex = 4;
            this.txtNickName.Text = "Guest";
            this.txtNickName.Leave += new System.EventHandler(this.txtNickName_Leave);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lvConnectUser);
            this.panel2.Controls.Add(this.logbox);
            this.panel2.Location = new System.Drawing.Point(6, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 333);
            this.panel2.TabIndex = 0;
            // 
            // lvConnectUser
            // 
            this.lvConnectUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvConnectUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvConnectUser.FullRowSelect = true;
            this.lvConnectUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvConnectUser.Location = new System.Drawing.Point(0, 0);
            this.lvConnectUser.MultiSelect = false;
            this.lvConnectUser.Name = "lvConnectUser";
            this.lvConnectUser.Size = new System.Drawing.Size(249, 97);
            this.lvConnectUser.TabIndex = 2;
            this.lvConnectUser.UseCompatibleStateImageBehavior = false;
            this.lvConnectUser.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "접속중인 유저";
            this.columnHeader1.Width = 223;
            // 
            // logbox
            // 
            this.logbox.BackColor = System.Drawing.Color.White;
            this.logbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logbox.Location = new System.Drawing.Point(0, 97);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logbox.Size = new System.Drawing.Size(249, 235);
            this.logbox.TabIndex = 3;
            this.logbox.Text = "";
            // 
            // tmrChecker
            // 
            this.tmrChecker.Enabled = true;
            this.tmrChecker.Interval = 1000;
            this.tmrChecker.Tick += new System.EventHandler(this.tmrChecker_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 413);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFront);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "동전게임";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGameScreen)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGameTime;
        private System.Windows.Forms.PictureBox pbGameScreen;
        private System.Windows.Forms.Button btnFront;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvConnectUser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.RichTextBox logbox;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.Button btnSendChat;
        private System.Windows.Forms.TextBox txtChatMsg;
        private System.Windows.Forms.Timer tmrChecker;
    }
}

