using FINALMENT_proj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINALMENT_proj
{
    public partial class Welcome : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        public Welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int username;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Select COUNT(*) from Users where username = @username";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", textBox4.Text);
                    username = (int)cmd.ExecuteScalar();
                }
            }
            if (username > 0  || textBox4.Text == "")
            {
                MessageBox.Show("Invalid Username");
            }
            else
            {
                if (textBox3.Text != textBox5.Text)
                {
                    MessageBox.Show("Passwords don't match, retry!");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string sqlQuery = "Insert into Users (name, username, password, permissions) values (@name, @username, @password, 'customer')";
                        using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", textBox6.Text);
                            cmd.Parameters.AddWithValue("@username", textBox4.Text);
                            cmd.Parameters.AddWithValue("@password", textBox3.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Successfully created account");
                    tabControl1.SelectTab(tabPage1);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string password;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Select password FROM Users Where Username = @username";
                using ( SqlCommand cmd = new SqlCommand( sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    object result = cmd.ExecuteScalar();
                    password = result != null ? result.ToString(): null;
                }
            }
            if (textBox2.Text == password)
            {
                String name;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Select name FROM Users Where Username = @username";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        object result = cmd.ExecuteScalar();
                        name = result != null ? result.ToString() : null;
                    }
                }
                String permissions;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Select permissions FROM Users Where Username = @username";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        object result = cmd.ExecuteScalar();
                        permissions = result != null ? result.ToString() : null;
                    }
                }
                
                User currentUser = new User(textBox1.Text, name, permissions);
                switch (currentUser.permissions)
                {
                    case "admin":
                        Admin admin = new Admin(currentUser);
                        this.Hide();
                        admin.ShowDialog();
                        this.Close();
                        break;
                    case "customer":
                        Customer customer = new Customer(currentUser);
                        this.Hide();
                        customer.ShowDialog();
                        this.Close();
                        break;
                    case "manager":
                        ManagerForm manager = new ManagerForm(currentUser);
                        this.Hide();
                        manager.ShowDialog();
                        this.Close();
                        break;
                    case "chef":
                        Chef_Menu chef = new Chef_Menu(currentUser);
                        this.Hide();
                        chef.ShowDialog();
                        this.Close();
                        break;

                }
                if (currentUser.permissions == "admin")
                {
                    
                    
                }
            }
            else
            {
                MessageBox.Show("Username or password incorrect");
            }
        }

    }
}
