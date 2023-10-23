using RabbitMQ.Client;

namespace webapi
{
    public static class RabbitMQStart
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Configure RabbitMQ Connection
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672, // Default RabbitMQ port
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };

            var connection = factory.CreateConnection();

            services.AddSingleton(connection);
        }

    }
}

