using Newtonsoft.Json;
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
            var diary = new DiaryData();

            diary.Title = textBox1.Text;
            diary.Content = richTextBox1.Text;
            diary.Date = date;

            var jsonText = JsonConvert.SerializeObject(diary,Formatting.Indented);
            var path = @"C:\hjun\git\C-Sharp-Study\DairyForm\text";
            //var fileName0 = date.ToString("yyyyMMdd") + ".txt";
            var fileName = $"{date:yyyyMMdd}.json"; //현업에서 쓰이는 문법
            var diaryPath = Path.Combine(path,fileName);

            File.WriteAllText(diaryPath, jsonText);

            Dispose();
        }

        //일기 제목
    }
}
