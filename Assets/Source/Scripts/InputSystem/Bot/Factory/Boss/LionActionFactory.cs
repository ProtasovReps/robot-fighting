using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory.Boss
{
    public class LionActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction super = stash.Get(MotionHashes.Super);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction special = stash.Get(MotionHashes.Special);
            
            new RandomActionExecutor<PlayerNearbyState>(machine, super, special);
            new RandomActionExecutor<ValidAttackDistanceState>(machine, upAttack, downAttack);
        }
    }
}