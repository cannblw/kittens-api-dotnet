using System;

namespace KittensApi.Exceptions
{
    public class KnownErrorException : Exception
    {
        public KnownErrorException(string? message) : base(message)
        {
        }
    }
}
