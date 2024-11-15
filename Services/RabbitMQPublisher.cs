using HL7APIProject.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace HL7APIProject.Services
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IModel _channel;

        public RabbitMQPublisher(IModel channel)
        {
            _channel = channel;
        }

        public void PublishMessage(string message, string uniqueId)
        {
            Console.WriteLine($"Publishing message to RabbitMQ: {message} with ID: {uniqueId}");
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.MessageId = uniqueId;

            _channel.BasicPublish(
                exchange: "",
                routingKey: "hl7_queue",
                basicProperties: properties,
                body: body);
        }


        public void SendToQueue(string message, string queueName = "hl7_queue")
        {
            Console.WriteLine($"Sending message to RabbitMQ queue: {queueName}, Message: {message}");
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "",
                routingKey: "HL7_queue",  
                basicProperties: null,
                body: body);
        }

    }
}
