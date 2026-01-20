using R3;
using UnityEngine;
using YG;

namespace UI.Guide
{
    public class PlatformReplic : Replic
    {
        [SerializeField] private bool _isPC;
        
        protected override void Say()
        {
            if (YG2.envir.isDesktop == _isPC)
            {
                base.Say();
            }
            else
            {
                SubjectExecuted.OnNext(Unit.Default);
            }
        }
    }
}