using System;

namespace Interface
{
    public interface IStateChangeable
    {
        Type CurrentState { get; }
    }
}