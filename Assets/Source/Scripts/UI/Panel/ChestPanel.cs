using System;
using CharacterSystem;
using R3;
using TMPro;
using UI.Buttons;
using UI.VictoryMenu;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.Panel
{
    public class ChestPanel : MonoBehaviour
    {
        [SerializeField] private UnitButton _openButton;
        [SerializeField] private TMP_Text _awardAmount;
        [SerializeField] private Chest _chest;

        private IDisposable _subcription;
        private float _addAmount;
        
        private void Awake()
        {
            Wallet wallet = new(YG2.saves.Money);
            WalletSaver walletSaver = new(wallet);

            _chest.Initialize(wallet);

            _subcription = _openButton.Pressed
                .Subscribe(_ => ShowAward());
        }

        private void ShowAward()
        {
            _subcription.Dispose();
            
            
            _chest.AddAward();
            _awardAmount.text = _chest.AwardAmount.ToString();
        }
    }
}