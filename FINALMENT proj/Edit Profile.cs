using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FINALMENT_proj
{
    public partial class Edit_Profile : Form
    {
        public User currentUser;
        public string connectionString = "Server=DESKTOP-BFNOIDM\\SQLEXPRESS01;Database=oop ga;Trusted_Connection=True;";
        public Edit_Profile(User currentuser)
        {
            InitializeComponent();
            currentUser = currentuser;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select Bio from Users where Username = @username";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    object result = cmd.ExecuteScalar();
                    richTextBox3.Text = result != null ? result.ToString() : null;
                }
            }

            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            richTextBox3.ReadOnly = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string originalUsername = currentUser.username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Update Users Set name= @name, username = @username, bio=@bio  where username = @original;";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox6.Text);
                    cmd.Parameters.AddWithValue("@username", textBox7.Text);
                    cmd.Parameters.AddWithValue("@bio", richTextBox3.Text);
                    cmd.Parameters.AddWithValue("@original", originalUsername);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Updated Values");

            button5.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox3.ReadOnly = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string password;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Select password FROM Users Where Username = @username";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    object result = cmd.ExecuteScalar();
                    password = result != null ? result.ToString() : null;
                }
            }
            if ((password == textBox3.Text) && (textBox4.Text == textBox5.Text))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "Update Users Set password = @password  where username = @original;";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@password", textBox4.Text);
                        cmd.Parameters.AddWithValue("@original", currentUser.username);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Updated password");
            }
            else if (password != textBox3.Text)
            {
                MessageBox.Show("Incorrect password");
            }
            else
            {
                MessageBox.Show("Passwords don't match");
            }

        }
    }
}