using Serilog.Events;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Data;
using Serilog.Core;
using System.Net;
using System.Configuration;

namespace Api6SinTlsSerilog;

public static class SerilogConfigurator
{
    public static void Configure(bool consoleSink, bool fileSink, bool dbSink, IConfiguration configuration, string conStr)
    {
        if (dbSink && conStr == "") throw new Exception();

        string hostName = Dns.GetHostName();
        var hostIp = Dns.GetHostEntry(hostName).AddressList[1].ToString();

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("IpAddress", hostIp)
            .ReadFrom.Configuration(configuration)
            //.Enrich.With(new MyEnricher())
            //.MinimumLevel.Debug()
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
            //.MinimumLevel.Override("System", LogEventLevel.Warning)
            //.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Warning)
            ;

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
                columnOptions: GetColumnOptions(),
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

    public static ColumnOptions GetColumnOptions()
    {
        var columnOptions = new ColumnOptions();

        columnOptions.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "IpAddress", DataLength = 50, AllowNull = true},
            };
        return columnOptions;
    }
}
