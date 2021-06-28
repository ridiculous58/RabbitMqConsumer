using Entities.Concrete.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConsumerService
{
    public interface IConsumerService
    {
        void AddConsumer(AddConsumeModel addConsumeModel=null);
    }
}
