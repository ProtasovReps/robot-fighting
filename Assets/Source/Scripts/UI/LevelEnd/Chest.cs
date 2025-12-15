using System;
using Interface;
using Reflex.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.LevelEnd
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _minAwardValue;
        [SerializeField] [Min(0)] private int _maxAwardValue;

        private IMoneyAddable _moneyAddable;

        public int AwardAmount { get; private set; }

        [Inject]
        private void Inject(IMoneyAddable moneyAddable)
        {
            if (_minAwardValue > _maxAwardValue)
            {
                throw new ArgumentOutOfRangeException(nameof(_minAwardValue));
            }

            _moneyAddable = moneyAddable;
            AwardAmount = Random.Range(_minAwardValue, _maxAwardValue);
        }

        public void AddAward()
        {
            _moneyAddable.Add(AwardAmount);
        }
    }
}