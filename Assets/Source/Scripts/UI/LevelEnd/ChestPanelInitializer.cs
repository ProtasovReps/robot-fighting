using Interface;
using Reflex.Attributes;
using UI.Info;
using UnityEngine;

namespace UI.LevelEnd
{
    public class ChestPanelInitializer : MonoBehaviour
    {
        [SerializeField] private IntegerView _moneyAmount;

        [Inject]
        private void Inject(IValueChangeable<int> wallet)
        {
            _moneyAmount.Initialize(wallet);
        }
    }
}