using System;
using UI.Store;
using UnityEngine;

namespace ImplantSystem
{
    public class ImplantStash : MonoBehaviour
    {
        [SerializeField] private ImplantView[] _implantViews;

        public ImplantView Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            
            for (int i = 0; i < _implantViews.Length; i++)
            {
                if (_implantViews[i].Name == name)
                    return _implantViews[i];
            }

            throw new ArgumentException(name);
        }
    }
}