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

namespace FINALMENT_proj
{
    public partial class Chef_Menu : Form
    {
        public User currentUser;
        public Chef_Menu(User currentuser)
        {
            InitializeComponent();

            currentUser = currentuser;

                // Create a User object for the current Manager

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT FoodID, Name FROM [Menu] ", conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("Menu");

            sda.Fill(dt);

            listMenu.DataSource = dt;
            listMenu.DisplayMember = "Name";
            listMenu.ValueMember = "FoodID";

            conn.Close();

            SqlConnection connt = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            conn.Open();
            SqlCommand cmnd = new SqlCommand("SELECT OrderID, username FROM Orders where status = 'waiting' or status = 'in progress'", connt);

            SqlDataAdapter sdap = new SqlDataAdapter(cmnd);

            DataTable dtbl = new DataTable("Orders");

            sdap.Fill(dtbl);

            listOrder.DataSource = dtbl;
            listOrder.DisplayMember = "Name";
            listOrder.ValueMember = "OrderID";

            conn.Close();

            Refresh(conn);
            conn.Close();
        }


        public void Refresh(SqlConnection conn)
        {
            using (SqlConnection _conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True"))
            {
                _conn.Open();
                string query = "Select OrderID, components, name, status from Orders inner join Users on Users.username = Orders.username where status = 'waiting' or status = 'in progress'";
                using (SqlCommand _cmd = new SqlCommand(query, _conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(_cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
                _conn.Close();
            }


            SqlConnection connt = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            conn.Open();
            SqlCommand cmnd = new SqlCommand("SELECT OrderID, username FROM Orders where status = 'waiting' or status = 'in progress'", connt);

            SqlDataAdapter sdap = new SqlDataAdapter(cmnd);

            DataTable dtbl = new DataTable("Orders");

            sdap.Fill(dtbl);

            listOrder.DataSource = dtbl;
            listOrder.DisplayMember = "Name";
            listOrder.ValueMember = "OrderID";
            conn.Close();
            connt.Close();
        }

        private void Chef_Menu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sedapMakanDataSet.Menu' table. You can move, or remove it, as needed.
            this.menuTableAdapter.Fill(this.sedapMakanDataSet.Menu);

        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            Edit_Profile nef = new Edit_Profile(currentUser);
            nef.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            Add_Menu nef = new Add_Menu();
            nef.ShowDialog();
        }

        private void btnUpdateMenuItem_Click(object sender, EventArgs e)
        {
            int empid = int.Parse(listMenu.SelectedValue.ToString());

            SqlConnection conn = new SqlConnection("Data Source=MSI/SQLEXPRESS;Initial " +
                "               Catalog=SedapMakan;Integrated Security=True");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT FoodID, Name, Description, Price, Availability " +
                "           Position FROM [Menu] Where FoodID = @FoodID", conn);
            cmd.Parameters.AddWithValue("@FoodID", empid);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);

            int FoodID = dt.Rows[0].Field<int>("FoodID");
            string Name = dt.Rows[0].Field<string>("Name");
            string Description = dt.Rows[0].Field<string>("Description");
            float Price = dt.Rows[0].Field<float>("Price");
            bool Availability = dt.Rows[0].Field<bool>("Availability");

            Menu emp = new Menu(FoodID, Name, Description, Price, Availability);
            Update_Menu uef = new Update_Menu(emp);
            uef.ShowDialog();

            conn.Close();

        }

        private void btnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            // Confirm deletion with user
            DialogResult result = MessageBox.Show("Are you sure you want to delete this menu item?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Get selected FoodID from listMenu
                    int empid = int.Parse(listMenu.SelectedValue.ToString());

                    // Connection string - ideally move to App.config or static class
                    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // SQL DELETE command
                        string query = "DELETE FROM Menu WHERE FoodID = @FoodID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@FoodID", empid);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Menu item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Optional: Refresh listMenu or reload data here
                            }
                            else
                            {
                                MessageBox.Show("No item found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMarkInProgress_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected OrderID from listOrder
                string selectedOrderID = listOrder.SelectedValue.ToString();

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL UPDATE command to set Status = 'In Progress'
                    string query = "UPDATE Orders SET status = @Status WHERE OrderID = @OrderID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "in Progress");
                        cmd.Parameters.AddWithValue("@OrderID", selectedOrderID);
                        cmd.ExecuteNonQuery();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order status updated successfully.",
                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optional: Refresh the list to reflect changes
                            //LoadOrders(); // Replace with your actual method to reload orders
                        }
                        else
                        {
                            MessageBox.Show("No order found with the given ID.",
                                            "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    conn.Close();
                    Refresh(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected OrderID from listOrder
                string selectedOrderID = listOrder.SelectedValue.ToString();

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL UPDATE command to set Status = 'In Progress'
                    string query = "UPDATE Orders SET status = @Status WHERE OrderID = @OrderID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "done");
                        cmd.Parameters.AddWithValue("@OrderID", selectedOrderID.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order status updated successfully.",
                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optional: Refresh the list to reflect changes
                            //LoadOrders(); // Replace with your actual method to reload orders
                        }
                        else
                        {
                            MessageBox.Show("No order found with the given ID.",
                                            "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    conn.Close();
                    Refresh(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

