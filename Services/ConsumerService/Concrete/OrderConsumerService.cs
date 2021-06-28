using Entities.Concrete.RabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConsumerService.Concrete
{
    public class OrderConsumerService : ConsumerServiceBase
    {
        
        public OrderConsumerService(IConnectionFactory factory) : base(factory)
        {

        }

        public override void AddConsumer(AddConsumeModel addConsumeModel = null)
        {
            var cosnumeType = "Order";
            addConsumeModel = new AddConsumeModel
            {
                BasicConsumeModel = new BasicConsumeModel
                {
                    Queue = cosnumeType,
                    AutoAck = false
                },
                QueueDeclareModel = new QueueDeclareModel
                {
                    Arguments = null,
                    Durable = true,
                    AutoDelete = false,
                    Exclusive = false,
                    Queue = cosnumeType
                }
            };
            base.AddConsumer(addConsumeModel);
        }

        public override void ProcessData(string message)
        {
            Console.WriteLine("Proccessing Data");
        }
    }
}
