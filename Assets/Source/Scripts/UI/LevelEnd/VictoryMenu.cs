using System;
using FiniteStateMachine;
using Reflex.Attributes;
using UnityEngine;
using YG.Saver;

namespace UI.LevelEnd
{
    public class VictoryMenu : EndMenu<BotStateMachine>
    {
        [SerializeField] private int _addScoreValue;
        
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

        protected override int GetNewScore(int currentScore)
        {
            if (_addScoreValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_addScoreValue));
            }
            
            return currentScore + _addScoreValue;
        }
    }
}