namespace HL7APIProject.Services.Interfaces
{
    public interface IRabbitMQPublisher
    {
        void SendToQueue(string message, string uniqueId);
    }
}
