using Nexmo.Api;

namespace _2FABotDemo.Helpers
{
    public class VerifyHelper
    {
        public string RequestId { get; set; }
        public Client Client { get; set; }

        public VerifyHelper()
        {
            Client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "NEXMO_API_KEY",
                ApiSecret = "NEXMO_API_SECRET"
            });
        }

        public void SendVerificationCode(string phoneNumber)
        {
            var result = Client.NumberVerify.Verify(new NumberVerify.VerifyRequest
            {
                number = phoneNumber,
                brand = "NexmoQS"
            });

            RequestId = result.request_id;
        }

        public string CheckVerificationCode(string code)
        {
            var result = Client.NumberVerify.Check(new NumberVerify.CheckRequest
            {
                request_id = RequestId,
                code = code
            });

            if (result.status == "0")
            {
                return "Verification Successful";
            }
            else
            {
                return result.error_text;
            }
        }
    }
}