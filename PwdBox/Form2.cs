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
    
    public partial class Form2 : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;

        public Form1 Form1;

        public Form2()
        {
            InitializeComponent();
        }


        private void MySqlAddRow()
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                //e.Cancel = true;
                MessageBox.Show("Поле 'Название' не может быть пустым");
            }
            else if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Поле 'Ссылка' не может быть пустым");
            }
            else
            {
                string conStr = "server=;user=; database=;password=;";//Настройки соединения с MYSQL

                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    try
                    {
                        string sql = "INSERT INTO `sites` (`name`, `url`, `urlcp`, `logincp`, `pwdcp`, `hostftp`, `loginftp`, `pwdftp`, `hosting`, `hostinglogin`, `hostingpwd` )" +
                                     "VALUES (" + "'" + textBox1.Text + "'" + ", " + "'" + textBox2.Text + "'" + ", " + "'" + textBox3.Text + "'" + ", " + "'" + textBox4.Text + "'" + ", " + "'" + textBox5.Text + "'" + ", " + "'" + textBox6.Text + "'" + ", " + "'" + textBox7.Text + "'" + ", " + "'" + textBox8.Text + "'" + ", " + "'" + textBox10.Text + "'" + ", " + "'" + textBox11.Text + "'" + ", " + "'" + textBox12.Text + "'" + " )";

                        MySqlCommand cmd = new MySqlCommand(sql, con);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Данные обновлены");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }

                }
            }
        }
        private void MySqlDelRow()
        {
            string conStr = "server=;user=; database=;password=;";//Настройки соединения с MYSQL

            if (String.IsNullOrWhiteSpace(textBox9.Text))
            {
                //e.Cancel = true;
                MessageBox.Show("Укажите ID связки!");
            }
            else
            {

                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    try
                    {

                        string sql = "DELETE FROM sites " +
                                     "WHERE id = " + textBox9.Text;

                        MySqlCommand cmd = new MySqlCommand(sql, con);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Данные обновлены");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            MySqlAddRow();
            Form1.DataGridUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlDelRow();
            Form1.DataGridUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X + 10;
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

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true; 
        }


        






    }
}
