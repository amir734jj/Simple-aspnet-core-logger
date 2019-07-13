using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Middleware
{
    public class SimpleLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        // ReSharper disable once SuggestBaseTypeForParameter
        public SimpleLoggerMiddleware(RequestDelegate next, ILogger<SimpleLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var payload = new
            {
                Header = context.Request.Headers.ToDictionary(x => x.Key, x => x.Value.Select(y => y)),
                Request = await GetRequestBody(context.Request),
                Response = await GetResponseBody(context.Response)
            };

            _logger.Log(LogLevel.Information, "ContextLoggerMiddleware", payload);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        /// <summary>
        ///     Gets the body of HttpRequest as a string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            if (request.ContentLength.HasValue)
            {
                var contentLength = request.ContentLength;
                const long num = 0;
                if ((contentLength.GetValueOrDefault() > num ? 1 : 0) != 0)
                {
                    using (var streamReader = new StreamReader(request.Body, Encoding.UTF8))
                        return await streamReader.ReadToEndAsync();
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts stream to a string
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static async Task<string> GetResponseBody(HttpResponse response)
        {
            var body = response.Body;
            //body.Position = 0;
            //var endAsync = await new StreamReader(body).ReadToEndAsync();
            //body.Position = 0;
            return "response";
        }
    }
}