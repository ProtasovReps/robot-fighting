using Cysharp.Threading.Tasks;
using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace UI.Guide
{
    public class SuperAttackGuide : MonoBehaviour
    {
        private const float EnabledTimeScale = 0.3f;
        private const float DefaultTimeScale = 1f;
        
        [SerializeField] private DistanceValidator _distanceValidator;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _shadow;
        [SerializeField] private Replic _mobileReplic;
        [SerializeField] private Replic _pCReplic;
        [SerializeField] private Transform[] _objectsToDisable;

        [Inject]
        private void Inject(PlayerStateMachine playerStateMachine)
        {
            playerStateMachine.Value
                .Where(state => state is AttackState)
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
            Time.timeScale = DefaultTimeScale;
        }

        private async UniTaskVoid CheckDistance()
        {
            while (true)
            {
                if (_distanceValidator.IsValidDistance(_player.position) == false)
                {
                    Time.timeScale = EnabledTimeScale;

                    if (YG2.envir.isDesktop)
                    {
                        _pCReplic.gameObject.SetActive(true);
                    }
                    else
                    {
                        _mobileReplic.gameObject.SetActive(true);
                    }

                    _shadow.gameObject.SetActive(true);
                    break;
                }

                await UniTask.Yield();
            }
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