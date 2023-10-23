using WorkerService;
using Dados;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddDbContext<TarefaContext>();
        services.AddScoped<TarefaRepository>();
        // 
    })
    .Build();

await host.RunAsync();
