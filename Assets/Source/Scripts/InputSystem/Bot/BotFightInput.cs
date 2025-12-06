using System;
using System.Collections.Generic;
using Extensions;
using R3;

namespace InputSystem.Bot
{
    public class BotFightInput
    {
        private readonly Dictionary<int, Subject<Unit>> _fightInputs;

        public BotFightInput()
        {
            _fightInputs = new Dictionary<int, Subject<Unit>>
            {
                { MotionHashes.ArmAttack, new Subject<Unit>() },
                { MotionHashes.LegAttack, new Subject<Unit>() },
                { MotionHashes.Special, new Subject<Unit>() },
                { MotionHashes.Super, new Subject<Unit>() },
                { MotionHashes.Block, new Subject<Unit>() }
            };
        }

        public Observable<Unit> GetObservable(int motionHash)
        {
            ValidateKey(motionHash);
            return _fightInputs[motionHash].AsObservable();
        }

        public Action GetAction(int motionHash)
        {
            ValidateKey(motionHash);
            return () => _fightInputs[motionHash].OnNext(Unit.Default);
        }

        private void ValidateKey(int motionHash)
        {
            if (_fightInputs.ContainsKey(motionHash) == false)
            {
                throw new KeyNotFoundException(nameof(motionHash));
            }
        }
    }
}