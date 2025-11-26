using TMPro;
using UI.LevelEnd;
using UnityEngine;

namespace YG.Awards
{
    public class MultiplyAwardPanel : AwardPanel
    {
        private const int OneMinus = 1;
        
        [SerializeField] private TMP_Text _multipliedAwardText;
        [SerializeField] private Chest _chest;
        [SerializeField, Min(1)] private int _multiplier;
        
        private void Start()
        {
            _multipliedAwardText.text = (_chest.AwardAmount * _multiplier).ToString();
        }

        protected override void AddAward()
        {
            int addAmount = _multiplier - OneMinus;

            for (int i = 0; i < addAmount; i++)
                _chest.AddAward();

            SetEnable(false);
        }
    }
}