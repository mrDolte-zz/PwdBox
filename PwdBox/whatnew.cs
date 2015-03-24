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
    public partial class whatnew : Form
    {
        settings settings = new settings();
        Props props = new Props();
        public Form1 Form1;
        public whatnew()
        {
            InitializeComponent();
        }
        public void writeSetting()
        {
            props.Fields.WhatNewWindow = checkBox1.Checked;
            props.WriteXml();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            writeSetting();
            Close();
        }
        private void readSetting()
        {
            props.ReadXml();
            checkBox1.Checked = props.Fields.WhatNewWindow;
            
        }

        private void whatnew_Load(object sender, EventArgs e)
        {
            string fixedText =
            "Версия: 2.3.23.25. \r\n\r\n"
            + "* Добавлены настройки. \r\n"
            + "** Добавлена возможность запуска программы без авторизации. \r\n"
            + "* Добавлены новые поля информации. \r\n"
            + "* Зашифрован код, сломать его довольно трудно, но возможно.";

            readSetting();
            textBox1.Text = fixedText;
        }
    }
}
