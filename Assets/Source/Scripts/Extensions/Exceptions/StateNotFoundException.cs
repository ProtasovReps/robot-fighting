using System;

namespace Extensions.Exceptions
{
    public class StateNotFoundException : ArgumentException
    {
        private const string ExceptionMessage = "State not found";

        public StateNotFoundException(string paramName) : base(ExceptionMessage, paramName)
        {
        }
    }
}