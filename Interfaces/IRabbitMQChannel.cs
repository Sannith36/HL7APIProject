using RabbitMQ.Client;

namespace HL7APIProject.Interfaces
{
    public interface IRabbitMQChannel : IModel
    {
        void BasicPublish(string exchange, string routingKey, IBasicProperties basicProperties, ReadOnlyMemory<byte> body);
    }
}
