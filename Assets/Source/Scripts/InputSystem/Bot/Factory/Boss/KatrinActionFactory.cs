using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory.Boss
{
    public class KatrinActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction super = stash.Get(MotionHashes.Super);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction block = stash.Get(MotionHashes.Block);
            
            new RandomActionExecutor<PlayerNearbyState>(machine, upAttack, downAttack, block);
            new RandomActionExecutor<ValidAttackDistanceState>(machine, super);
        }
    }
}