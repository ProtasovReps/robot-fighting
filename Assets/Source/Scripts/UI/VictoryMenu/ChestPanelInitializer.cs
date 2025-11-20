using CharacterSystem;
using Reflex.Attributes;
using UI.Buttons;
using UI.Effect;
using UI.Info;
using UI.Panel;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.VictoryMenu
{
    public class ChestPanelInitializer : MonoBehaviour
    {
        [SerializeField] private IntegerView _moneyAmount;
        [SerializeField] private ChestPanel _chestPanel;
        [SerializeField] private Chest _chest;
        [SerializeField] private MultiplyAwardPanel _multiplyAwardPanel;
        [SerializeField] private AnimatablePanelSwitcher _switcher;
        [SerializeField] private UnitButton _goNextButton;

        private ProgressSaver _progressSaver;
        
        [Inject]
        private void Inject(ProgressSaver saver)
        {
            _progressSaver = saver;
        }
        
        private void Awake()
        {
            Wallet wallet = new(YG2.saves.Money);
            WalletSaver walletSaver = new(wallet);
            
            _progressSaver.Add(walletSaver);
            _chest.Initialize(wallet);
            _chestPanel.Initialize(_chest);
            _multiplyAwardPanel.Initialize(_chest);
            _moneyAmount.Initialize(wallet);
            
            _switcher.Initialize(_goNextButton.Pressed);
        }
    }
}