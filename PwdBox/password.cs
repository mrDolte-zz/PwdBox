using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace PwdBox
{
    public partial class password : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;
        public password()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
         //
            MySqlCommand command = new MySqlCommand();;
            string connectionString, commandString;
            connectionString = "Database=;Data Source=;User Id=;Password=";//Настройки соединения с MYSQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            commandString = "SELECT * FROM user;";
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
                    listBox1.Items.Add(reader["name"]);
                    listBox1.Items.Add(reader["password"]);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка");
            }
            if (textBox2.Text != listBox1.Items[0].ToString() || textBox1.Text != listBox1.Items[1].ToString()) MessageBox.Show("Не верный логин или пароль");
            else
            {
                this.DialogResult=DialogResult.OK;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X + 11;
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
