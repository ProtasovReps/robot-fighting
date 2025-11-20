using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;
using UnityEngine;

namespace InputSystem.Bot.Factory
{
    public abstract class ActionFactory : MonoBehaviour
    {
        public void InstallActions(
            ActionStash stash,
            BotMoveInput moveInput,
            BotInputStateMachine stateMachine,
            Disposer disposer)
        {
            BotAction leftMove = stash.Get(MotionHashes.MoveLeft);
            BotAction rightMove = stash.Get(MotionHashes.MoveRight);
            BotAction idle = stash.Get(MotionHashes.Idle);
            BotAction special = stash.Get(MotionHashes.Special);
            
            disposer.Add(new NothingNearbyActionExecutor(stateMachine, moveInput, leftMove, rightMove, idle));
            disposer.Add(new SoloActionExecutor<WallNearbyState>(stateMachine, leftMove));
            disposer.Add(new SoloActionExecutor<WallOpponentNearbyState>(stateMachine, special));
            
            AddActions(stash, stateMachine, disposer);
        }
        
        protected abstract void AddActions(ActionStash stash, BotInputStateMachine machine, Disposer disposer);
    }
}