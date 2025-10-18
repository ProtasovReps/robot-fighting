using System;
using System.Collections.Generic;
using CharacterSystem.Parameters;
using Extensions;

namespace InputSystem.Bot
{
    public class ActionStash
    {
        private readonly Dictionary<int, BotAction> _actions;

        public ActionStash(BotMoveInput moveInput, BotFightInput fightInput, BotParameters botParameters)
        {
            Action armAttack = fightInput.GetAction(MotionHashes.ArmAttack);
            Action legAttack = fightInput.GetAction(MotionHashes.LegAttack);
            Action special = fightInput.GetAction(MotionHashes.Special);
            Action super = fightInput.GetAction(MotionHashes.Super);
            Action block = fightInput.GetAction(MotionHashes.Block);
            
            _actions = new Dictionary<int, BotAction>
            {
                { MotionHashes.MoveLeft, new(moveInput.MoveLeft, botParameters.MoveDuration) },
                { MotionHashes.MoveRight, new(moveInput.MoveRight, botParameters.MoveDuration) },
                { MotionHashes.Idle, new(moveInput.Stop, botParameters.MoveDuration / 2f) }, // idleDuration
                { MotionHashes.ArmAttack, new(armAttack, botParameters.AttackDelay) }, // не attackDelay, скорее UpAttackDuration брать
                { MotionHashes.LegAttack, new(legAttack, botParameters.AttackDelay) }, // downDuration
                { MotionHashes.Special, new(special, botParameters.AttackDelay) },// special duration
                { MotionHashes.Super, new(super, botParameters.AttackDelay) }, // super duration
                { MotionHashes.Block, new(block, botParameters.BlockDuration) }
            };
        }
        
        public BotAction Get(int motionHash)
        {
            if (_actions.ContainsKey(motionHash) == false)
                throw new KeyNotFoundException(nameof(motionHash));

            return _actions[motionHash];
        }
    }
}