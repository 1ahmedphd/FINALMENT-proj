using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALMENT_proj
{
    /// <summary>
    /// Represents a feedback record in the system.
    /// </summary>
    public class Feedback
    {
        // Properties
        public int FeedbackID { get; set; }
        public string Username { get; set; }
        public string FeedbackText { get; set; }
        public string ManagerResponse { get; set; }
        public bool IsResponded { get; set; }
        public DateTime CreatedAt { get; set; }

        // Constructor
        public Feedback(int feedbackID, string username, string feedbackText, string managerResponse, bool isResponded, DateTime createdAt)
        {
            FeedbackID = feedbackID;
            Username = username;
            FeedbackText = feedbackText;
            ManagerResponse = managerResponse;
            IsResponded = isResponded;
            CreatedAt = createdAt;
        }
    }
}
