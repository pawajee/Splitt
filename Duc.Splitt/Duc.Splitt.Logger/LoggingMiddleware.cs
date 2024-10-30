using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;

namespace Duc.Splitt.Logger
{
    public class LoggingMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// The logger
        /// </summary>
        private ILoggerService _logger;

        private IConfiguration _configuration;

        public LoggingMiddleware(RequestDelegate next, IConfiguration configuration, ILoggerService logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="logger">The logger.</param>
        public async Task Invoke(HttpContext httpContext, ILoggerService logger)
        {
            _logger = logger;

            // bool excludeSensitiveApis = _configuration.GetValue<bool>("ExcludeSensitiveApis");
            List<string>? requestLogExcludedApis = _configuration.GetSection("RequestLogExcludedApis").Get<List<string>>();
            bool isExcludeSensitiveApis = false;
            if (requestLogExcludedApis != null && requestLogExcludedApis.Count > 0 && requestLogExcludedApis.Contains(httpContext.Request.Path.ToString().ToLower()))
            {
                isExcludeSensitiveApis = true;
                // await _next(httpContext);
            }
            if (httpContext.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                var stopWatch = Stopwatch.StartNew();
                await LogApiRequest(httpContext, isExcludeSensitiveApis);
                await LogApiResponse(httpContext, stopWatch, isExcludeSensitiveApis);
            }
            else
            {
                await _next(httpContext);
            }

        }

        /// <summary>
        /// Logs the API response.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="stopWatch">The stop watch.</param>
        private async Task LogApiResponse(HttpContext httpContext, Stopwatch stopWatch, bool IsExcludeSensitiveApis = false)
        {
            var request = httpContext.Request;
            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                var response = httpContext.Response;
                response.Body = responseBody;
                await _next(httpContext);
                stopWatch.Stop();
                var responseBodyContent = await ReadResponseBody(response);
                await responseBody.CopyToAsync(originalBodyStream);

                _logger.LogRequestResponce(IsExcludeSensitiveApis && response.StatusCode == 200 ? $"StatusCode:{response.StatusCode}" : await ReadResponseBody(response), this.GetType(), Enums.LogTypes.Response.ToString(), $"{stopWatch.Elapsed.ToString(@"mm\:ss\.fff")}");

            }
        }

        /// <summary>
        /// Logs the API request.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        private async Task LogApiRequest(HttpContext httpContext, bool IsExcludeSensitiveApis = false)
        {
            var request = httpContext.Request;
            var requestTime = DateTime.Now;
            var requestBodyContent = IsExcludeSensitiveApis ? "" : await ReadRequestBody(request);

            _logger.LogRequestResponce(requestBodyContent, this.GetType(), Enums.LogTypes.Request.ToString(), "00:00:00", _logger.ToJson(request.Headers));

        }


        /// <summary>
        /// Reads the request body.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            //var buffer = new byte[Convert.ToInt32(request.ContentLength * 9)];
            //await request.Body.ReadAsync(buffer, 0, buffer.Length);
            //var bodyAsText = Encoding.UTF8.GetString(buffer);

            await using var stream = new MemoryStream();
            request.Body.Seek(0, SeekOrigin.Begin);
            await request.Body.CopyToAsync(stream);
            var requestBody = Encoding.UTF8.GetString(stream.ToArray());
            request.Body.Seek(0, SeekOrigin.Begin);
            return requestBody;
        }

        /// <summary>
        /// Reads the response body.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>System.String.</returns>
        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;

        }
    }

}
