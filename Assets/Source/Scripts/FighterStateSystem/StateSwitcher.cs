using System;
using InputSystem;

namespace FighterStateSystem
{
    public class StateSwitcher
    {
        private readonly CharacterStateMachine _machine;
        private readonly InputReader _inputReader;
        
        public StateSwitcher(CharacterStateMachine machine, InputReader inputReader)
        {
            if(machine == null)
                throw new ArgumentNullException(nameof(machine));
            
            if(inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));
            
            _machine = machine;
            _inputReader = inputReader;
        }
    }
}