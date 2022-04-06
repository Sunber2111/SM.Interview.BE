using System;
using System.Threading.Tasks;
using Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            object errors = null;

            switch (ex)
            {
                case AppError er:
                    _logger.LogError(er.Message, "REST ERROR");
                    context.Response.StatusCode = 400;
                    errors = new { status = "Fail", error = er.Message, code = er.ErrorCode };
                    break;
                case Exception e:
                    _logger.LogError(e.Message, "SERVER ERROR");
                    errors = new { status = "Fail", error = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message, code = 500 };
                    context.Response.StatusCode = 500;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(errors);

                await context.Response.WriteAsync(result);
            }

        }
    }
}
