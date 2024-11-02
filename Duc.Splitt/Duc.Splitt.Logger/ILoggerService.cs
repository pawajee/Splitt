using static Duc.Splitt.Logger.Enums;

namespace Duc.Splitt.Logger
{
    public interface ILoggerService
    {
       List<string> ConvertExceptionToStringList(Exception ex);
        void LogTrace(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);
        void LogInfo(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);
        void LogWarning(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);
        void LogDebug(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);
        void LogError(Exception exception, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);
        void LogFatal(Exception exception, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1);

        void LogRequestResponce(string message, Type type, string logType, string responceTime, string header = "",
             LoggerNames loggerNames = LoggerNames.writeToLog1);
        void LogMessage(LoggerRequestDto request, Type? type = null, LoggerNames loggerNames = LoggerNames.writeToLog1);
        string ToJson(object value);

    }
}
