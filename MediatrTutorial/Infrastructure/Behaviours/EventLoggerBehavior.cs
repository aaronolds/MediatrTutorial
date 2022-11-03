namespace MediatrTutorial.Infrastructure.Behaviours;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.EventStore;
using EventStore.ClientAPI;
using MediatR;
using Newtonsoft.Json;

public class EventLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest :  IRequest<TResponse>
    where TResponse : notnull {
    private readonly IEventStoreDbContext _eventStoreDbContext;

    public EventLoggerBehavior(IEventStoreDbContext eventStoreDbContext) {
        _eventStoreDbContext = eventStoreDbContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)  {
        var response = await next();

        var requestName = request.ToString();

        // Commands convention
        if (requestName is not null && requestName.EndsWith("Command")) {
            var requestType = request.GetType();
            var commandName = requestType.Name;

            var data = new Dictionary<string, object> {
                {
                    "request", request
                }, {
                    "response", response
                }
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var dataBytes = Encoding.UTF8.GetBytes(jsonData);

            var eventData = new EventData(Guid.NewGuid(),
                commandName,
                true,
                dataBytes,
                null);

            await _eventStoreDbContext.AppendToStreamAsync(eventData);
        }

        return response;
    }
}