using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace UI.Guide.Pointers
{
    public class StorePointer : MonoBehaviour
    {
        private const int MinScenePassedToShow = 5;
        private const int MaxScenePassedToShow = 9;
        
        [SerializeField] private float _disableDelay;

        private CancellationTokenSource _cancellationTokenSource;
        
        private void Awake()
        {
            int sceneIndex = YG2.saves.SceneIndex;

            if (sceneIndex > MinScenePassedToShow && sceneIndex < MaxScenePassedToShow)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                
                DisableDelayed(_cancellationTokenSource.Token).Forget();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid DisableDelayed(CancellationToken token)
        {
            await UniTask.WaitForSeconds(_disableDelay, cancellationToken: token, cancelImmediately: true);
            gameObject.SetActive(false);
        }
    }
}