using FiniteStateMachine;
using Reflex.Attributes;
using YG.Saver;

namespace UI.LevelEnd
{
    public class VictoryMenu : EndMenu<BotStateMachine>
    {
        private ProgressSaver _progressSaver;
        
        [Inject]
        private void Inject(ProgressSaver progressSaver)
        {
            _progressSaver = progressSaver;
        }

        protected override void Appear()
        {
            _progressSaver.Add(new LevelSaver());
            base.Appear();
        }
    }
}