using ApexSolutions.Interfaces;

namespace ApexSolutions.Services
{
    public class FeedbackObserver : IObserver
    {
        private readonly string _email;

        public FeedbackObserver(string email)
        {
            _email = email;
        }

        public void Update()
        {
            SendFeedbackSurvey();
        }

        private void SendFeedbackSurvey()
        {
            // TODO - Logic for sending survey
            Console.WriteLine($"Sending feedback survey to {_email}");
        }
    }
}
