using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FINALMENT_proj
{
    public partial class Admin : Form
    {
        string connectionString = "Server=DESKTOP-BFNOIDM\\SQLEXPRESS01;Database=oop ga;Trusted_Connection=True;";
        public User currentUser;
        public Admin(User CurrentUser)
        {
            InitializeComponent();
            currentUser = CurrentUser;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select Name, Username, permissions as Type, Balance, Bio from Users";
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select Bio from Users where Username = @username";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    object result = cmd.ExecuteScalar();
                    richTextBox2.Text = result != null ? result.ToString() : null;
                }
            }

            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select OrderID, chef, components, full_price, CAST(time as date) as date from Orders where status = 'done'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select username, Amount, CAST(TopUpDate as date) as date from TopUpHistory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView3.DataSource = table;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select year(time) as years from Orders group by year(time)";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(reader["years"].ToString());
                }

                reader.Close();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select chef from Orders group by chef";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox4.Items.Add(reader["chef"].ToString());
                }

                reader.Close();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select year(TopUpDate) as years from TopUpHistory group by year(TopUpDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox6.Items.Add(reader["years"].ToString());
                }

                reader.Close();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select username, permissions from Users where permissions = 'customers'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox5.Items.Add(reader["username"].ToString());
                }

                reader.Close();
            }
        }
        private void ReloadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select Name, Username, permissions as Type, Balance, Bio from Users";
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        string originalUsername;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                originalUsername = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox1.SelectedItem = row.Cells[2].Value.ToString();
                richTextBox1.Text = row.Cells[4].Value.ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Update Users Set name= @name, username = @username, permissions=@type, bio=@bio  where username = @original;";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@username", textBox2.Text);
                    cmd.Parameters.AddWithValue("@type", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@bio", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@original", originalUsername);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Updated Values");
            ReloadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Select COUNT(*) from Users where username = @username";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", textBox2.Text);
                    username = (int)cmd.ExecuteScalar();
                }
            }
            if (username > 0)
            {
                MessageBox.Show("Invalid Username");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Insert into Users (name, username, password, permissions, bio) values (@name, @username, @password, @permissions, @bio)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@username", textBox2.Text);
                        cmd.Parameters.AddWithValue("@password", textBox2.Text + "1234");
                        cmd.Parameters.AddWithValue("@permissions", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@bio", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Successfully created account");
                tabControl1.SelectTab(tabPage1);
                ReloadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Delete from Users where username = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", originalUsername);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Successfully deleted User");
                ReloadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            richTextBox2.ReadOnly = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            originalUsername = currentUser.username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Update Users Set name= @name, username = @username, bio=@bio  where username = @original;";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox6.Text);
                    cmd.Parameters.AddWithValue("@username", textBox7.Text);
                    cmd.Parameters.AddWithValue("@bio", richTextBox2.Text);
                    cmd.Parameters.AddWithValue("@original", originalUsername);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Updated Values");

            button5.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox2.ReadOnly = true;

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
                    string query = "Update Users password = @password  where username = @original;";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@password", textBox4.Text);
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

        private void button7_Click(object sender, EventArgs e)
        {
            string query = "Select OrderID, chef, components, full_price, CAST(time as date) as date from Orders where status = 'done'";

            if (comboBox2.SelectedIndex > -1)
            {
                query = query + " and month(time) = " + (comboBox2.SelectedIndex + 1);
            }

            if (comboBox3.SelectedIndex > -1)
            {
                query = query + " and year(time) = " + comboBox3.SelectedItem.ToString();
            }

            if (comboBox4.SelectedIndex > -1)
            {
                query = query + " and chef = " + "'" + comboBox4.SelectedItem.ToString() + "'";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "Select username, Amount, CAST(TopUpDate as date) as date from TopUpHistory where 1 = 1";

            if (comboBox7.SelectedIndex > -1)
            {
                query = query + " and month(TopUpDate) = " + (comboBox7.SelectedIndex + 1);
            }

            if (comboBox6.SelectedIndex > -1)
            {
                query = query + " and year(TopUpDate) = " + comboBox6.SelectedItem.ToString();
            }

            if (comboBox5.SelectedIndex > -1)
            {
                query = query + " and username = " + "'" + comboBox5.SelectedItem.ToString() + "'";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView3.DataSource = table;
            }
        }
        private void button9_Click(object sender, EventArgs e)
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
    }
}