using RabbitMQ.Client;

namespace HL7APIProject.Interfaces
{
    public interface IRabbitMQPublisher
    {
        void PublishMessage(string message, string uniqueId);
        void SendToQueue(string message, string queueName); 
    }
}
