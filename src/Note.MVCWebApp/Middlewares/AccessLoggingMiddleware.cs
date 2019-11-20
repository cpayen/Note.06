using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Note.MVCWebApp.Middlewares
{
    class AccessLoggingMiddleware
    {
        protected readonly RequestDelegate _next;
        protected readonly ILogger<AccessLoggingMiddleware> _logger;

        public AccessLoggingMiddleware(RequestDelegate next, ILogger<AccessLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var ellapsed = sw.Elapsed.TotalMilliseconds;
            await _next(context);
            HandleLogging(context, ellapsed, _logger);
        }

        private static void HandleLogging(HttpContext context, double ellapsed, ILogger<AccessLoggingMiddleware> logger)
        {
            var code = context.Response.StatusCode;
            var method = context.Request.Method;
            var path = context.Request.Path;
            var user = context.User.Identity.Name ?? "Anonymous";

            var log = $"{code} - HTTP {method} {path} in {ellapsed:0.0000} ms (User: {user})";

            switch (code)
            {
                case (int)HttpStatusCode.OK:
                case (int)HttpStatusCode.Created:
                case (int)HttpStatusCode.NoContent:
                case (int)HttpStatusCode.Found:
                    logger.LogInformation(log);
                    break;

                case (int)HttpStatusCode.InternalServerError:
                    logger.LogCritical(log);
                    break;

                case (int)HttpStatusCode.NotFound:
                    logger.LogError(log);
                    break;

                default:
                    logger.LogError(log);
                    break;
            }
        }
    }
}