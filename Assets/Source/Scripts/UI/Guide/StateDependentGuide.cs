using Cysharp.Threading.Tasks;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace UI.Guide
{
    public abstract class StateDependentGuide<TState> : MonoBehaviour
        where TState : State
    {
        [SerializeField] private float _enabledTimeScale = 0.3f;
        [SerializeField] private float _defaultTimeScale = 1f;
        [SerializeField] private Transform _shadow;
        [SerializeField] private Transform _mobileInput;
        [SerializeField] private Transform _pCInput;
        [SerializeField] private Transform[] _objectsToDisable;

        [Inject]
        private void Inject(PlayerStateMachine playerStateMachine)
        {
            playerStateMachine.Value
                .Where(state => state is TState)
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }

        private void Awake()
        {
            if (YG2.saves.IsGuidePassed)
            {
                return;
            }

            SetObjectsActive(false);
            CheckDistance().Forget();
        }

        private void OnDisable()
        {
            SetObjectsActive(true);
            Time.timeScale = _defaultTimeScale;
        }

        protected abstract bool IsValidCondition();

        private async UniTaskVoid CheckDistance()
        {
            await UniTask.WaitUntil(IsValidCondition);

            Time.timeScale = _enabledTimeScale;

            if (YG2.envir.isDesktop)
            {
                _pCInput.gameObject.SetActive(true);
            }
            else
            {
                _mobileInput.gameObject.SetActive(true);
            }

            _shadow.gameObject.SetActive(true);
        }

        private void SetObjectsActive(bool isActive)
        {
            for (int i = 0; i < _objectsToDisable.Length; i++)
            {
                _objectsToDisable[i].gameObject.SetActive(isActive);
            }
        }
    }
}