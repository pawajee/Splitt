using Duc.Splitt.CustomerApi.Extensions;
using NLog;
using NLog.Web;


var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
     var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
    logger.Debug("Application Starting Up");
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();
    ServiceExtensions.ApplyServiceExtensions(builder);
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
