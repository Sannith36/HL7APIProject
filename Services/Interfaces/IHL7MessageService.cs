namespace HL7APIProject.Services.Interfaces
{
    public interface IHL7MessageService
    {
        string ParseHL7ToJSON(string hl7Message);
        string StoreJSONToDatabase(string jsonMessage);
        void PublishToRabbitMQ(string jsonMessage, string uniqueId);
    }
}
