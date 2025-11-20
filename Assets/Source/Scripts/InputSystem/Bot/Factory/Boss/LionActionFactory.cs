using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory.Boss
{
    public class LionActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction super = stash.Get(MotionHashes.Super);
            BotAction down = stash.Get(MotionHashes.LegAttack);
            BotAction special = stash.Get(MotionHashes.Special);
            
            disposer.Add(new RandomActionExecutor<PlayerNearbyState>(machine, super, special));
            disposer.Add(new RandomActionExecutor<ValidAttackDistanceState>(machine, upAttack, down));
            disposer.Add(upAttack);
            disposer.Add(super);
            disposer.Add(down);
            disposer.Add(special);
        }
    }
}