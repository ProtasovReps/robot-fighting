using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory.Boss
{
    public class KatrinActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction super = stash.Get(MotionHashes.Super);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction block = stash.Get(MotionHashes.Block);
            
            disposer.Add(new RandomActionExecutor<PlayerNearbyState>(machine, upAttack, downAttack, block));
            disposer.Add(new RandomActionExecutor<ValidAttackDistanceState>(machine, super));
            disposer.Add(upAttack);
            disposer.Add(super);
            disposer.Add(downAttack);
            disposer.Add(block);
        }
    }
}