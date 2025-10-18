using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory
{
    public class RangeActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction special = stash.Get(MotionHashes.Special);
            BotAction block = stash.Get(MotionHashes.Block);
            
            new RandomActionExecutor<PlayerNearbyState>(machine, block, special);
            new RandomActionExecutor<ValidAttackDistanceState>(machine, upAttack, downAttack);
            new SoloActionExecutor<WallOpponentNearbyState>(machine, special);
        }
    }
}