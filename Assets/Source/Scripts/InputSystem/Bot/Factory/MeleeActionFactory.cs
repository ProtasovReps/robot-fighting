using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;

namespace InputSystem.Bot.Factory
{
    public class MeleeActionFactory : ActionFactory
    {
        protected override void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer)
        {
            BotAction upAttack = stash.Get(MotionHashes.ArmAttack);
            BotAction downAttack = stash.Get(MotionHashes.LegAttack);
            BotAction block = stash.Get(MotionHashes.Block);
            
            disposer.Add(new RandomActionExecutor<PlayerNearbyState>(
                machine,
                block,
                upAttack,
                downAttack));
            
            disposer.Add(new RandomActionExecutor<ValidAttackDistanceState>(
                machine,
                upAttack, 
                downAttack));
            
            disposer.Add(upAttack);
            disposer.Add(downAttack);
            disposer.Add(block);
        }
    }
}