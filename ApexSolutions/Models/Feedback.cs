using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSolutions.Models
{
    public class Feedback
    {
        // Fields
        private int feedbackID;
        private int clientID;
        private int serviceRequestID;
        private int satisfactionRating;
        private string comments;
        private DateTime feedbackDate;

        // Properties
        public int FeedbackID { get => feedbackID; set => feedbackID = value; }
        public int ClientID { get => clientID; set => clientID = value; }
        public int ServiceRequestID { get => serviceRequestID; set => serviceRequestID = value; }
        public int SatisfactionRating { get => satisfactionRating; set => satisfactionRating = value; }
        public string Comments { get => comments; set => comments = value; }
        public DateTime FeedbackDate { get => feedbackDate; set => feedbackDate = value; }

        // Constructor
        public Feedback(int feedbackID, int clientID, int serviceRequestID, int satisfactionRating, string comments)
        {
            this.feedbackID = feedbackID;
            this.clientID = clientID;
            this.serviceRequestID = serviceRequestID;
            this.satisfactionRating = satisfactionRating;
            this.comments = comments;
            feedbackDate = DateTime.Now;
        }

        // Methods
        public void RecordFeedback(int rating, string comments)
        {
            satisfactionRating = rating;
            this.comments = comments;
        }
    }
}
