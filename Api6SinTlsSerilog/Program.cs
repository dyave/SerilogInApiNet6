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

            // Add services to the container.
            builder.Services.AddTransient<IMyService, MyService>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(
                    //connectionString: "Server=localhost;Database=LogsFromSerilog;Integrated Security=SSPI;",
                    connectionString: "Server=localhost;Database=LogsFromSerilog;Trusted_Connection=True;TrustServerCertificate=True;",
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" })
                .CreateLogger();

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