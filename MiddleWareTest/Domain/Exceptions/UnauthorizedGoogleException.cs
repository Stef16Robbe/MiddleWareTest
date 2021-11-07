using System;

namespace MiddleWareTest.Domain.Exceptions
{
    public class UnauthorizedGoogleException : Exception
    {
        public UnauthorizedGoogleException(string originalMessage = "") : base(
            "You are unauthorized to perform this request.")
        {
        }
    }
}