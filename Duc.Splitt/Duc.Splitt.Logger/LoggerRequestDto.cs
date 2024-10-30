namespace Duc.Splitt.Logger
{
    public class LoggerRequestDto
    {

        public string? module { get; set; }
        public string? method { get; set; }
        public string? methodType { get; set; }
        public string? logType { get; set; }
        public string? responseTime { get; set; }
        public string? traceIdentifier { get; set; }
        public string? statusCode { get; set; }
        public string? queryParam { get; set; }
        public string? message { get; set; }

        public string? param1 { get; set; }
        public string? param2 { get; set; }
        public string? param3 { get; set; }
        public string? param4 { get; set; }
        public string? param5 { get; set; }
        public string? param6 { get; set; }
        public string? param7 { get; set; }
        public string? param8 { get; set; }
        public string? param9 { get; set; }
        public string? param10 { get; set; }
        public Exception? exception { get; set; }
        public string? ipAddress { get; set; }
        public string? userAgent { get; set; }
        public string? refURL { get; set; }
    }
}
