//using Serilog;

namespace Api6SinTlsSerilog.Services;

public class MyService: IMyService
{
    private readonly ILogger<MyService> _logger;
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
        //_logger = Log.ForContext<MyService>();
    }
    public string GetMyData()
    {
        try
        {
            _logger.LogInformation("Insertando una linea de log");
            return "This is my service call";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ha ocurrido un error: {ex.Message}");
            return ex.Message;
        }

    }
}
