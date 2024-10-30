using Newtonsoft.Json;
using NLog;
using System.Reflection;
using static Duc.Splitt.Logger.Enums;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace Duc.Splitt.Logger
{
    public class LoggerService : ILoggerService
    {

        public LoggerService()
        {
        }
        public void LogTrace(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerMessage(message, LogLevel.Trace, type, loggerName);
        }
        public void LogInfo(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerMessage(message, LogLevel.Info, type, loggerName);

        }
        public void LogWarning(string message, Type? type, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerMessage(message, LogLevel.Warn, type, loggerName);

        }
        public void LogDebug(string message, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerMessage(message, LogLevel.Warn, type, loggerName);
        }


        public void LogError(Exception exception, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerException(exception, LogLevel.Error, type, loggerName);
        }
        public void LogFatal(Exception exception, Type? type = null, LoggerNames? loggerName = LoggerNames.writeToLog1)
        {
            AddLoggerException(exception, LogLevel.Fatal, type, loggerName);
        }

        public void LogRequestResponce(string message, Type type, string logType, string responceTime, string header = "",
            LoggerNames loggerNames = LoggerNames.writeToLog1)
        {
            ILogger logger = (type == null)
                                ? LogManager.GetCurrentClassLogger()
                                : LogManager.GetLogger(type.FullName);
            LogEventInfo eventInfo = new LogEventInfo(LogLevel.Trace, loggerNames.ToString(), message);
            eventInfo.Properties.Add("logType", logType);
            eventInfo.Properties.Add("appheader", header);
            eventInfo.Properties.Add("responseTime", responceTime);
            logger.Log(eventInfo);
        }
        public void LogMessage(LoggerRequestDto request, Type? type = null, LoggerNames loggerNames = LoggerNames.writeToLog1)
        {

            ILogger logger = (type == null)
                                ? LogManager.GetCurrentClassLogger()
                                : LogManager.GetLogger(type.FullName);
            LogEventInfo eventInfo = new LogEventInfo();

            if (request.exception != null)
            {
                eventInfo = new LogEventInfo(LogLevel.Error, loggerNames.ToString(), request.message);
                eventInfo.Exception = request.exception;
                foreach (PropertyInfo prop in typeof(LoggerRequestDto).GetProperties())
                {
                    eventInfo.Properties.Add(prop.Name, prop.GetValue(request, null));
                }
                logger.Log(typeof(LoggerService), eventInfo);
            }
            else
            {
                eventInfo = new LogEventInfo(LogLevel.Trace, loggerNames.ToString(), request.message);
                foreach (PropertyInfo prop in typeof(LoggerRequestDto).GetProperties())
                {
                    eventInfo.Properties.Add(prop.Name, prop.GetValue(request, null));
                }
                logger.Log(typeof(LoggerService), eventInfo);

            }
        }
        public string ToJson(object value)
        {
            string str = "";
            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    StringEscapeHandling = StringEscapeHandling.Default

                };

                str = JsonConvert.SerializeObject(value, settings);
            }
            catch (Exception ex)
            {

            }
            return str;
        }
        private void AddLoggerMessage(string message, LogLevel logLevel, Type? type, LoggerNames? loggerName)
        {
            ILogger logger = (type == null)
                                ? LogManager.GetCurrentClassLogger()
                                : LogManager.GetLogger(type.FullName);

            LogEventInfo eventInfo = new LogEventInfo(logLevel, logger.Name, message);
            eventInfo.Properties.Add("logType", logLevel.ToString());
            logger.Log(eventInfo);
        }
        private void AddLoggerException(Exception ex, LogLevel logLevel, Type? type, LoggerNames? loggerName)
        {
            ILogger logger = (type == null)
                                ? LogManager.GetCurrentClassLogger()
                                : LogManager.GetLogger(type.FullName);

            LogEventInfo eventInfo = new LogEventInfo(logLevel, logger.Name, ex.Message);
            eventInfo.Properties.Add("logType", logLevel.ToString());
            eventInfo.Exception = ex;
            logger.Log(eventInfo);
        }


    }
}
