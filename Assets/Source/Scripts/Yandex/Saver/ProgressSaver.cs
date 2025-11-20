using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace YG.Saver
{
    public class ProgressSaver : MonoBehaviour
    {
        private readonly List<ISaver> _savers = new();

        public void Save()
        {
            if (_savers.Count == 0)
                return;
            
            for (int i = 0; i < _savers.Count; i++)
            {
                _savers[i].Save();
            }            
            
            YG2.SaveProgress();
        }

        public void Add(ISaver saver)
        {
            _savers.Add(saver);
        }
    }
}