using R3;
using UI.Buttons;
using UI.Panel;
using UnityEngine;

namespace YG.Awards
{
    public abstract class AwardPanel : SwitchablePanel
    {
        [SerializeField] private RewardType _rewardType;
        [SerializeField] private UnitButton _unitButton;

        private void Awake()
        {
            _unitButton.Pressed
                .Subscribe(_ => YG2.RewardedAdvShow(_rewardType.ToString(), AddAward))
                .AddTo(this);
        }

        protected abstract void AddAward();
    }
}