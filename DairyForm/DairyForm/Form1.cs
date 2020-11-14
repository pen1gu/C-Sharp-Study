using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DairyForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 오
            // 대박
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;
            var text = richTextBox1.Text;

            var path = @"C:\hjun\git\C-Sharp-Study\DairyForm\text";
            var fileName = date.ToString("yyyyMMdd") + ".txt";
            var fileName0 = $"{date:yyyyMMdd}.txt"; //현업에서 쓰이는 문법
            var diaryPath = Path.Combine(path,fileName);

            File.WriteAllText(diaryPath, text);
        }
    }
}
