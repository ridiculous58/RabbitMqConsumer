//using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.RabbitMq
{
    public class BasicConsumeModel
    {
        public string Queue { get; set; }
        public bool AutoAck { get; set; }
       // public IBasicConsumer Consumer { get; set; }
    }
}
