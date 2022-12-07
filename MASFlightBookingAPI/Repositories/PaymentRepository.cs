using MASFlightBookingAPI.View_Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MASFlightBookingAPI.Repositories
{
    public class PaymentRepository :IPaymentRepository
    {
        private readonly IConfiguration configuration;
        public PaymentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public async Task<PaymentResponseModel> InitiatePayment(PaymentRequestModel model)
        {
            PaymentResponseModel deserialize;

            var key = configuration.GetValue<string>("FlutterWave:SecretKey");
            var url = $"{configuration.GetValue<string>("FlutterWave:url")}/payments";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");

            //serialize model object to json string
            var data = JsonConvert.SerializeObject(model);
            var sendRequest = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
            var response = await sendRequest.Content.ReadAsStringAsync();

            deserialize = JsonConvert.DeserializeObject<PaymentResponseModel>(response);

            return deserialize;
        }

        public async Task<PaymentVerificationResponseModel> VerifyPayment(string transactionId)
        {
            PaymentVerificationResponseModel deserialize;

            var key = configuration.GetValue<string>("FlutterWave:SecretKey");
            var url = $"{configuration.GetValue<string>("FlutterWave:url")}/transactions/{transactionId}/verify";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
            var sendRequest = await client.GetAsync(url);
            var response = await sendRequest.Content?.ReadAsStringAsync();

            deserialize = JsonConvert.DeserializeObject<PaymentVerificationResponseModel>(response);

            return deserialize;
        }
    }
}

