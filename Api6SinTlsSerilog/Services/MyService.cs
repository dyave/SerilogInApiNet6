//using Serilog;

namespace Api6SinTlsSerilog.Services;

public class MyService: IMyService
{
    private readonly ILogger<MyService> _logger;
    private readonly IHostInfo _hostInfo;

    public MyService(ILogger<MyService> logger, IHostInfo hostInfo)
    {
        _logger = logger;
        //_logger = Log.ForContext<MyService>();
        _hostInfo = hostInfo;
    }
    public string GetMyData()
    {
        try
        {
            _logger.LogInformation("[IP: {hostIp}]Insertando una linea de log", _hostInfo.Get());
            return "This is my service call";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ha ocurrido un error: {ex.Message}");
            return ex.Message;
        }

    }
}
