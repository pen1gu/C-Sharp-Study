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
        DiaryData diaryData;
        public Form1()
        {
            this.diaryData = new DiaryData();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 오
            // 대박
            
        }

        private void Form1_KeyDown(Object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;


            diaryData.Title = textBox1.Text;
            diaryData.Content = richTextBox1.Text;
            diaryData.Date = date;

            var jsonText = JsonConvert.SerializeObject(diaryData,Formatting.Indented);
            var path = @"C:\hjun\git\C-Sharp-Study\DairyForm\text";
            var fileName = $"{date:yyyyMMdd}.json"; //현업에서 쓰이는 문법
            var diaryPath = Path.Combine(path,fileName);

            File.WriteAllText(diaryPath, jsonText);

            Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var y = 280;
                //var 
                var newTagLabel = new Label();
                newTagLabel.Text = textBox2.Text;
                newTagLabel.SetBounds(textBox2.Left, y, 100, 30);

                textBox2.Left += 100;
                diaryData.Tags.Add(textBox2.Text);
                this.Controls.Add(newTagLabel);
            }
        }
        //일기 제목
    }
}
