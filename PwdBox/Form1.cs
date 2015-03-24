using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace PwdBox
{
    public partial class Form1 : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;
        settings settings = new settings();
        Props props = new Props();


        public Form1()
        {
            InitializeComponent();
        }
        public void DataGridUpdate()
        {
            Synchronized();  
        }

// пошло поехало
        public void Synchronized()
        {
            
            string connectionString = "Database= ;Data Source= ;User Id= ;Password= ";//Настройки соединения с MYSQL
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand();
            string commandString = "SELECT * FROM sites;";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {

                command.Connection.Open();
                reader = command.ExecuteReader();
                var count = dataGridView1.RowCount;
                if (count < 1)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                }
                else if (count > 1)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                };
                this.dataGridView1.Columns.Add("id", "ID");
                this.dataGridView1.Columns["id"].Width = 30;
                this.dataGridView1.Columns.Add("name", "Название");
                this.dataGridView1.Columns["name"].Width = 140;
                this.dataGridView1.Columns.Add("url", "Ссылка на сайт");
                this.dataGridView1.Columns["url"].Width = 140;
                this.dataGridView1.Columns.Add("urlcp", "Ссылка на ПУ");
                this.dataGridView1.Columns["urlcp"].Width = 140;
                this.dataGridView1.Columns.Add("logincp", "Логин ПУ");
                this.dataGridView1.Columns["logincp"].Width = 90;
                this.dataGridView1.Columns.Add("pwdcp", "Пароль ПУ");
                this.dataGridView1.Columns["pwdcp"].Width = 90;
                this.dataGridView1.Columns.Add("hostftp", "Хост FTP");
                this.dataGridView1.Columns["hostftp"].Width = 140;
                this.dataGridView1.Columns.Add("loginftp", "Логин FTP");
                this.dataGridView1.Columns["loginftp"].Width = 90;
                this.dataGridView1.Columns.Add("pwdftp", "Пароль FTP");
                this.dataGridView1.Columns["pwdftp"].Width = 90;
                this.dataGridView1.Columns.Add("hosting", "Ссылка Хостинг");
                this.dataGridView1.Columns["hosting"].Width = 140;
                this.dataGridView1.Columns.Add("hostinglogin", "Логин Хостинг");
                this.dataGridView1.Columns["hostinglogin"].Width = 90;
                this.dataGridView1.Columns.Add("hostingpwd", "Пароль Хостинг");
                this.dataGridView1.Columns["hostingpwd"].Width = 90;
                this.dataGridView1.Columns.Add("datecreate", "Дата добавления");
                this.dataGridView1.Columns["datecreate"].Width = 140;
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["id"].ToString(), reader["name"].ToString(), reader["url"].ToString(), reader["urlcp"].ToString(), reader["logincp"].ToString(), reader["pwdcp"].ToString(), reader["hostftp"].ToString(), reader["loginftp"].ToString(), reader["pwdftp"].ToString(), reader["hosting"].ToString(), reader["hostinglogin"].ToString(), reader["hostingpwd"].ToString(), reader["datecreate"].ToString());
                }
                reader.Close();
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Синхронизация в данный момент не доступна\r\nПроизвожу открытие из локального файла", "Ошибка синхронизации");
                StreamReader rd = new StreamReader(@"sitelist.txt");
                DataSet ds = new DataSet();
                ds.Tables.Add("Score");
                string header = "ID;Название;Ссылка на сайт;Ссылка на ПУ;Логин ПУ;Пароль ПУ;Хост FTP;Логин FTP;Пароль FTP;Ссылка xостинг;Логин Хостинг;Пароль Хостинг;Дата добавления";
                string[] col = System.Text.RegularExpressions.Regex.Split(header, ";");
                for (int c = 0; c < col.Length; c++)
                {
                    ds.Tables[0].Columns.Add(col[c]);
                }
                string row = rd.ReadLine();
                while (row != null)
                {
                    string[] rvalue = System.Text.RegularExpressions.Regex.Split(row, ";");
                    ds.Tables[0].Rows.Add(rvalue);
                    row = rd.ReadLine();
                }
                dataGridView1.DataSource = ds.Tables[0];
            }
        }   // Синхронизация
        private void SaveToFile()
        {
            // диалоговое окно
            var savedialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "bin",
                Filter = @"Текстовые файлы (*.txt)|*.txt|CSV-файл (*.csv)|*.csv|Bin-файл (*.bin)|*.bin",
                FilterIndex = 2,
                RestoreDirectory = true

            };

            if (savedialog.ShowDialog() != DialogResult.OK) return;
            var savefile = new StreamWriter(savedialog.FileName, true, Encoding.UTF8);

            foreach (DataGridViewRow row in dataGridView1.Rows) //запись
                if (!row.IsNewRow)
                {
                    var first = true;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!first) savefile.Write(";");
                        savefile.Write(cell.Value.ToString());
                        first = false;
                    }
                    savefile.WriteLine();
                }
            savefile.Close();
        }   // Сохранение файла
        private void OpenToCastomSavedFile(){
            StreamReader rd = new StreamReader(@"sitelist.txt");
            DataSet ds = new DataSet();
            ds.Tables.Add("Score");
            string header = "ID;Название;Ссылка на сайт;Ссылка на ПУ;Логин ПУ;Пароль ПУ;Хост FTP;Логин FTP;Пароль FTP;Ссылка xостинг;Логин Хостинг;Пароль Хостинг;Дата добавления";
            string[] col = System.Text.RegularExpressions.Regex.Split(header, ";");
            for (int c = 0; c < col.Length; c++)
            {
                ds.Tables[0].Columns.Add(col[c]);
            }
            string row = rd.ReadLine();
            while (row != null)
            {
                string[] rvalue = System.Text.RegularExpressions.Regex.Split(row, ";");
                ds.Tables[0].Rows.Add(rvalue);
                row = rd.ReadLine();
            }
            dataGridView1.DataSource = ds.Tables[0];
        }   // открытие файла
        private void SearchFunc()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                for (j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.White;
                    dataGridView1.Rows[j].Cells[i].Style.ForeColor = Color.Black;
                }
            }
            for (i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (j = 0; j < dataGridView1.RowCount; j++)
                {
                    var value = dataGridView1.Rows[j].Cells[i].Value;
                    if (value != null)
                    {
                        string baseStr = value.ToString();

                        if (baseStr.IndexOf(textBox1.Text) > -1)
                        {
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.AliceBlue;
                            dataGridView1.Rows[j].Cells[i].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }   // Функция поиска
        private void setTimer()
        {
            Timer t = new Timer();
            t.Interval = 7000;
            t.Tick += new EventHandler(showUpdate);
            t.Start();
        }   // Таймер формы обновления
        private void SetTimewWhatNewWindow()
        {
            Timer t = new Timer();
            t.Interval = 2000;
            t.Tick += new EventHandler(showWhatNewWindow);
            t.Start();
        }
        void showUpdate(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            scanUpdate();
        } // Автоматизация обновления
        void showWhatNewWindow(object senders, EventArgs e)
        {
            (senders as Timer).Stop();
            readSetting();
        }   // Что нового ?
        private void getUpdate()
        {
            Form3 Form3 = new Form3();
            Form3.Form1 = this;
            Form3.ShowDialog();
            this.Show();
        }  // Открытие формы обновления
        private void showWindowWN()
        {
            whatnew whatnew = new whatnew();
            whatnew.Form1 = this;
            whatnew.ShowDialog();
            this.Show();
        }
        private void scanUpdate()
        {
            string currentVersion = "232325";

            MySqlCommand command = new MySqlCommand(); ;
            string connectionString, commandString;
            connectionString = "Database= ;Data Source= ;User Id= ;Password= "; //Настройки соединения с MYSQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            commandString = "SELECT * FROM curentversion;";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
                List<string> codes = new List<string>();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["version"]);
                    //listBox1.Items.Add(reader["toversion"]);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message,"Ошибка");
            }

            if (listBox1.Items[0].ToString() == currentVersion)
            {
                
            }
            else
            {
                getUpdate();
            }
            

        } // Проверка обновления



        private void readSetting()
        {
            props.ReadXml();
            if (props.Fields.WhatNewWindow == true)
            {
                //MessageBox.Show("Ничего нового");
            }
            else
            {
                whatnew whatnew = new whatnew();
                whatnew.Form1 = this;
                whatnew.ShowDialog();
                this.Show();
            }
            
        }

        
// События

        private void Form1_Load(object sender, EventArgs e)
        {
            Synchronized();
            System.IO.File.Delete(@"sitelist.txt");
            var savedefault = new StreamWriter("sitelist.txt", false, Encoding.UTF8);
            foreach (DataGridViewRow row in dataGridView1.Rows) //запись
                if (!row.IsNewRow)
                {
                    var first = true;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!first) savedefault.Write(";");
                        savedefault.Write(cell.Value.ToString());
                        first = false;
                    }
                    savedefault.WriteLine();
                }
            savedefault.Close();

            setTimer();
            SetTimewWhatNewWindow();

        }   // Событие при загрузке формы
        private void button1_Click(object sender, EventArgs e)
        {
            Synchronized();
            System.IO.File.Delete(@"sitelist.txt");
            var savedefault = new StreamWriter("sitelist.txt", false, Encoding.UTF8);
            foreach (DataGridViewRow row in dataGridView1.Rows) //запись
                if (!row.IsNewRow)
                {
                    var first = true;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!first) savedefault.Write(";");
                        savedefault.Write(cell.Value.ToString());
                        first = false;
                    }
                    savedefault.WriteLine();
                }
            savedefault.Close();
        }   // Синхронизация
        private void button3_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }   // Сохранение в произвольный файл
        private void button5_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Close();
        }   //Закрыть

        //В трей
        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Form2 Form2 = new Form2();
            Form2.Form1 = this;
            Form2.ShowDialog();
            this.Show();

        }   //Администрирование открытие формы
        private void button4_Click_1(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                //e.Cancel = true;
                MessageBox.Show("Поисковый запрос не может быть пустым");
            }
            else
            {
                SearchFunc();
            }
        
        }   // Поиск по dataGridView

        // Перемещение формы
        private void dataGridView3_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X + 6;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        private void dataGridView3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }
        private void dataGridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.Form1 = this;
            settings.ShowDialog();
            this.Show();
        }   // Открытие окна настроек

        private void button8_Click(object sender, EventArgs e)
        {
            whatnew whatnew = new whatnew();
            whatnew.Form1 = this;
            whatnew.ShowDialog();
            this.Show();
        }



       

 
       
}       
}
