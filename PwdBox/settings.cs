using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PwdBox
{
    public partial class settings : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;
        public Form1 Form1;

        Props props = new Props();
        //Form1 Form1 = new Form1();
        password password = new password();
        private void writeSetting()
        {
            //props.Fields.TextValue = textBox1.Text;
            props.Fields.AuthorizedToStart = checkBox1.Checked;
            props.WriteXml();
        }
        private void readSetting()
        {
            props.ReadXml();
            //textBox1.Text = props.Fields.TextValue;
            checkBox1.Checked = props.Fields.AuthorizedToStart;
        }


       
        public void SelectStartPassword()
        {
            readSetting();
            if (props.Fields.AuthorizedToStart = checkBox1.Checked)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(password);
                if (password.DialogResult == DialogResult.OK)
                    Application.Run(new Form1());
            }
        }   // Выводим или не выводим окно авторизации
        public settings()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            writeSetting();
        }   //Сохраняем настройки
        private void settings_Load(object sender, EventArgs e)
        {
            readSetting();
        }   // Проверяем настройки
        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }   // Закрываем форму

        // Перемещение окна
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X + 1;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }   
        

    }
}
