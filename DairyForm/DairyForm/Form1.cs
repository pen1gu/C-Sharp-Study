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
        private List<TagControlData> _tagList = new List<TagControlData>();
        private List<string> _tagTexts = new List<string>();

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
                encrypted: false,
                key: null,
                iv: null);
        }


        private void SaveDiary(string title, DateTime date, string content, bool encrypted, byte[] key, byte[] iv)
        {
            var diary = new DiaryData();
            diary.Title = title;
            diary.Content = content;
            diary.Date = date;
            diary.Encrypted = encrypted;
            if (encrypted != false)
            {
                diary.key = key;
                diary.iv = iv;
            }
            diary.Tags = _tagList
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

                var newTagLabel = new Label();
                newTagLabel.Text = tagText;

                var deleteButton = new Button();
                deleteButton.Text = "X";
                deleteButton.Click += (_, __) =>
                {
                    try
                    {
                        var removedData = _tagList
                            .FirstOrDefault(x => x.Control == newTagLabel);

                        if (removedData != null)
                        {
                            _tagList.Remove(removedData);
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

                TagControlData tagData = new TagControlData();
                tagData.TagText = tagText;
                tagData.Control = newTagLabel;
                tagData.DeleteButton = deleteButton;

                /*tagPanel.Controls.Add(newTagLabel);
                tagPanel.Controls.Add(deleteButton);
*/
                _tagList.Add(tagData);

                RefreshTagList();

                txtTag.Text = "";
            }
        }

        //일기 제목

        private void RefreshTagList() //태그 리스트 업데이트는 충분히 가능
        {
            int index = 0;

            _tagList.Clear();
            tagPanel.Controls.Clear();
            foreach (var tagData in _tagList)
            {
                tagData.Control.SetBounds(index * 100 + 12, 5, 60, 80);
                /*tagData.Control.Text = _tagTexts[index];*/
                tagData.DeleteButton.SetBounds(tagData.Control.Left + 50 + 12, 0, 30, 30);

                tagPanel.Controls.Add(tagData.Control);
                tagPanel.Controls.Add(tagData.DeleteButton);
                index++;
            }

            txtTag.Left = index * 100 + 12;
            tagPanel.Controls.Add(txtTag);
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
                password = password.Trim() + password.Trim(); // 공백을 제거 했을 때를 기준으로 pw 문자열 길이가 32를 안 넘을 시 계속해서 추가
            }

            byte[] key = password // key 생성 () 
                .Select(x => (byte)x)
                .Take(32)
                .ToArray();

            byte[] iv = key // key의 앞 16개
                .Take(16)
                .ToArray();//initial vector

            var encrypted = Encryptor.EncryptStringToBytes(txtContents.Text, key, iv);
            var serialized= string.Join(",", encrypted); // 암호화한 결과를 , 로 나눔

            SaveDiary(
               title: txtTitle.Text,
               date: dateTimePicker1.Value,
               content: serialized,
               encrypted: true,
               key: key,
               iv: iv); // 직렬화 한 것을 json 파일에 저장
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

        /*private void InsertData()
        {
            _tagList =
        }*/

        private void UpdateView(DiaryData diaryData)
        {
            dateTimePicker1.Value = diaryData.Date;
            txtTitle.Text = diaryData.Title;

            string decrypted = diaryData.Content;
            if(diaryData.Encrypted == true)
            {
                var bytes = decrypted.Split(',')
                    .Select(x =>
                    (byte)int.Parse(x))
                    .ToArray();

                decrypted = Encryptor.DecryptStringFromBytes(bytes,diaryData.key,diaryData.iv);
            }

            txtContents.Text = decrypted;


            _tagTexts = diaryData.Tags;

            int index = 0;
            foreach (string tagText in _tagTexts)
            {
                Label label = new Label();
                label.Text = tagText;
                Button button = new Button();
                button.Text = "X"; // x버튼 누를 시 삭제 추가

                TagControlData ControlData = new TagControlData();
                ControlData.Control = label;
                ControlData.DeleteButton = button;
                ControlData.Index = index;
                ControlData.TagText = tagText;

                _tagList.Add(ControlData);
                index++;
            }



            #region Tags
            #region Remove tag Controls

            RefreshTagList();


            /*  foreach (var tagData in _tagData)
              {
                  Controls.Remove(tagData.Control);
                  Controls.Remove(tagData.DeleteButton);
              }
  */
            #endregion

            
            /*#region Create tag Controls
            _tagList = diaryData.Tags.Select((x, i) => new TagControlData
            {
                Index = i,
                TagText = x,
                Control = new Label { Text = x },
                DeleteButton = new Button { Text = "X" }
            }).ToList(); //리스트 불러오기

            #endregion*/
            #endregion
        }

        //추가 해야할 부분: 태그 업데이트, 복호화, 전체 재 구성(업데이트 관련 부분)
    }


    public class TagControlData
    {
        public string TagText;
        public int Index;
        public Label Control;
        public Button DeleteButton;
    }
}
