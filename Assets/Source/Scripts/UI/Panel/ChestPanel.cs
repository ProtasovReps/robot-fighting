using System;
using R3;
using TMPro;
using UI.Buttons;
using UI.Effect;
using UI.LevelEnd;
using UnityEngine;

namespace UI.Panel
{
    public class ChestPanel : MonoBehaviour
    {
        [SerializeField] private UnitButton _openButton;
        [SerializeField] private TMP_Text _awardAmount;
        [SerializeField] private ChestEffect _effect;
        [SerializeField] private Chest _chest;
        
        private IDisposable _subcription;
        private float _addAmount;

        private void Awake()
        {
            _subcription = _openButton.Pressed
                .Subscribe(_ => ShowAward());
        }

        private void ShowAward()
        {
            _subcription.Dispose();

            _chest.AddAward();
            _awardAmount.text = _chest.AwardAmount.ToString();

            _effect.StartEffect().Forget();
        }
    }
}