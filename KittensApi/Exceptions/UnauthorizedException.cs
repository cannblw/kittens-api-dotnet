using System;

namespace KittensApi.Exceptions
{
    public class UnauthorizedException : Exception
    {
        private static readonly string _message = "Bad credentials";
                
        public UnauthorizedException() : base(_message)
        {
        }
    }
}
