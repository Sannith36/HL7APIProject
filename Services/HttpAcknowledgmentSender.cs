using System.Net.Http;
using System.Text;
using HL7APIProject.Interfaces;
using Newtonsoft.Json;


namespace HL7APIProject.Services
{
    public class HttpAcknowledgmentSender : IHttpAcknowledgmentSender
    {
        private readonly HttpClient _httpClient;

        public HttpAcknowledgmentSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool SendAcknowledgment(string acknowledgmentUrl, string uniqueId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { uniqueId }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, acknowledgmentUrl) { Content = content };

           
            var response = _httpClient.Send(request);

            return response.IsSuccessStatusCode;
        }
    }
}