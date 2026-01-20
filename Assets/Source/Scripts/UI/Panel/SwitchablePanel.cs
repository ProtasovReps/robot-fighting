using R3;
using UnityEngine;

namespace UI.Panel
{
    public class SwitchablePanel : MonoBehaviour
    {
        private readonly Subject<bool> _enableSwitched = new ();

        public Observable<bool> EnableSwitched => _enableSwitched;

        public void SetEnable(bool isEnabled)
        {
            _enableSwitched.OnNext(isEnabled);
        }
    }
}