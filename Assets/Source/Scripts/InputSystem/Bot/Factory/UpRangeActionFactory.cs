using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory
{
    public class UpRangeActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction special = stash.Get(MotionHashes.Special);
            BotAction block = stash.Get(MotionHashes.Block);
            
            disposer.Add(new RandomActionExecutor<PlayerNearbyState>(
                machine,
                block,
                downAttack,
                special));
            
            disposer.Add(new RandomActionExecutor<ValidAttackDistanceState>(machine, upAttack));
            
            disposer.Add(upAttack);
            disposer.Add(downAttack);
            disposer.Add(special);
            disposer.Add(block);
        }
    }
}