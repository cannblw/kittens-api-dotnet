using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KittensApi.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(
            RequestDelegate next,
            ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error");

                httpContext.Response.StatusCode = 500;
            }
        }

        private async Task SendErrorResponse(HttpContext httpContext, Exception exception, HttpStatusCode statusCode)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;

            var responseBody = new ObjectResult(new
            {
                errorMessage = exception.Message,
            }) { StatusCode = (int)statusCode };
            
            var serializedBody = JsonSerializer.Serialize(responseBody.Value);

            await httpContext.Response.WriteAsync(serializedBody);
        }
    }
}