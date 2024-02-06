using Serilog.Core;
using Serilog.Events;

namespace Api6SinTlsSerilog;

public class MyEnricher : ILogEventEnricher
{
    public void Enrich(
        LogEvent logEvent,
        ILogEventPropertyFactory propertyFactory)
    {
        var enrichProperty = propertyFactory
            .CreateProperty(
                "IpAddress",
                "123");

        logEvent.AddOrUpdateProperty(enrichProperty);
    }
}