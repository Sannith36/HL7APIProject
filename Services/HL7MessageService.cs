using HL7APIProject.Interfaces;
using Newtonsoft.Json;
using NHapi.Base.Parser;
using NHapi.Base.Model;

namespace HL7APIProject.Services
{
    public class HL7MessageService : IHL7MessageService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public HL7MessageService(IDatabaseService databaseService, IRabbitMQPublisher rabbitMQPublisher)
        {
            _databaseService = databaseService;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public string ParseHL7ToJSON(string hl7Message)
        {
            try
            {
                PipeParser parser = new PipeParser();
                IMessage message = parser.Parse(hl7Message);
                string jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
                return jsonMessage;
            }
            catch
            {
                return "{ \"parsed\": \"jsonData\" }";
            }
        }

        public string StoreJSONToDatabase(string jsonMessage)
        {
            return _databaseService.InsertMessage(jsonMessage);
        }

        public void PublishToRabbitMQ(string jsonMessage, string uniqueId)
        {
            _rabbitMQPublisher.SendToQueue(jsonMessage, uniqueId);
        }

        public object ProcessHL7Message(string hl7Message)
        {
            try
            {
                
                string jsonMessage = ParseHL7ToJSON(hl7Message);

               
                string uniqueId = StoreJSONToDatabase(jsonMessage);

                
                PublishToRabbitMQ(jsonMessage, uniqueId);

                return new { Message = "HL7 message processed successfully", UniqueId = uniqueId };
            }
            catch (Exception ex)
            {
               
                return new { Error = "An error occurred while processing the HL7 message", Details = ex.Message };
            }
        }
    }
}
