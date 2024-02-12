using Api6SinTlsSerilog.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;

namespace Api6SinTlsSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var enviroment = builder.Environment;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(enviroment.ContentRootPath)
                .AddJsonFile(enviroment.ContentRootPath + $"\\appsettings.{enviroment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();

            var conStr = configuration.GetConnectionString("LoggingDb");

            // Add services to the container.
            builder.Services.AddTransient<IMyService, MyService>();
            builder.Services.AddSingleton<IHostInfo, HostInfo>();

            SerilogConfigurator.Configure(true, true, true, configuration, "Server=localhost;Database=DbForLogs;Trusted_Connection=True;TrustServerCertificate=True;");

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });

            builder.Host.UseSerilog(); //Here Serilog.AspNetCore needed

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}