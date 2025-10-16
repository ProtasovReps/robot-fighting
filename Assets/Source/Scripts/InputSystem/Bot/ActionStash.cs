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
            _actions = new Dictionary<int, BotAction>
            {
                { MotionHashes.MoveLeft, new(moveInput.MoveLeft, botParameters.MoveDuration) },
                { MotionHashes.MoveRight, new(moveInput.MoveRight, botParameters.MoveDuration) },
                { MotionHashes.Idle, new(moveInput.Stop, botParameters.MoveDuration / 2f) }, // idleDuration
                { MotionHashes.ArmAttack, new(fightInput.AttackUp, botParameters.AttackDelay) }, // не attackDelay, скорее UpAttackDuration брать
                { MotionHashes.LegAttack, new(fightInput.AttackDown, botParameters.AttackDelay) }, // downDuration
                { MotionHashes.Block, new(fightInput.BlockAttack, botParameters.BlockDuration) },
                { MotionHashes.Special, new(fightInput.AttackSpecial, botParameters.AttackDelay) } // special duration
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