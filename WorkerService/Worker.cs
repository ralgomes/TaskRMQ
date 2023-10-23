using Dados;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // aqui eu consumo o a queue do RabbitMQ e faço as alterações necessárias no banco dados

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(20, stoppingToken);
            }
        }
    }
}