using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory.Boss
{
    public class RobertActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction super = stash.Get(MotionHashes.Super);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            
            new RandomActionExecutor<PlayerNearbyState>(machine, super, downAttack);
            new SoloActionExecutor<ValidAttackDistanceState>(machine, upAttack);
        }
    }
}