using System;

namespace KittensApi.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private static readonly string _message = "User already exists";
                
        public UserAlreadyExistsException() : base(_message)
        {
        }
    }
}
