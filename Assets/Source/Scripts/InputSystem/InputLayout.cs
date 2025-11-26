using UnityEngine;
using YG;

namespace InputSystem
{
    public class InputLayout : MonoBehaviour
    {
        [SerializeField] private Transform[] _mobileLayout;
        [SerializeField] private Transform _pCLayout;

        private void Awake()
        {
            if (YG2.envir.isDesktop)
            {
                _pCLayout.gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < _mobileLayout.Length; i++)
                {
                    _mobileLayout[i].gameObject.SetActive(true);
                }
            }
        }
    }
}