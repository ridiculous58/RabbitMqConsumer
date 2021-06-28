using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.RabbitMq
{
    public class AddConsumeModel
    {
        public BasicConsumeModel BasicConsumeModel { get; set; }
        public QueueDeclareModel QueueDeclareModel { get; set; }
    }
}
