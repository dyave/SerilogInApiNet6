//using Serilog;

using Newtonsoft.Json;
using System.Reflection;

namespace Api6SinTlsSerilog.Services;

public class MyService: IMyService
{
    private readonly ILogger<MyService> _Logger;
    private readonly IHostInfo _hostInfo;

    public MyService(ILogger<MyService> logger, IHostInfo hostInfo)
    {
        _Logger = logger;
        _hostInfo = hostInfo;
    }
    public string GetMyData(string tmpPara)
    {
        try
        {
            _Logger.LogInformation("Hey! Method: {method}", MethodBase.GetCurrentMethod().Name);
            //int num = 123;
            //bool flag = false;
            //List<int> listx = new List<int> { 10, 11, 12, 13 };
            //var serList = JsonConvert.SerializeObject(listx);

            //var strTemplate = string.Format("This is for log: {0} | {1} | {2} | {3}", tmpPara, flag, listx, serList);
            //_Logger.LogInformation(strTemplate);

            //_Logger.LogError("Hubo error 1!");
            //var tmp = 5;
            //tmp = 1 / (5 - tmp);
            //var currentTime = DateTime.Now;
            //var className = this.GetType().Name;
            //var methodName = MethodBase.GetCurrentMethod().Name;
            //Log(methodName, LogLevel.Information, new string[] { "diego" });
            //Log("Log this for me at {currentTime}", LogLevel.Information, currentTime);

            List<int> list = new List<int> { 10, 11, 12, 13 };
            var res = list?.Aggregate("", (acc, x) => acc + x.ToString() + ",");

            //int? statusId = null;
            //bool? isNavbar = null;
            //_Logger.LogInformation("Parametros {statusId}, {isNavbar}", statusId, isNavbar);

            //Establecer una propiedad con MS Logging (LogContext.PushProperty en Serilog):
            //_logger.BeginScope(new Dictionary<string, object> { ["IpAddress"] = _hostInfo.Get() });
            //_logger.LogInformation("Hey! Mira: {tmpPara} ", tmpPara);
            //_logger.LogInformation("Hola, {nombre}", res);
            //_logger.LogInformation("Hola, {nombre}.", "Pablo");
            //_logger.LogError("Hubo error!");

            return "This is my service call";
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Hubo error 2! {exMessage}", ex.Message);
            return ex.Message;
        }

    }

    private void Log(string message, LogLevel loglevel, params object?[] args)
    {
        //message += " | Method {method}";
        //args.Append(MethodBase.GetCurrentMethod().Name);
        _Logger.Log(loglevel, message, args);
    }

    protected void Log(string method, LogLevel loglevel, string[] parameters = null)
    {
        var sharedLocalizer = "[{0}]({1} - {2})";
        var projLocalizer = "Localizer log: {0}";
        string message = string.Format(sharedLocalizer, DateTime.Now, this.GetType().Name, method) +
            string.Format(projLocalizer, parameters != null ? parameters : new string[] { });

        Log(message, loglevel);
    }

    protected void Log(string message, LogLevel loglevel)
    {
        if (loglevel.Equals(LogLevel.Information))
        {
            _Logger.LogInformation(message);
        }
        else if (loglevel.Equals(LogLevel.Trace))
        {
            _Logger.LogTrace(message);
        }
        else if (loglevel.Equals(LogLevel.Critical))
        {
            _Logger.LogCritical(message);
        }
        else if (loglevel.Equals(LogLevel.Warning))
        {
            _Logger.LogWarning(message);
        }
        else if (loglevel.Equals(LogLevel.Error))
        {
            _Logger.LogError(message);
        }
    }

}
