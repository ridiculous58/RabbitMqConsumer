using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.DependencyResolvers;
using Infrastructure.Extensions;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Services.ConsumerService;
using Services.ConsumerService.Concrete;
using Services.IoCService.Autofac;
using System;
using System.Linq;
using System.Threading;

namespace Consumer.Waitress
{
    class Program
    {
        static readonly AutoResetEvent autoEvent = new(false);
        static void Main(string[] args)
        {
            var startup = BuildStartup(args);

            startup.Run();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);

            autoEvent.WaitOne();

        }


        static Startup BuildStartup(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            
            var firstArg = args.Length > 0 ? args[1] : string.Empty;

            AddCustomService(firstArg, services);

            services.AddOptions();
            services.AddSingleton<Startup>();

            services.AddDependencyResolvers(new ICoreModule[] { new CoreModule()});

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new AutofacServiceModule());
            containerBuilder.Populate(services);
            
            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider.GetService<Startup>();
        }
        static void AddCustomService(string consumerType, IServiceCollection services)
        {
            switch (consumerType)
            {
                default:
                    services.AddSingleton<IConsumerService, OrderConsumerService>();
                    break;
            }
        }

        protected static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Consumer closed.");
            autoEvent.Set();
            Environment.Exit(0);
        }
    }
}
