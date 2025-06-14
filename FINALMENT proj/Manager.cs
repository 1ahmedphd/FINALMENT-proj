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
    public partial class ManagerForm : Form
    {
        public string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        private ManagerLogic _managerLogic; // Instance of the ManagerLogic class
        public User currentUser;
        public ManagerForm(User currentuser)
        {
            InitializeComponent();
            // TODO: This line of code loads data into the 'iOOPGADataSet.Feedback' table. You can move, or remove it, as needed.
            this.feedbackTableAdapter.Fill(this.iOOPGADataSet.Feedback);
            // TODO: This line of code loads data into the 'iOOPGADataSet.RefundRequests' table. You can move, or remove it, as needed.
            this.refundRequestsTableAdapter.Fill(this.iOOPGADataSet.RefundRequests);
            _managerLogic = new ManagerLogic(_connectionString); // Initialize the ManagerLogic instance
            LoadFeedback(); // Load new feedback when the form loads
            LoadRefunds(); // Load refund requests when the form loads
            LoadUsernames(); // Populate the ListBox for the usernames to top up
            #region update manager profile
            currentUser = currentuser;            
            lblWelcomeManager.Text = lblWelcomeManager.Text + currentUser.name;
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            #endregion
        }

        #region Feedback Handling

        /// <summary>
        /// Loads new feedback into the DataGridView.
        /// </summary>
        private void LoadFeedback()
        {
            try
            {
                // Determine the filter based on the selected radio button
                string filter;
                if (rbUnresponded.Checked)
                {
                    filter = "unresponded";
                }
                else if (rbResponded.Checked)
                {
                    filter = "responded";
                }
                else
                {
                    filter = "all";
                }

                // Fetch feedback based on the filter
                List<Feedback> feedbackList = _managerLogic.GetFeedback(filter);

                // Bind the data to the DataGridView
                dataGridViewFeedback.DataSource = feedbackList;

                // Adjust column widths for better readability
                dataGridViewFeedback.Columns[0].Width = 75;     // FeedbackID
                dataGridViewFeedback.Columns[1].Width = 100;    // Customer   
                dataGridViewFeedback.Columns[2].Width = 250;    // FeedbackText
                dataGridViewFeedback.Columns[3].Width = 250;    // ManagerResponse
                dataGridViewFeedback.Columns[4].Width = 125;    // CreatedAt
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading feedback: {ex.Message}");
            }
        }
        private void rbUnresponded_CheckedChanged(object sender, EventArgs e)
        {
            LoadFeedback();
        }
        private void rbResponded_CheckedChanged(object sender, EventArgs e)
        {
            LoadFeedback();
        }
        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadFeedback();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFeedback();
        }

        /// <summary>
        /// Handles the button click event to respond to feedback.
        /// </summary>
        private void btnRespond_Click(object sender, EventArgs e)
        {
            // Validate if at least one row is selected
            if (dataGridViewFeedback.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one feedback to respond.");
                return;
            }

            try
            {
                bool hasResponded = false;

                // Check the status of all selected rows
                foreach (DataGridViewRow selectedRow in dataGridViewFeedback.SelectedRows)
                {
                    int isResponded = 0;
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        string query = "SELECT IsResponded FROM Feedback WHERE FeedbackID = @feedbackid";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        conn.Open();
                        cmd.Parameters.AddWithValue("@feedbackid", selectedRow.Cells[0].Value);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            isResponded = Convert.ToInt32(reader["IsResponded"]);
                        }
                    }

                    if (isResponded == 1)
                    {
                        hasResponded = true;
                    }
                }

                // If any selected feedback has already been responded to, show a message and stop
                if (hasResponded)
                {
                    MessageBox.Show("One or more selected feedbacks have already been responded to.");
                    return;
                }

                // Get the manager's response from the textbox
                string response = txtManagerResponse.Text.Trim();

                // Validate input
                if (string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show("Please enter a response.");
                    return;
                }

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to submit this response to the selected feedback(s)?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Iterate through all selected rows
                    foreach (DataGridViewRow selectedRow in dataGridViewFeedback.SelectedRows)
                    {
                        int feedbackID = Convert.ToInt32(selectedRow.Cells[0].Value);
                        //int isResponded = Convert.ToInt32(selectedRow.Cells[4].Value);

                        // Update the selected row in the DataGridView
                        selectedRow.Cells[3].Value = response;
                        //selectedRow.Cells[4].Value = 1;

                        // Call the backend method to update the database
                        _managerLogic.RespondToFeedback(feedbackID, response);
                    }

                    // Reload feedback to reflect changes
                    LoadFeedback();

                    // Clear the response textbox
                    txtManagerResponse.Clear();

                    MessageBox.Show("All feedback responses submitted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error responding to feedback: {ex.Message}");
            }
        }

        #endregion

        #region Refund Requests Handling

        /// <summary>
        /// Unchecks radio buttons to approve or reject refund requests
        /// </summary>
        private void UncheckRefundRbs() 
        {
            rbApprove.Checked = false;
            rbReject.Checked = false;
        }

        /// <summary>
        /// Loads refund requests into the DataGridView.
        /// </summary>
        private void LoadRefunds()
        {
            try
            {
                string filter;
                if (rbPending.Checked)
                {
                    filter = "pending";
                    UncheckRefundRbs();
                }
                else if (rbApproved.Checked)
                {
                    filter = "approved";
                    UncheckRefundRbs();
                }
                else if (rbRejected.Checked)
                {
                    filter = "rejected";
                    UncheckRefundRbs();
                }
                else
                {
                    filter = "all";
                    UncheckRefundRbs();
                }

                List<RefundRequest> refundList = _managerLogic.GetRefundRequest(filter);

                // Bind the data to the DataGridView
                dataGridViewRefunds.DataSource = refundList;

                // Adjust column widths for better readability
                dataGridViewRefunds.Columns[0].Width = 75;  // RequestID
                dataGridViewRefunds.Columns[1].Width = 100; // Customer
                dataGridViewRefunds.Columns[2].Width = 75;  // Amount
                dataGridViewRefunds.Columns[3].Width = 200; // Reason
                dataGridViewRefunds.Columns[4].Width = 75;  // Status
                dataGridViewRefunds.Columns[5].Width = 120; // ReviewedAt
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading refund requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbPending_CheckedChanged(object sender, EventArgs e)
        {
            LoadRefunds();
        }
        private void rbApproved_CheckedChanged(object sender, EventArgs e)
        {
            LoadRefunds();
        }
        private void rbRejected_CheckedChanged(object sender, EventArgs e)
        {
            LoadRefunds();
        }
        private void rbAll2_CheckedChanged(object sender, EventArgs e)
        {
            LoadRefunds();
        }
        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            LoadRefunds();
        }

        /// <summary>
        /// Handles the button click event to approve or reject a refund request.
        /// </summary>
        private void btnUpdateRefund_Click(object sender, EventArgs e)
        {
            // Validate if at least one row is selected
            if (dataGridViewRefunds.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one refund request to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                bool hasProcessed = false;

                // Check the status of all selected rows
                foreach (DataGridViewRow selectedRow in dataGridViewRefunds.SelectedRows)
                {
                    string currentStatus = selectedRow.Cells[4].Value?.ToString();

                    if (currentStatus == "Approved" || currentStatus == "Rejected")
                    {
                        hasProcessed = true;
                    }
                }

                // If any selected refund request has already been updated, show a message and stop
                if (hasProcessed)
                {
                    MessageBox.Show("One or more selected refund requests have already been updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Validate the status selection
                if (!rbApprove.Checked && !rbReject.Checked)
                {
                    MessageBox.Show("Please select either Approve or Reject.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Determine the status based on the selected radio button
                string status = rbApprove.Checked ? "Approved" : "Rejected";

                // Show confirmation dialog
                string confirmationMessage = rbApprove.Checked
                    ? "Are you sure you want to APPROVE the selected refund request(s)?"
                    : "Are you sure you want to REJECT the selected refund request(s)?";

                DialogResult result = MessageBox.Show(
                    confirmationMessage,
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Iterate through all selected rows
                    foreach (DataGridViewRow selectedRow in dataGridViewRefunds.SelectedRows)
                    {
                        int requestID = Convert.ToInt32(selectedRow.Cells[0].Value);
                        string username = selectedRow.Cells[1].Value?.ToString();
                        decimal amount = Convert.ToDecimal(selectedRow.Cells[2].Value);

                        // Update the selected row in the DataGridView
                        selectedRow.Cells[4].Value = status;
                        selectedRow.Cells[5].Value = DateTime.Now;

                        // Call the backend method to update the refund request
                        _managerLogic.UpdateRefundRequest(requestID, status, username, amount, rbApprove.Checked);
                    }

                    // Reload refunds to reflect changes
                    LoadRefunds();

                    MessageBox.Show("Refund request(s) updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating refund request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region E-Wallet Top-Up Handling

        /// <summary>
        /// Populates the ListBox to select a customer to top up their e-wallet.
        /// </summary>
        private void LoadUsernames()
        {
            try
            {
                // Retrieve customer usernames using ManagerLogic
                List<string> usernames = _managerLogic.GetCustomerUsernames();

                // Populate the ComboBox
                lbUsername.Items.Clear(); // Clear existing items
                foreach (string username in usernames)
                {
                    lbUsername.Items.Add(username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading usernames: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lbUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected username from the ListBox
                string selectedUsername = lbUsername.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedUsername))
                {
                    // Fetch the name corresponding to the selected username
                    string name = _managerLogic.GetUserName(selectedUsername);
                    string balance = _managerLogic.GetBalance(selectedUsername);

                    // Update the Name label
                    lblName2.Text = name;
                    lblBalance2.Text = balance;
                }
                else
                {
                    lblName2.Text = string.Empty; // Clear the label if no username is selected
                    lblBalance2.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearTopUp()
        {
            // Clear the selected item in the ComboBox
            lbUsername.SelectedIndex = -1; // Deselect any selected item

            // Reset the lblName2 label to its default state
            lblName2.Text = "N/A"; // Set it to placeholder "N/A"

            // Reset the lblBalance2 label to its default state
            lblBalance2.Text = "N/A"; // Set it to placeholder "N/A"

            // Clear the typed top up amount in the textbox
            txtAmount.Text = string.Empty; //Delete any value
        }

        /// <summary>
        /// Handles the button click event to top up a customer's e-wallet.
        /// </summary>
        private void btnTopUp_Click(object sender, EventArgs e)
        {
            string username = lbUsername.SelectedItem?.ToString();
            string name = lblName2.Text;
            decimal amount;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please select a valid username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Show confirmation dialog
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to top up RM {amount} to the selected user ({username} - {name})?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Call the backend method
                    _managerLogic.TopUpEwallet(username, amount);

                    ClearTopUp();

                    MessageBox.Show($"Successfully topped up RM {amount} for user {username}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error topping up e-wallet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTopUp();
        }

        #endregion


        private void ManagerForm_Load(object sender, EventArgs e)
        {
            // Initialization logic can go here if needed
        }

        private void dataGridViewFeedback_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // TODO: This line of code loads data into the 'iOOPGADataSet.Feedback' table. You can move, or remove it, as needed.
            this.feedbackTableAdapter.Fill(this.iOOPGADataSet.Feedback);
        }

        private void dataGridViewRefunds_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // TODO: This line of code loads data into the 'iOOPGADataSet.RefundRequests' table. You can move, or remove it, as needed.
            this.refundRequestsTableAdapter.Fill(this.iOOPGADataSet.RefundRequests);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button2.Visible = true;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            richTextBox3.ReadOnly = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string originalUsername = currentUser.username;
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            MessageBox.Show("Profile Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            button5.Visible = false;
            button2.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox3.ReadOnly = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string password;
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
                using (SqlConnection conn = new SqlConnection(_connectionString))
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
                MessageBox.Show("Updated password", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (password != textBox3.Text)
            {
                MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Passwords don't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void dataGridViewFeedback_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure the click is on a valid cell (not header row)
            try
            {
                string cellValue = dataGridViewRefunds.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (e.RowIndex >= 0 && (e.ColumnIndex == 3) && (cellValue != "" || cellValue != null))
                {
                    MessageBox.Show(cellValue);
                }
            }
            catch
            { }
        }

        private void txtManagerResponse_Enter(object sender, EventArgs e)
        {
            bool isResponded = false;
            foreach (DataGridViewRow row in dataGridViewFeedback.SelectedRows)
            {
                if (!string.IsNullOrEmpty(row.Cells[3].Value?.ToString()))
                {
                    isResponded = true;
                }
            }
            if (isResponded)
            {
                MessageBox.Show("One or more selected feedbacks have already been responded to.");
                LoadFeedback();
            }
        }

        private void dataGridViewRefunds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cellValue = dataGridViewRefunds.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (e.RowIndex >= 0 && (e.ColumnIndex == 3) && (cellValue != "" || cellValue != null))
                {
                    MessageBox.Show(cellValue);
                }
            }
            catch
            {}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;
        }
    }
    
}