using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FINALMENT_proj
{
    public partial class Customer : Form
    {
        Random rand = new Random();
        List<string> chefs = new List<string>();
        string connectionString = "Server=DESKTOP-BFNOIDM\\SQLEXPRESS01;Database=oop ga;Trusted_Connection=True;";
        public User currentUser;
        public double balance;
        public string generatedOrderID;
        public Customer(User CurrentUser)
        {
            InitializeComponent();

            // Removed the duplicate CellContentClick event handler
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            dataGridView1.CellClick += dataGridView1_CellClick;

            currentUser = CurrentUser;
            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select Name,Description,Price from Menu where Availability = 1";
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                table.Columns.Add("Quantity", typeof(int));
                foreach (DataRow row in table.Rows)
                {
                    row["Quantity"] = 0;
                }
                dataGridView1.DataSource = table;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Name != "Quantity")
                    {
                        column.ReadOnly = true;
                    }
                    else
                    {
                        column.ReadOnly = false; // Just to be explicit
                    }
                }
            }
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewButtonColumn btnDecrease = new DataGridViewButtonColumn();
            btnDecrease.Name = "Decrease";
            btnDecrease.HeaderText = "";
            btnDecrease.Text = "◀️";
            btnDecrease.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnDecrease);


            DataGridViewButtonColumn btnIncrease = new DataGridViewButtonColumn();
            btnIncrease.Name = "Increase";
            btnIncrease.HeaderText = "";
            btnIncrease.Text = "▶️";
            btnIncrease.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnIncrease);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[4].Value = 0;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select name from Users where permissions = 'chef'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chefs.Add(reader.GetString(0));
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT balance FROM Users WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            balance = Convert.ToDouble(reader.GetDecimal(0));
                        }
                    }
                }
            }

            label2.Text = label2.Text + balance.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select OrderID, full_price, components, status from Orders where username = @username and status = 'waiting'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            label3.Text = label3.Text + reader.GetString(0);
                            label4.Text = label4.Text + reader.GetDouble(1).ToString();
                            label11.Text = label11.Text + reader.GetString(2);
                            label12.Text = label12.Text + reader.GetString(3);
                        }
                    }
                }
            }

            if (label3.Text != "Order ID: ")
            {
                button2.Visible = true;
                button1.Visible = false;
            }
            Relod_Tables();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT OrderID FROM Orders WHERE username = @username and status = 'done'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", currentUser.username);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["OrderID"].ToString());
                }

                reader.Close();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select OrderID from Orders where username = @username and status = 'waiting'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", currentUser.username);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    generatedOrderID = reader.GetString(0);
                }

                reader.Close();
            }

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

        }

        private void Relod_Tables()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FeedbackText AS 'Feedback', ManagerResponse AS 'Response', CreatedAt AS Time FROM Feedback WHERE username = @username";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Reason AS Request, Amount, Status, ReviewedAt AS 'Time of review' FROM RefundRequests WHERE username = @username";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView3.DataSource = table;
                }

            }
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = dataGridView1;

            if (grid.Columns[e.ColumnIndex].Name == "Decrease")
            {
                int current = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["Quantity"].Value);
                grid.Rows[e.RowIndex].Cells["Quantity"].Value = Math.Max(0, current - 1);
            }
            else if (grid.Columns[e.ColumnIndex].Name == "Increase")
            {
                int current = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["Quantity"].Value);
                grid.Rows[e.RowIndex].Cells["Quantity"].Value = current + 1;
            }
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["Quantity"].Index)
            {
                System.Windows.Forms.TextBox textBox = e.Control as System.Windows.Forms.TextBox;
                if (textBox != null)
                {

                    textBox.KeyPress -= Quantity_KeyPress;
                    textBox.KeyPress += Quantity_KeyPress;
                }
            }
        }

        // Removed the duplicate dataGridView1_CellContentClick method


        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void OnlyAllowDigits(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Quantity")
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    cell.Value = 0;
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            bool ordered = false;
            List<string> orderItems = new List<string>();
            double totalPrice = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                int quantity = 0;
                if (row.Cells["Quantity"].Value != DBNull.Value && row.Cells["Quantity"].Value != null)
                {
                    int.TryParse(row.Cells["Quantity"].Value.ToString(), out quantity);
                }

                if (quantity > 0)
                {
                    string itemName = row.Cells["Name"].Value.ToString(); // column: "Name"
                    double price = Convert.ToDouble(row.Cells["Price"].Value); // column: "Price"

                    orderItems.Add($"{quantity}x{itemName}");
                    totalPrice += quantity * price;
                    ordered = true;
                }
            }

            if (orderItems.Count == 0)
            {
                MessageBox.Show("No items selected.");
                return;
            }

            string orderText = string.Join(", ", orderItems);

            if (ordered)
            {
                if (balance >= totalPrice)
                {

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"
        INSERT INTO Orders (username, components, chef, full_price, status)
        OUTPUT INSERTED.OrderID
        VALUES (@username, @order, @chef, @total, 'waiting')";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", currentUser.username);
                            cmd.Parameters.AddWithValue("@order", orderText);
                            cmd.Parameters.AddWithValue("@chef", (string)chefs[rand.Next(chefs.Count)]);
                            cmd.Parameters.AddWithValue("@total", totalPrice);

                            generatedOrderID = (string)cmd.ExecuteScalar();
                        }
                    }
                    MessageBox.Show("Order made");
                    button2.Visible = true;
                    button1.Visible = false;
                    double price = 0;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "Select OrderID, full_price, components, status from Orders where username = @username and status = 'waiting'";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", currentUser.username);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    label3.Text = label3.Text + reader.GetString(0);
                                    price = reader.GetDouble(1);
                                    label4.Text = label4.Text + reader.GetDouble(1).ToString();
                                    label11.Text = label11.Text + reader.GetString(2);
                                    label12.Text = label12.Text + reader.GetString(3);
                                }
                            }
                        }
                    }
                    balance = balance - price;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "update Users set balance = @balance where username = @username";
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", currentUser.username);
                            cmd.Parameters.AddWithValue("@balance", balance);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    label2.Text = "Balance: " + balance.ToString();
                }
                else
                {
                    MessageBox.Show("No sufficient funds");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "OrderID: ";
            label4.Text = "Total Price: ";
            label11.Text = "Order: ";
            label12.Text = "Status: ";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "update Orders set status = 'cancelled' where OrderID = @orderID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderID", generatedOrderID);
                    cmd.ExecuteNonQuery();
                }
            }
            double price = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select full_price from Orders where OrderID = @orderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderID", generatedOrderID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            price = reader.GetDouble(0);
                        }
                    }
                }
            }
            balance = balance + price;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "update Users set balance = @balance where username = @username";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.ExecuteNonQuery();
                }
            }
            label2.Text = "Balance: " + balance.ToString();
            MessageBox.Show("Cancelled Order");
            button2.Visible = false;
            button1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Insert into Feedback (username, FeedbackText) values (@username, @feedback)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    cmd.Parameters.AddWithValue("@feedback", textBox1.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Thank you for your feedback");
            Relod_Tables();
        }
        double price = 0;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderID = comboBox2.SelectedItem.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select full_price, components, time from Orders where OrderID = @orderid and status = 'done'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderid", orderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            price = reader.GetDouble(0);
                            label15.Text = label15.Text + reader.GetString(1);
                            label16.Text = label16.Text + reader.GetDateTime(2);

                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Invalid Values");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Insert into RefundRequests (username, Amount, Reason) values (@username, @ammount, @reason)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", currentUser.username);
                        cmd.Parameters.AddWithValue("@ammount", price);
                        cmd.Parameters.AddWithValue("@reason", textBox2.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Sorry for the inconvinece, we will get to you as soon as possible");
                Relod_Tables();
            }
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            this.Close();
        }
    }
}