namespace Api6SinTlsSerilog.Services;

public interface IHostInfo
{
    void Set(string ipAddress);
    string Get();
}
