//using Serilog;

namespace Api6SinTlsSerilog.Services;

public class MyService: IMyService
{
    private readonly ILogger<MyService> _logger;
    private readonly IHostInfo _hostInfo;

    public MyService(ILogger<MyService> logger, IHostInfo hostInfo)
    {
        _logger = logger;
        _hostInfo = hostInfo;
    }
    public string GetMyData()
    {
        try
        {
            //Establecer una propiedad con MS Logging (LogContext.PushProperty en Serilog):
            //_logger.BeginScope(new Dictionary<string, object> { ["IpAddress"] = _hostInfo.Get() });
            _logger.LogInformation("Hola, {nombre}", "Pepo");
            _logger.LogInformation("Hola, {nombre}.", "Pablo");
            _logger.LogError("Hubo error!");

            return "This is my service call";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ha ocurrido un error: {ex.Message}");
            return ex.Message;
        }

    }
}
