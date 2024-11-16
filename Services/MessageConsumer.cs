using HL7APIProject.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace HL7APIProject.Services
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IModel _channel;
        private readonly IAcknowledgmentService _acknowledgmentService;

        public MessageConsumer(IConnectionFactory connectionFactory, IAcknowledgmentService acknowledgmentService)
        {
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
            _acknowledgmentService = acknowledgmentService;

            
            _channel.QueueDeclare(
                queue: "HL7_queue",  
                durable: false,      
                exclusive: false,     
                autoDelete: false,    
                arguments: null);     
        }

        public void ConsumeMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received message from RabbitMQ: {message}");

                Thread.Sleep(5000);
                AcknowledgeMessage(message);
            };

            _channel.BasicConsume(queue: "HL7_queue", autoAck: true, consumer: consumer);
        }

        public void AcknowledgeMessage(string uniqueId)
        {
            Console.WriteLine($"Acknowledging message with ID: {uniqueId}");
            _acknowledgmentService.ReceiveAcknowledgment(uniqueId);
        }
    }
}
