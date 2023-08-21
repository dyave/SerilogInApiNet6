using Serilog;

namespace Api6SinTlsSerilog.Services;

public class MyService: IMyService
{
    private readonly Serilog.ILogger _logger;
    public MyService()
    {
        _logger = Log.ForContext<MyService>();
    }
    public string GetMyData()
    {
        try
        {
            _logger.Information("Insertando una linea de log");
            return "This is my service call";
        }
        catch (Exception ex)
        {
            _logger.Error($"Ha ocurrido un error: {ex.Message}");
            return ex.Message;
        }

    }
}
