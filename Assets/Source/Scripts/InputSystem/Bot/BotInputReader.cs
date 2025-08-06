using Interface;
using R3;
using UnityEngine;

namespace InputSystem.Bot
{
    public class BotInputReader : MonoBehaviour, IDirectionChangeable
    {
        private BotMovement _botMovement;
        private BotAttack _botAttack;
        private ReactiveProperty<float> _direction;

        public ReadOnlyReactiveProperty<float> Direction => _direction;
        public Observable<Unit> UpAttackPressed => _botAttack.UpAttack;
        public Observable<Unit> DownAttackPressed => _botAttack.DownAttack;
        
        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_botMovement.Direction))
                .AddTo(this);
        }

        public void Initialize(BotMovement botMovement, BotAttack botAttack)
        {
            _botMovement = botMovement;
            _botAttack = botAttack;
            _direction = new ReactiveProperty<float>();
        }
    }
}