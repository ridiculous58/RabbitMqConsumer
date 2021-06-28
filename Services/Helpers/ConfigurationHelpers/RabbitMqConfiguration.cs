using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers.ConfigurationHelpers
{
    public class RabbitMqConfiguration
    {
#if DEBUG
        public const string HOST = "localhost";
        public const string USERNAME = "guest";
        public const string PASSWORD = "guest";
        public const int PORT = 5672;
        public static string URI = $"amqp://{USERNAME}:{PASSWORD}@{HOST}:{PORT}/vhost";

#else
                public const string RABBIT_MQ_URL = "rabbitmq";
                public const int PORT = 5672;
#endif
    }
}
