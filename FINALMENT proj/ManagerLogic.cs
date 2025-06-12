using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINALMENT_proj
{
    /// <summary>
    /// Contains backend logic for the Manager role.
    /// </summary>
    public class ManagerLogic
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the ManagerLogic class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public ManagerLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Feedback Methods

        /// <summary>
        /// Retrieves feedback based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter type: "Unresponded", "Responded", or "All".</param>
        /// <returns>A list of Feedback objects.</returns>
        public List<Feedback> GetFeedback(string filter)
        {
            List<Feedback> feedbackList = new List<Feedback>();
            string query;

            switch (filter.ToLower())
            {
                case "unresponded":
                    query = "SELECT * FROM Feedback WHERE IsResponded = 0";
                    break;
                case "responded":
                    query = "SELECT * FROM Feedback WHERE IsResponded = 1";
                    break;
                case "all":
                default:
                    query = "SELECT * FROM Feedback";
                    break;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        feedbackList.Add(new Feedback(
                            Convert.ToInt32(reader["FeedbackID"]),
                            reader["username"].ToString(),
                            reader["FeedbackText"].ToString(),
                            reader["ManagerResponse"]?.ToString(),
                            Convert.ToBoolean(reader["IsResponded"]),
                            Convert.ToDateTime(reader["CreatedAt"])
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching feedback: " + ex.Message);
            }

            return feedbackList;
        }

        /// <summary>
        /// Responds to a specific feedback by updating the database.
        /// </summary>
        /// <param name="feedbackID">The ID of the feedback to respond to.</param>
        /// <param name="response">The manager's response.</param>
        public void RespondToFeedback(int feedbackID, string response)
        {
            string query = "UPDATE Feedback SET ManagerResponse = @Response, IsResponded = 1 WHERE FeedbackID = @FeedbackID";
            
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Response", response);
                    cmd.Parameters.AddWithValue("@FeedbackID", feedbackID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error responding to feedback: " + ex.Message);
            }
        }

        #endregion

        #region Refund Requests Methods

        /// <summary>
        /// Retrieves pending refund requests from the database.
        /// </summary>
        /// <returns>A DataTable containing refund requests.</returns>
        public List<RefundRequest> GetRefundRequest(string filter)
        {
            List<RefundRequest> refundList = new List<RefundRequest>();
            string query;

            switch (filter.ToLower())
            {
                case "pending":
                    query = "SELECT * FROM RefundRequests WHERE Status = 'Pending'";
                    break;
                case "approved":
                    query = "SELECT * FROM RefundRequests WHERE Status = 'Approved'";
                    break;
                case "rejected":
                    query = "SELECT * FROM RefundRequests WHERE Status = 'Rejected'";
                    break;
                default:
                    query = "SELECT * FROM RefundRequests";
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        refundList.Add(new RefundRequest(
                            Convert.ToInt32(reader["RequestID"]),
                            reader["username"].ToString(),
                            Convert.ToDecimal(reader["Amount"]),
                            reader["Reason"].ToString(),
                            reader["Status"].ToString(),
                            reader["ReviewedAt"] != DBNull.Value ? Convert.ToDateTime(reader["ReviewedAt"]) : (DateTime?)null
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching refund requests: " + ex.Message);
            }

            return refundList;
        }

        /// <summary>
        /// Updates the status of a refund request in the database.
        /// </summary>
        /// <param name="requestID">The ID of the refund request.</param>
        /// <param name="status">The new status ('Approved' or 'Rejected').</param>
        public void UpdateRefundRequest(int requestID, string status, string username, decimal amount, bool isApproved)
        {
            // SQL query to update the refund request
            string updateQuery = "UPDATE RefundRequests SET Status = @Status, ReviewedAt = GETDATE() WHERE RequestID = @RequestID";

            // SQL query to top up the user's e-wallet balance
            string topUpQuery = "UPDATE Users SET balance = balance + @Amount WHERE username = @Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Start a transaction to ensure atomicity
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Step 1: Update the refund request status
                        SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction);
                        cmdUpdate.Parameters.AddWithValue("@Status", status);
                        cmdUpdate.Parameters.AddWithValue("@RequestID", requestID);
                        cmdUpdate.ExecuteNonQuery();

                        // Step 2: If approved, refund the amount to the user's e-wallet
                        if (isApproved)
                        {
                            SqlCommand cmdTopUp = new SqlCommand(topUpQuery, conn, transaction);
                            cmdTopUp.Parameters.AddWithValue("@Amount", amount);
                            cmdTopUp.Parameters.AddWithValue("@Username", username);
                            cmdTopUp.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating refund request: " + ex.Message);
            }
        }

        #endregion

        #region E-Wallet Top-Up Methods

        /// <summary>
        /// Retrieves a list of usernames for customers.
        /// </summary>
        /// <returns>A list of usernames.</returns>
        public List<string> GetCustomerUsernames()
        {
            List<string> usernames = new List<string>();
            string query = "SELECT username FROM Users WHERE permissions = 'customer'";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usernames.Add(reader["username"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customer usernames: " + ex.Message);
            }

            return usernames;
        }

        /// <summary>
        /// Retrieves the name associated with a specific username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The name associated with the username.</returns>
        public string GetUserName(string username)
        {
            string query = "SELECT name FROM Users WHERE username = @Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return string.Empty; // Return empty string if no name is found
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user name: " + ex.Message);
            }
        }

        /// <summary>
        /// Tops up the e-wallet balance for a customer.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="amount">The amount to top up.</param>
        public void TopUpEwallet(string username, decimal amount)
        {
            // SQL query to update the e-wallet balance
            string updateQuery = "UPDATE Users SET balance = balance + @Amount WHERE username = @Username";

            // SQL query to log the transaction
            string logQuery = "INSERT INTO TopUpHistory (username, Amount, TopUpDate) VALUES (@Username, @Amount, GETDATE())";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Start a transaction to ensure atomicity
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Update wallet balance
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                        updateCmd.Parameters.AddWithValue("@Amount", amount);
                        updateCmd.Parameters.AddWithValue("@Username", username);
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        // Check if the username exists
                        if (rowsAffected == 0)
                        {
                            throw new Exception("The specified username does not exist.");
                        }

                        // Log the transaction
                        SqlCommand logCmd = new SqlCommand(logQuery, conn, transaction);
                        logCmd.Parameters.AddWithValue("@Username", username);
                        logCmd.Parameters.AddWithValue("@Amount", amount);
                        logCmd.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error topping up e-wallet: " + ex.Message);
            }
        }

        #endregion

        #region Update Manager Profile Methods

        /// <summary>
        /// Updates the Manager's profile in the Users table.
        /// </summary>
        /// <param name="username">The username of the Manager.</param>
        /// <param name="name">The updated name.</param>
        /// <param name="password">The updated password.</param>
        public void UpdateManagerProfile(string username, string name, string password, string bio)
        {
            string query = "UPDATE Users SET name = @Name, password = @Password, bio = @Bio WHERE username = @Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Bio", bio);
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating manager profile: " + ex.Message);
            }
        }

        #endregion
    }
}
