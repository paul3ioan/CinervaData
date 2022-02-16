using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;
namespace Cinerva.Web.MiddleWare
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogMiddleware(RequestDelegate next)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ILogger logger = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File(config["LogFile"], rollingInterval: RollingInterval.Day)
               .CreateLogger();

            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = GetHeaders(context);
            _logger.Information(header);

            await _next(context);
            _logger.Information(context.Response.StatusCode.ToString() + Environment.NewLine);
        }
        private string GetHeaders(HttpContext httpContext)
        {
            var header = new StringBuilder();

            foreach (var item in httpContext.Request.Headers)
            {
                header.Append($"{item.Value}: {item.Value.ToString()} {Environment.NewLine}");
            }
            return header.ToString();
        }

    }
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
