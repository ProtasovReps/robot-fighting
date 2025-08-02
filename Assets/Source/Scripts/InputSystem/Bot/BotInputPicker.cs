using System;
using Interface;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InputSystem
{
    public class BotInputPicker<TTargetState> : IDisposable
        where TTargetState : IState
    {
        private readonly IBotInput[] _botInputs;
        private readonly CompositeDisposable _subscriptions;
        
        private IDisposable _currentSubscription;
        private IBotInput _currentInput;
        
        public BotInputPicker(IStateMachine stateMachine, params IBotInput[] botInputs)
        {
            if (botInputs == null)
                throw new ArgumentNullException(nameof(botInputs));
            
            _botInputs = botInputs;
            _subscriptions = new CompositeDisposable(2);
            
            stateMachine.CurrentState
                .Where(state => state.Type == typeof(TTargetState))
                .Subscribe(_ => ActivateRandom())
                .AddTo(_subscriptions);

            stateMachine.CurrentState
                .Pairwise()
                .Where(pair => pair.Previous.Type == typeof(TTargetState) 
                               && pair.Current.Type != typeof(TTargetState))
                .Subscribe(_ => Deactivate())
                .AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
            Deactivate();
        }

        private void ActivateRandom()
        {
            _currentInput = GetRandomAction();
            _currentSubscription = _currentInput.Executed
                .Subscribe(_ => Reactivate());
            
            _currentInput.Activate();
        }

        private void Reactivate()
        {
            Deactivate();
            ActivateRandom();
        }
        
        private void Deactivate()
        {
            _currentSubscription?.Dispose();
            _currentInput?.Deactivate();
        }

        private IBotInput GetRandomAction()
        {
            int randomIndex = Random.Range(0, _botInputs.Length);
            return _botInputs[randomIndex];
        }
    }
}