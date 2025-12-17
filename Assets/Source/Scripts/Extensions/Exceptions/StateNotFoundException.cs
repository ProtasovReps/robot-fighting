using System;

namespace Extensions.Exceptions
{
    public class StateNotFoundException : NullReferenceException
    {
        public StateNotFoundException(string paramName)
            : base(paramName)
        {
        }
    }
}