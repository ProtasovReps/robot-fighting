using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory
{
    public class BorschActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            
            disposer.Add(new SoloActionExecutor<ValidAttackDistanceState>(machine, upAttack));
        }
    }
}