using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SimpleLogger.Models;

namespace SimpleLogger.Middleware
{
    public class SimpleLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly Func<HttpContextLogModel, object> _formatter;

        // ReSharper disable once SuggestBaseTypeForParameter
        public SimpleLoggerMiddleware(RequestDelegate next, ILogger<SimpleLoggerMiddleware> logger,
            Func<HttpContextLogModel, object> formatter)
        {
            _next = next;
            _logger = logger;
            // ReSharper disable once RedundantDelegateCreation
            _formatter = formatter ?? new Func<HttpContextLogModel, object>(x => x);
        }

        public async Task Invoke(HttpContext context)
        {
            string responseBody;
            var originalBodyStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await _next(context);

                responseBody = await GetResponseBody(context.Response.Body);

                await memoryStream.CopyToAsync(originalBodyStream);
            }

            var requestBody = await GetRequestBody(context.Request);

            var logContext = new HttpContextLogModel
            {
                Method = context.Request.Method,
                Host = context.Request.Host.ToString(),
                Headers = context.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                Path = context.Request.Path.ToString(),
                QueryString = context.Request.QueryString.ToString(),
                RequestBody = requestBody,
                ResponseBody = responseBody
            };

            var payload = _formatter(logContext);
            
            _logger.Log(
                LogLevel.Information,
                new EventId(),
                payload,
                null, (log, exception) => log.ToString()
            );
        }

        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            if (request.ContentLength.HasValue && request.ContentLength > 0)
            {
                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }

            return string.Empty;
        }

        private static async Task<string> GetResponseBody(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            var bodyString = await new StreamReader(body).ReadToEndAsync();
            body.Seek(0, SeekOrigin.Begin);

            return bodyString;
        }
    }
}