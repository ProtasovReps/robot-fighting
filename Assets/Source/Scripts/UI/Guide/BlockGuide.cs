using FiniteStateMachine.States;

namespace UI.Guide
{
    public class BlockGuide : StateDependentGuide<BlockState>
    {
        protected override bool IsValidCondition()
        {
            return true;
        }
    }
}