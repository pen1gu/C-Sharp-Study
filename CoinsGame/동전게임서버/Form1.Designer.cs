namespace 동전게임서버
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
            this.btnServerStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMaxUserCnt = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvUserList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.선택유저강퇴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnKick = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnIceUser = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnUnIceUser = new System.Windows.Forms.ToolStripMenuItem();
            this.채팅방ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnIceAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnUnIceAll = new System.Windows.Forms.ToolStripMenuItem();
            this.nudGameTIme = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxUserCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameTIme)).BeginInit();
            this.SuspendLayout();
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(12, 118);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(378, 36);
            this.btnServerStart.TabIndex = 0;
            this.btnServerStart.Text = "서버시작";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudGameTIme);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudMaxUserCnt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtIPAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "설정";
            // 
            // nudMaxUserCnt
            // 
            this.nudMaxUserCnt.Location = new System.Drawing.Point(80, 63);
            this.nudMaxUserCnt.Name = "nudMaxUserCnt";
            this.nudMaxUserCnt.Size = new System.Drawing.Size(115, 21);
            this.nudMaxUserCnt.TabIndex = 7;
            this.nudMaxUserCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMaxUserCnt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "최대인원";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(292, 28);
            this.nudPort.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(57, 21);
            this.nudPort.TabIndex = 5;
            this.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "포트";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(80, 27);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(115, 21);
            this.txtIPAddress.TabIndex = 3;
            this.txtIPAddress.Text = "127.0.0.1";
            this.txtIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "아이피";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.logbox);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 300);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOG";
            // 
            // logbox
            // 
            this.logbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logbox.Location = new System.Drawing.Point(3, 17);
            this.logbox.Name = "logbox";
            this.logbox.Size = new System.Drawing.Size(372, 280);
            this.logbox.TabIndex = 0;
            this.logbox.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lvUserList);
            this.groupBox3.Location = new System.Drawing.Point(396, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(293, 448);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "유저목록";
            // 
            // lvUserList
            // 
            this.lvUserList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvUserList.ContextMenuStrip = this.cmsMenu;
            this.lvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUserList.FullRowSelect = true;
            this.lvUserList.GridLines = true;
            this.lvUserList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUserList.Location = new System.Drawing.Point(3, 17);
            this.lvUserList.MultiSelect = false;
            this.lvUserList.Name = "lvUserList";
            this.lvUserList.Size = new System.Drawing.Size(287, 428);
            this.lvUserList.TabIndex = 0;
            this.lvUserList.UseCompatibleStateImageBehavior = false;
            this.lvUserList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "번호";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "닉네임";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 196;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.선택유저강퇴ToolStripMenuItem,
            this.채팅방ToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(111, 48);
            // 
            // 선택유저강퇴ToolStripMenuItem
            // 
            this.선택유저강퇴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsbtnKick,
            this.cmsbtnIceUser,
            this.cmsbtnUnIceUser});
            this.선택유저강퇴ToolStripMenuItem.Name = "선택유저강퇴ToolStripMenuItem";
            this.선택유저강퇴ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.선택유저강퇴ToolStripMenuItem.Text = "유저";
            // 
            // cmsbtnKick
            // 
            this.cmsbtnKick.Name = "cmsbtnKick";
            this.cmsbtnKick.Size = new System.Drawing.Size(138, 22);
            this.cmsbtnKick.Text = "강퇴";
            this.cmsbtnKick.Click += new System.EventHandler(this.cmsbtnKick_Click);
            // 
            // cmsbtnIceUser
            // 
            this.cmsbtnIceUser.Name = "cmsbtnIceUser";
            this.cmsbtnIceUser.Size = new System.Drawing.Size(138, 22);
            this.cmsbtnIceUser.Text = "채팅 얼리기";
            this.cmsbtnIceUser.Click += new System.EventHandler(this.cmsbtnIceUser_Click);
            // 
            // cmsbtnUnIceUser
            // 
            this.cmsbtnUnIceUser.Name = "cmsbtnUnIceUser";
            this.cmsbtnUnIceUser.Size = new System.Drawing.Size(138, 22);
            this.cmsbtnUnIceUser.Text = "채팅 녹이기";
            this.cmsbtnUnIceUser.Click += new System.EventHandler(this.cmsbtnUnIceUser_Click);
            // 
            // 채팅방ToolStripMenuItem
            // 
            this.채팅방ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsbtnIceAll,
            this.cmsbtnUnIceAll});
            this.채팅방ToolStripMenuItem.Name = "채팅방ToolStripMenuItem";
            this.채팅방ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.채팅방ToolStripMenuItem.Text = "채팅방";
            // 
            // cmsbtnIceAll
            // 
            this.cmsbtnIceAll.Name = "cmsbtnIceAll";
            this.cmsbtnIceAll.Size = new System.Drawing.Size(110, 22);
            this.cmsbtnIceAll.Text = "얼리기";
            this.cmsbtnIceAll.Click += new System.EventHandler(this.cmsbtnIceAll_Click);
            // 
            // cmsbtnUnIceAll
            // 
            this.cmsbtnUnIceAll.Name = "cmsbtnUnIceAll";
            this.cmsbtnUnIceAll.Size = new System.Drawing.Size(110, 22);
            this.cmsbtnUnIceAll.Text = "녹이기";
            this.cmsbtnUnIceAll.Click += new System.EventHandler(this.cmsbtnUnIceAll_Click);
            // 
            // nudGameTIme
            // 
            this.nudGameTIme.Location = new System.Drawing.Point(292, 63);
            this.nudGameTIme.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudGameTIme.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudGameTIme.Name = "nudGameTIme";
            this.nudGameTIme.Size = new System.Drawing.Size(57, 21);
            this.nudGameTIme.TabIndex = 9;
            this.nudGameTIme.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudGameTIme.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "게임시간(초)";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "MD5해쉬";
            this.columnHeader3.Width = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 472);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnServerStart);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(717, 511);
            this.MinimumSize = new System.Drawing.Size(717, 511);
            this.Name = "Form1";
            this.Text = "동전게임서버";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxUserCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudGameTIme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudMaxUserCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox logbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvUserList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem 선택유저강퇴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnKick;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnIceUser;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnUnIceUser;
        private System.Windows.Forms.ToolStripMenuItem 채팅방ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnIceAll;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnUnIceAll;
        private System.Windows.Forms.NumericUpDown nudGameTIme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

