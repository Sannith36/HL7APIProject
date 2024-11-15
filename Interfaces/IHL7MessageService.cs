namespace HL7APIProject.Interfaces
{
    public interface IHL7MessageService
    {
        string ParseHL7ToJSON(string hl7Message);
        string StoreJSONToDatabase(string jsonMessage);
        void PublishToRabbitMQ(string jsonMessage, string uniqueId);
        object ProcessHL7Message(string hl7Message);
    }
}
