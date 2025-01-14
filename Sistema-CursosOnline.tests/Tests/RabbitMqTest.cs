using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sistema_CursosOnline.Application.Messaging;
using Xunit;

namespace Sistema_CursosOnline.Tests
{
    public class RabbitMqIntegrationTest
    {
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbitMqIntegrationTest()
        {
            _rabbitMqConfig = new RabbitMqConfig(); 
        }

        [Fact]
        public async Task SendMessage_ShouldSendAndReceiveMessage_WhenRabbitMqIsRunning()
        {


            var messageToSend = "Test Message for RabbitMQ";
            var messageReceived = string.Empty;

  
            var tcs = new TaskCompletionSource<bool>();


            using (var connection = _rabbitMqConfig.CreateConnection())
            using (var channel = _rabbitMqConfig.CreateChannel(connection))
            {
      
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    messageReceived = Encoding.UTF8.GetString(body);


                    tcs.SetResult(true);
                };


                channel.BasicConsume(queue: "sistemaQueue", autoAck: true, consumer: consumer);


                _rabbitMqConfig.SendMessage(channel, messageToSend);

                await tcs.Task;


                Assert.Equal(messageToSend, messageReceived);
            }
        }
    }
}