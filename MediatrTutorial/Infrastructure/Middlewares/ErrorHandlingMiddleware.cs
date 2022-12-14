namespace MediatrTutorial.Infrastructure.Middlewares;

using System;
using System.Net;
using System.Threading.Tasks;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ErrorHandlingMiddleware {
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        int statusCode;
        object? errors = default;

        if (exception is RestException re) {
            statusCode = (int)re.Code;

            if (re.Message is string) {
                errors = new[] { re.Message };
            }
        }
        else {
            statusCode = (int)HttpStatusCode.InternalServerError;
            errors = "An internal server error has occurred.";
        }

        _logger.LogError($"{errors} - {exception.Source} - {exception.Message} - {exception.StackTrace} - {exception.TargetSite?.Name}");

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new {
            errors
        }));
    }
}