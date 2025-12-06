using FiniteStateMachine.States;
using Interface;

namespace HitSystem.HitTypes
{
    public class DownHit : Hit
    {
        public DownHit(float stunDuration, HitReader hitReader, IConditionAddable conditionAddable)
            : base(stunDuration, hitReader.LegsHitted)
        {
            conditionAddable.Add<DownHittedState>(_ => IsContinuing);
        }
    }
}