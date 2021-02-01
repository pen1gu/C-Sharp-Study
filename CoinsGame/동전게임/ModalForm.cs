using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 동전게임
{
    public partial class ModalForm : Form
    {
        public ModalForm(Socket socket, string gameindex, string gameresult, string betresult)
        {
            InitializeComponent();

            label1.Text = Convert.ToString(gameindex) + "회차 결과는 [" + gameresult + "] 입니다." + Environment.NewLine +
                          "" + Environment.NewLine +
                          "회원님의 배팅 결과 : " + betresult;
        }

        private void ModalForm_Shown(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                int offset = Owner.OwnedForms.Length;
                Point p = new Point(Owner.Left + Owner.Width / 2 - Width / 2 + offset, Owner.Top + Owner.Height / 2 - Height / 2 + offset);
                this.Location = p;
            }
        }
    }
}
