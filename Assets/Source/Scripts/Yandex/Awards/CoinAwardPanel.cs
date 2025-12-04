using Interface;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace YG.Awards
{
    public class CoinAwardPanel : AwardPanel
    {
        private const string RewardMoneyWatched = nameof(RewardMoneyWatched);
        private const char AddSymbol = '+';
        
        [SerializeField, Min(1)] private int _minAddAmount;
        [SerializeField, Min(1)] private int _maxAddAmount;
        [SerializeField] private TMP_Text _amountView;

        private int _addAmount;
        private IMoneyAddable _moneyAddable;
        
        [Inject]
        private void Inject(IMoneyAddable moneyAddable)
        {
            _moneyAddable = moneyAddable;
        }

        private void Start()
        {
            _addAmount = Random.Range(_minAddAmount, _maxAddAmount);
            _amountView.text = $"{AddSymbol}{_addAmount}";
        }

        protected override void AddAward()
        {
            YG2.MetricaSend(RewardMoneyWatched);
            
            _moneyAddable.Add(_addAmount);
            SetEnable(false);
        }
    }
}