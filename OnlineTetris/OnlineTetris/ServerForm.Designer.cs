namespace OnlineTetris
{
    partial class ServerForm
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtClientLog = new System.Windows.Forms.RichTextBox();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtClientLog
            // 
            this.TxtClientLog.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.TxtClientLog.Location = new System.Drawing.Point(16, 12);
            this.TxtClientLog.Name = "TxtClientLog";
            this.TxtClientLog.Size = new System.Drawing.Size(328, 381);
            this.TxtClientLog.TabIndex = 0;
            this.TxtClientLog.Text = "";
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(240, 413);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(104, 25);
            this.BtnSubmit.TabIndex = 1;
            this.BtnSubmit.Text = "전송";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(16, 413);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(92, 23);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "연결";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 450);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.TxtClientLog);
            this.Name = "ServerForm";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox TxtClientLog;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.Button BtnConnect;
    }
}

