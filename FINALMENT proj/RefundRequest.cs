using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALMENT_proj
{
    /// <summary>
    /// Represents a refund request record in the system.
    /// </summary>
    public class RefundRequest
    {
        // Properties
        public int RequestID { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
        public DateTime? ReviewedAt { get; set; } // Nullable DateTime

        // Constructor
        public RefundRequest(int requestID, string username, decimal amount, string reason, string status, DateTime? reviewedAt)
        {
            RequestID = requestID;
            Username = username;
            Amount = amount;
            Reason = reason;
            Status = status;
            ReviewedAt = reviewedAt;
        }
    }
}
