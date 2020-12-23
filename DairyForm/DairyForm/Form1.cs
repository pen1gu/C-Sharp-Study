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
        readonly string _rootPath = @"C:\hjun\git\C-Sharp-Study\DairyForm\text";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           SaveDiary(
                title: txtTitle.Text,
                date: dateTimePicker1.Value,
                content: txtContents.Text,
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
            
            var fileName = $"{date:yyyyMMdd}.json"; //현업에서 쓰이는 문법
            var diaryPath = Path.Combine(_rootPath, fileName);

            File.WriteAllText(diaryPath, jsonText);

            Dispose();
        }


        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var tagText = txtTag.Text;
                /*_diaryData.tags.add(tagText);
                UpdateView(_diaryData);*/

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

                

                RefreshTagList();

                txtTag.Text = "";
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

            txtTag.Left = index * 100 + 12;
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

            var encrypted = Encryptor.EncryptStringToBytes(txtContents.Text, key, iv);
            var serialized= string.Join(",", encrypted);

            SaveDiary(
               title: txtTitle.Text,
               date: dateTimePicker1.Value,
               content: serialized,
               encrypted: true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(_rootPath, "*.json");
            foreach(var file in files)
            {
                listContents.Items.Add(Path.GetFileName(file));
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var fileName = listContents.SelectedItem.ToString();
            LoadDiary(fileName);
            
        }

        private void LoadDiary(string fileName)
        {
            var filePath = Path.Combine(_rootPath, fileName);
            var jsonText = File.ReadAllText(filePath);
            var diaryData = JsonConvert.DeserializeObject<DiaryData>(jsonText);

            UpdateView(diaryData);
        }

        private void UpdateView(DiaryData diaryData)
        {
            dateTimePicker1.Value = diaryData.Date;
            txtTitle.Text = diaryData.Title;
            txtContents.Text = diaryData.Content;

            #region Tags
            #region Remove tag Controls
            foreach (var tagData in _tagData)
            {
                Controls.Remove(tagData.Control);
                Controls.Remove(tagData.DeleteButton);
            }

            #endregion

            #region Create tag Controls
            _tagData = diaryData.Tags.Select((x, i) => new TagControlData
            {
                Index = i,
                TagText = x,
                Control = new Label { Text = x },
                DeleteButton = new Button { Text = "X" }
            }).ToList();

            #endregion
            #endregion
        }
    }

    public class TagControlData
    {
        public string TagText;
        public int Index;
        public Label Control;
        public Button DeleteButton;
    }
}
