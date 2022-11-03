namespace MediatrTutorial.Infrastructure.Behaviours;

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull {
    private readonly ILogger<RequestPerformanceBehavior<TRequest, TResponse>> _logger;

    public RequestPerformanceBehavior(ILogger<RequestPerformanceBehavior<TRequest, TResponse>> logger) {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > TimeSpan.FromSeconds(5).TotalMilliseconds) {
            // This method has taken a long time, So we log that to check it later.
            _logger.LogWarning($"{request} has taken {stopwatch.ElapsedMilliseconds} to run completely !");
        }

        return response;
    }
}