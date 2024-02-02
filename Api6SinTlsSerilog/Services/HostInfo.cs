//using Serilog;

namespace Api6SinTlsSerilog.Services;

public class HostInfo: IHostInfo
{
    public string _ipAddress { get; set; } = "";

    public void Set(string ipAddress)
    {
        _ipAddress = ipAddress;
    }

    public string Get()
    {
        return _ipAddress;
    }

}
