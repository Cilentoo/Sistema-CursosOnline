﻿using RabbitMQ.Client;
using System;

namespace Sistema_CursosOnline.Application.Messaging
{
    public class RabbitMqConfig
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "sistemaQueue";
        private readonly string _exchangeName = "sistemaExchange";


        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            return factory.CreateConnection();  
        }

   
        public IModel CreateChannel(IConnection connection)
        {
            IModel channel = connection.CreateModel(); 
            
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,    
                                 exclusive: false,  
                                 autoDelete: false, 
                                 arguments: null);  
            return channel;
        }


        public void SendMessage(IModel channel, string message)
        {
            var body = System.Text.Encoding.UTF8.GetBytes(message); 

            channel.BasicPublish(exchange: _exchangeName,
                                 routingKey: _queueName, 
                                 basicProperties: null,  
                                 body: body);           
        }
    }
}