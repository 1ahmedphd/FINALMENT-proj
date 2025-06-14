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
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
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
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (textBox3.Text == textBox4.Text && textBox4.Text == textBox5.Text)
            {
                MessageBox.Show("Old password cannot be the same as new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                string password;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Select password from Users Where Username = @username";
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
                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else if (password != textBox3.Text)
                {
                    MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Are you sure you want to log out?",
    "Confirm Logout",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question
);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                Welcome welcome = new Welcome();
                welcome.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select bio from Users where username = @username";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    object result = cmd.ExecuteScalar();
                    richTextBox3.Text = result != null ? result.ToString() : null;
                }
            }
            button5.Visible = false;
            button2.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox3.ReadOnly = true;
        }
    }
}