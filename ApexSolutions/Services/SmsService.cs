using System.Text;

namespace ApexSolutions.Services
{
    public class SmsService
    {
        private readonly HttpClient _client;

        public SmsService(HttpClient client)
        {
            _client = client;
        }

        public async Task SendSmsAsync(string apiToken, string recipient1, string recipient2, string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.d7networks.com/messages/v1/send");

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {apiToken}");

            var payload = $@"
            {{
              ""messages"": [
                {{
                    ""channel"": ""sms"",
                    ""recipients"": [""{recipient1}"", ""{recipient2}""],
                    ""content"": ""{message}"",
                    ""msg_type"": ""text"",
                    ""data_coding"": ""text""
                }}
              ],
              ""message_globals"": {{
                ""originator"": ""SignOTP"",
                ""report_url"": ""https://the_url_to_receive_delivery_report.com""
              }}
            }}";

            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
