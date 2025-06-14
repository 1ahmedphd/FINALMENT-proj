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
            this.feedbackTableAdapter.Fill(this.iOOPGADataSet.Feedback);            // Loads data into the Feedback table
            this.refundRequestsTableAdapter.Fill(this.iOOPGADataSet.RefundRequests);// Loads data into the RefundRequests table
            _managerLogic = new ManagerLogic(_connectionString);                    // Initialize the ManagerLogic instance
            LoadFeedback();         // Load new feedback when the form loads
            LoadRefunds();          // Load refund requests when the form loads
            LoadUsernames();        // Populate the listBox with the usernames to top up
            #region Update Manager Profile 2
            // Set the current user's profile information and display it on the form
            currentUser = currentuser;            
            lblWelcomeManager.Text = lblWelcomeManager.Text + currentUser.name;
            // Retrieve the manager's bio from the database and populate the rich text box
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
            // Populate the text boxes with the manager's name and username for editing
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

                List<Feedback> feedbackList = _managerLogic.GetFeedback(filter);    // Fetch feedback based on the filter           
                dataGridViewFeedback.DataSource = feedbackList;                     // Bind the data to the DataGridView

                // Adjust column widths for better readability
                dataGridViewFeedback.Columns[0].Width = 75;     // FeedbackID
                dataGridViewFeedback.Columns[1].Width = 100;    // Customer   
                dataGridViewFeedback.Columns[2].Width = 250;    // FeedbackText
                dataGridViewFeedback.Columns[3].Width = 250;    // ManagerResponse
                dataGridViewFeedback.Columns[4].Width = 125;    // CreatedAt
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridViewFeedback_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cellValue = dataGridViewFeedback.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3) && (!string.IsNullOrEmpty(cellValue)))
                {
                    MessageBox.Show(cellValue);
                }
            }
            catch { }
        }

        private void txtManagerResponse_Enter(object sender, EventArgs e)
        {
            bool isResponded = false;

            // Iterate through all selected rows in the DataGridView
            foreach (DataGridViewRow row in dataGridViewFeedback.SelectedRows)
            {
                // Check if the ManagerResponse column has a non-empty value
                if (!string.IsNullOrEmpty(row.Cells[3].Value?.ToString()))
                {
                    isResponded = true;
                }
            }
            if (isResponded)
            {
                MessageBox.Show("One or more selected feedbacks have already been responded to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadFeedback();
            }
        }

        /// <summary>
        /// Handles the button click event to respond to feedback.
        /// </summary>
        private void btnRespond_Click(object sender, EventArgs e)
        {
            // Validate if at least one row is selected
            if (dataGridViewFeedback.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one feedback to respond.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show("One or more selected feedbacks have already been responded to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string response = txtManagerResponse.Text.Trim();   // Get the manager's response from the textbox

                // Validate input
                if (string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show("Please enter a response.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        selectedRow.Cells[3].Value = response;                      // Update the selected row in the DataGridView
                        _managerLogic.RespondToFeedback(feedbackID, response);      // Call the backend method to update the feedbacks
                    }
                    
                    LoadFeedback();                 // Reload feedback to reflect changes
                    txtManagerResponse.Clear();     // Clear the response textbox

                    MessageBox.Show("Feedback response(s) submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error responding to feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Refund Requests Handling

        private void UncheckRefundRbs() 
        {
            // Unchecks radio buttons to approve or reject refund requests
            rbApprove.Checked = false;
            rbReject.Checked = false;
        }

        /// <summary>
        /// Loads new refund requests into the DataGridView.
        /// </summary>
        private void LoadRefunds()
        {
            try
            {
                string filter;

                // Determine the filter based on the selected radio button
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
                // Fetch refund data based on the filte
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

        private void dataGridViewRefunds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Get the value of the clicked cell
                string cellValue = dataGridViewRefunds.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (e.RowIndex >= 0 && (e.ColumnIndex == 3) && (cellValue != "" || cellValue != null))
                {
                    MessageBox.Show(cellValue); // Display the cell value in a message box
                }
            }
            catch
            { }
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

                        // Call the backend method to update the refund requests
                        _managerLogic.UpdateRefundRequest(requestID, status, username, amount, rbApprove.Checked);
                    }

                    LoadRefunds();  // Reload refunds to reflect changes

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
                lbUsername.Items.Clear(); 
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

                    lblName2.Text = name;           // Update the Name label
                    lblBalance2.Text = balance;     // Update the Balance label
                }
                else
                {
                    lblName2.Text = string.Empty;       // Clear the label if no username is selected
                    lblBalance2.Text = string.Empty;    // Clear the balance if no username is selected
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearTopUp()
        {
            lbUsername.SelectedIndex = -1;  // Clear the selected item in the ComboBox
            lblName2.Text = "N/A";          // Reset the lblName2 label to its default state
            lblBalance2.Text = "N/A";       // Reset the lblBalance2 label to its default state
            txtAmount.Text = string.Empty;  // Clear the typed top up amount in the textbox
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
                "Top-Up Confirmation",
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

        #region Update Manager Profile 1

        private void button4_Click(object sender, EventArgs e)
        {
            // Enable editing by making buttons visible and text fields editable
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
                // SQL query to update the user's profile in the database
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
            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            currentUser.Refresh(textBox7.Text);                         // Refresh the current user object with the updated username
            lblWelcomeManager.Text = "Welcome, " + currentUser.name;    // Update the welcome label to reflect the updated name

            // Disable editing mode by hiding buttons and making fields read-only
            button5.Visible = false;
            button2.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox3.ReadOnly = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Validate that all fields are filled
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Ensure the old password is not the same as the new password
            else if (textBox3.Text == textBox4.Text && textBox4.Text == textBox5.Text)
            {
                MessageBox.Show("Old password cannot be the same as new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                string password;

                // Retrieve the current password from the database
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sqlQuery = "Select password from Users Where Username = @username";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", currentUser.username);
                        object result = cmd.ExecuteScalar();
                        password = result != null ? result.ToString() : null;   // Store the retrieved password
                    }
                }

                // Check if the old password matches and the new passwords match each other
                if ((password == textBox3.Text) && (textBox4.Text == textBox5.Text))
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        // SQL query to update the password in the database
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

                    // Clear the password fields after successful update
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                // Handle incorrect old password and mismatched new passwords
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
            // Confirm with the user before logging out
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
);

            if (result == DialogResult.Yes)
            {
                // Hide the current form and navigate back to the Welcome screen
                this.Hide();
                Welcome welcome = new Welcome();
                welcome.ShowDialog();
                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Populate the fields with the current user's data to cancel editing
            textBox6.Text = currentUser.name;
            textBox7.Text = currentUser.username;
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                string query = "Select bio from Users where username = @username";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUser.username);
                    object result = cmd.ExecuteScalar();
                    richTextBox3.Text = result != null ? result.ToString() : null;  // Set the bio field
                }
            }
            // Exit editing mode by hiding buttons and making fields read-only
            button5.Visible = false;
            button2.Visible = false;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            richTextBox3.ReadOnly = true;
        }

        #endregion
    }
}