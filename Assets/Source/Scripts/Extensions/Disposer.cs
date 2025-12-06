using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public class Disposer : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();

        private void OnDestroy()
        {
            if (_disposables.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[i].Dispose();
            }
        }
        
        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }
}