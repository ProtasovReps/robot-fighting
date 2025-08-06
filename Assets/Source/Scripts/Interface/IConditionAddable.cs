using System;
using FiniteStateMachine.States;
using R3;

namespace Interface
{
    public interface IConditionAddable
    {
        void Add<TKeyState>(Func<Unit, bool> condition) where TKeyState : State;
    }
}