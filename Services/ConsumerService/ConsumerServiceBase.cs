using Entities.Concrete.RabbitMq;
using Infrastructure.Aspects.Autofac.Exception;
using Infrastructure.CrossCuttingConcerns.Logging.ElasticSearch;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Helpers.ConfigurationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConsumerService
{
    public abstract class ConsumerServiceBase : IConsumerService
    {
        private readonly IConnectionFactory _factory;
        protected readonly IConnection _connection;
        public ConsumerServiceBase(IConnectionFactory factory)
        {
            _factory = factory;
            _connection = _factory.CreateConnection(RabbitMqConfiguration.URI);
        }
       [ExceptionLogAspect(typeof(ElasticLogger))]
        public virtual void AddConsumer(AddConsumeModel addConsumeModel = null)
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: addConsumeModel.QueueDeclareModel.Queue,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ProcessData(message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: addConsumeModel.BasicConsumeModel.Queue, autoAck: addConsumeModel.BasicConsumeModel.AutoAck, consumer: consumer);
        }
        public abstract void ProcessData(string message);
        
    }
}
