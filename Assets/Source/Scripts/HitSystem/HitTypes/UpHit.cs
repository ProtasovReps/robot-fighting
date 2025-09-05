using FiniteStateMachine.States;
using Interface;

namespace HitSystem
{
    public class UpHit : Hit
    {
        public UpHit(float stunDuration, HitReader hitReader, IConditionAddable conditionAddable)
            : base(stunDuration, hitReader.TorsoHitted)
        {
            conditionAddable.Add<UpHittedState>(_ => IsContinuing);
        }
    }
}