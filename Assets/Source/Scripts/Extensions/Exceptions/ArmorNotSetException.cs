using System;

namespace Extensions.Exceptions
{
    public class ArmorNotSetException : NullReferenceException
    {
        public ArmorNotSetException(string paramName) : base(paramName)
        {
        }
    }
}