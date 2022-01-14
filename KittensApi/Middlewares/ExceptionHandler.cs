using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using KittensApi.Dto.Details;
using KittensApi.Exceptions;
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
            catch (UnauthorizedException ex)
            {
                _logger.LogError(ex, "User not authorized to access endpoint");
                await SendErrorResponse(httpContext, ex, HttpStatusCode.Unauthorized);
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

            var responseBody = new ObjectResult(new ErrorDetails
            {
                ErrorMessage = exception.Message,
            }) { StatusCode = (int)statusCode };
            
            var serializedBody = JsonSerializer.Serialize(responseBody.Value);

            await httpContext.Response.WriteAsync(serializedBody);
        }
    }
}