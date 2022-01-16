using System;
using System.Net;

namespace KittensApi.Exceptions
{
    public class KnownErrorException : Exception
    {
        public HttpStatusCode? httpStatusCode { get; }
        public KnownErrorException(string? message, HttpStatusCode? statusCode) : base(message)
        {
            httpStatusCode = statusCode;
        }
    }
}
