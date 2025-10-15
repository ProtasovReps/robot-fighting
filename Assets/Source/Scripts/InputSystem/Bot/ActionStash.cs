using System.Collections.Generic;
using CharacterSystem.Data;
using Extensions;

namespace InputSystem.Bot
{
    public class ActionStash
    {
        private readonly Dictionary<int, BotAction> _actions;

        public ActionStash(BotMoveInput moveInput, BotFightInput fightInput, BotData botData)
        {
            _actions = new Dictionary<int, BotAction>
            {
                { MotionHashes.MoveLeft, new(moveInput.MoveLeft, botData.MoveDuration) },
                { MotionHashes.MoveRight, new(moveInput.MoveRight, botData.MoveDuration) },
                { MotionHashes.Idle, new(moveInput.Stop, botData.MoveDuration / 2f) }, // idleDuration
                { MotionHashes.ArmAttack, new(fightInput.AttackUp, botData.AttackDelay) }, // не attackDelay, скорее UpAttackDuration брать
                { MotionHashes.LegAttack, new(fightInput.AttackDown, botData.AttackDelay) }, // downDuration
                { MotionHashes.Block, new(fightInput.BlockAttack, botData.BlockDuration) },
                { MotionHashes.Special, new(fightInput.AttackSpecial, botData.AttackDelay) } // special duration
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