using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DairyForm
{
    

    public partial class Form1 : Form
    {
        private List<TagControlData> _tagData = new List<TagControlData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           SaveDiary(
                title: textBox1.Text,
                date: dateTimePicker1.Value,
                content: richTextBox1.Text,
                encrypted: false);
        }


        private void SaveDiary(string title, DateTime date, string content, bool encrypted)
        {
            var diary = new DiaryData();
            diary.Title = title;
            diary.Content = content;
            diary.Date = date;

            diary.Tags = _tagData
                .Select(x => x.TagText)
                .ToList();

            var jsonText = JsonConvert.SerializeObject(diary, Formatting.Indented);
            var path = @"C:\hjun\git\C-Sharp-Study\DairyForm\text";
            var fileName = $"{date:yyyyMMdd}.json"; //현업에서 쓰이는 문법
            var diaryPath = Path.Combine(path,fileName);

            File.WriteAllText(diaryPath, jsonText);

            Dispose();
        }


        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var tagText = textBox2.Text;
                var newTagLabel = new Label();
                newTagLabel.Text = tagText;

                var deleteButton = new Button();
                deleteButton.Text = "X";
                deleteButton.Click += (_, __) =>
                {
                    try
                    {
                        var removedData = _tagData
                            .FirstOrDefault(x => x.Control == newTagLabel);

                        if (removedData != null)
                        {
                            _tagData.Remove(removedData);
                            Controls.Remove(newTagLabel);
                            Controls.Remove(deleteButton);
                            RefreshTagList();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };

                var tagControlData = new TagControlData
                {
                    TagText = tagText,
                    Control = newTagLabel,
                    DeleteButton = deleteButton,
                };

                _tagData.Add(tagControlData);

                this.Controls.Add(deleteButton);
                this.Controls.Add(newTagLabel);

                RefreshTagList();

                textBox2.Text = "";
            }
        }

        //일기 제목

        private void RefreshTagList()
        {
            int index = 0;
            foreach(var tagData in _tagData)
            {
                tagData.Control.SetBounds(index * 100 + 12, 280, 80, 80);
                tagData.DeleteButton.SetBounds(tagData.Control.Left + 50 + 12, 275, 30, 30);
                index++;
            }

            textBox2.Left = index * 100 + 12;
        }

        private void btnEncryption_Click(object sender, EventArgs e)
        {
            var password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("암호를 확인하세요.");
                return;
            }

            while(password.Trim().Length < 32)
            {
                password = password.Trim() + password.Trim();
            }

            byte[] key = password
                .Select(x => (byte)x)
                .Take(32)
                .ToArray();

            byte[] iv = key
                .Take(16)
                .ToArray();//initial vector

            var encrypted = Encryptor.EncryptStringToBytes(richTextBox1.Text, key, iv);
            var serialized= string.Join(",", encrypted);

            SaveDiary(
               title: textBox1.Text,
               date: dateTimePicker1.Value,
               content: serialized,
               encrypted: true);
        }
    }

    public class TagControlData
    {
        public string TagText;
        public Label Control;
        public Button DeleteButton;
    }
}
