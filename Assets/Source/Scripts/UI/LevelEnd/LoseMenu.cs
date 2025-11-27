using System;
using FiniteStateMachine;
using UnityEngine;

namespace UI.LevelEnd
{
    public class LoseMenu : EndMenu<PlayerStateMachine>
    {
        [SerializeField] private int _scoreReduceValue;
        
        protected override int GetNewScore(int currentScore)
        {
            if (_scoreReduceValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(_scoreReduceValue));
            
            int newScore = currentScore - _scoreReduceValue;
            
            return Mathf.Clamp(newScore, 0, newScore);
        }
    }
}