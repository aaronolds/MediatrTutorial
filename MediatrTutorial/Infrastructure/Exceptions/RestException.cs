namespace MediatrTutorial.Infrastructure.Exceptions;

using System;
using System.Net;

public class RestException : Exception {
    public RestException(HttpStatusCode code, object message = default!) {
        Code = code;
        Message = message;
    }

    public HttpStatusCode Code { get; }

    public new object Message { get; }
}