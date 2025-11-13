using R3;

namespace UI.Buttons
{
    public class UnitButton : ObservableButton<Unit> 
    {
        protected override Unit GetReturnable()
        {
            return Unit.Default;
        }
    }
}