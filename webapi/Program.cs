using webapi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Dados;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<TarefaContext>();
        builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
        builder.Services.AddCors();
        // RabbitMQStart.ConfigureServices(builder.Services);
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Logging.ClearProviders().AddConsole();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(options => options.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()); // só porque é um teste

        app.MapControllers();

        app.Run();
    }
}