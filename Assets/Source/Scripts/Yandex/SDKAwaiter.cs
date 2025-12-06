using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace YG
{
    public class SDKAwaiter : MonoBehaviour
    {
        public async UniTaskVoid WaitSDKInitialization(Action callback)
        {
            while (YG2.isSDKEnabled == false)
            {
                await UniTask.Yield();
            }

            callback?.Invoke();
        }
    }
}