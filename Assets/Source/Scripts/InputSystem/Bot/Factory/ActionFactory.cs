using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using InputSystem.Bot.Executor;
using UnityEngine;

namespace InputSystem.Bot.Factory
{
    public abstract class ActionFactory : MonoBehaviour
    {
        public void InstallActions(ActionStash stash, BotMoveInput moveInput, BotInputStateMachine stateMachine)
        {
            BotAction leftMove = stash.Get(MotionHashes.MoveLeft);
            BotAction rightMove = stash.Get(MotionHashes.MoveRight);
            BotAction idle = stash.Get(MotionHashes.Idle);
            
            new NothingNearbyActionExecutor(stateMachine, moveInput, leftMove, rightMove, idle);
            new SoloActionExecutor<WallNearbyState>(stateMachine, leftMove);
            
            AddActions(stash, stateMachine);
        }
        
        protected abstract void AddActions(ActionStash stash, BotInputStateMachine machine);
    }
}