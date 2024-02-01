using Serilog.Events;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;

namespace Api6SinTlsSerilog;

public static class SerilogConfigurator
{
    public static void Configure(bool consoleSink, bool fileSink, bool dbSink, string conStr = "")
    {
        if (dbSink && conStr == "") throw new Exception();

        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext();

        if (consoleSink)
        {
            logger.WriteTo.Console();
        }

        if (fileSink)
        {
            var logFilePath = $"{Directory.GetCurrentDirectory()}/logs/log.log";
            logger.WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 99999999999,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
            );
        }

        if (dbSink)
        {
            logger.WriteTo.MSSqlServer(
                connectionString: conStr,
                columnOptions: new ColumnOptions()
                {

                },
                sinkOptions: new MSSqlServerSinkOptions()
                {
                    TableName = "AppLogs",
                    SchemaName = "dbo",
                    AutoCreateSqlTable = true,
                    AutoCreateSqlDatabase = true
                }
            );
        }

        Log.Logger = logger.CreateLogger();
    }
}
