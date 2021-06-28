using Services.ConsumerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.Waitress
{
    class Startup
    {
        private readonly IConsumerService _consumerService;
        public Startup(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }
        public void Run()
        {
            _consumerService.AddConsumer();
            Thread.Sleep(100000);
        }
    }



}
