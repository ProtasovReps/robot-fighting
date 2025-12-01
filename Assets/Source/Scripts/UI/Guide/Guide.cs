using System;
using R3;
using UnityEngine;
using YG;

namespace UI.Guide
{
    public class Guide : MonoBehaviour
    {
        private const float DefaultTimeScale = 1f;
        
        [SerializeField] private Transform[] _objectsToDisable;
        [SerializeField] private Replic[] _replics;
        [SerializeField] private float _enabledTimeScale;
        
        private int _replicIndex;
        private IDisposable _subscription;
        
        private void Start()
        {
            if (YG2.saves.IsGuidePassed)
            {
                SetGuideActive(false);
                return;
            }
            
            SetGuideActive(true);
            StartReplic();
        }

        private void StartReplic()
        {
            if (_replicIndex > _replics.Length - 1)
            {
                SetGuideActive(false);
                return;
            }
            
            Replic newReplic = _replics[_replicIndex];

            _subscription = newReplic.Executed
                .Subscribe(_ => ChangeReplic());

            newReplic.gameObject.SetActive(true);
            newReplic.Say();
        }
        
        private void ChangeReplic()
        {
            _subscription?.Dispose();
            _replics[_replicIndex].gameObject.SetActive(false);
            
            _replicIndex++;
            
            StartReplic();
        }

        private void SetGuideActive(bool isActive)
        {
            Time.timeScale = isActive ? _enabledTimeScale : DefaultTimeScale;
            gameObject.SetActive(isActive);
            
            foreach (Transform disabling in _objectsToDisable)
            {
                disabling.gameObject.SetActive(!isActive);
            }
        }
    }
}