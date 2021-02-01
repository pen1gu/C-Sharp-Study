namespace OnlineTetris
{
    partial class TetrisForm
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
            this.BtnConnect = new System.Windows.Forms.Button();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(627, 415);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(92, 23);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "시작";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.Location = new System.Drawing.Point(12, 12);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(707, 397);
            this.gamePanel.TabIndex = 3;
            // 
            // TetrisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 450);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.BtnConnect);
            this.Name = "TetrisForm";
            this.Text = "Tetris";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Panel gamePanel;
    }
}

