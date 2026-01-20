using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace YG
{
    public class SDKAwaiter : MonoBehaviour
    {
        private const string SDKEnabled = nameof(SDKEnabled);
        
        public async UniTaskVoid WaitSDKInitialization(Action callback)
        {
            while (YG2.isSDKEnabled == false)
            {
                await UniTask.Yield();
            }

            YG2.MetricaSend(SDKEnabled);
            callback?.Invoke();
        }
    }
}